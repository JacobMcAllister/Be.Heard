using BeHeard.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BeHeard.Application
{
    public class Authentication : IAuthentication
    {
        public IImmutableDictionary<string, IRefreshToken> UserRefreshTokensReadOnlyDictionary => _userRefreshTokens.ToImmutableDictionary();
        private readonly ConcurrentDictionary<string, IRefreshToken> _userRefreshTokens; // can store in a database
        private readonly IJwtTokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;

        public Authentication(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _userRefreshTokens = new ConcurrentDictionary<string, IRefreshToken>();
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }

        public IAuthenticationResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty, // keep token clean (short aud array)
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserName = username,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
            };
            _userRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (string x, IRefreshToken y) => refreshToken);

            return new AuthenticationResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.TokenString
            };
        }

        public IAuthenticationResult Refresh(string refreshToken, string accessToken, DateTime now)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            var userName = principal.Identity?.Name;
            if (!_userRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return GenerateTokens(userName, principal.Claims.ToArray(), now); // need to recover original claims
        }

        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            throw new NotImplementedException();
        }

        public void RemoveRefreshTokenByUserName(string userName)
        {
            throw new NotImplementedException();
        }
  

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validateToken);
            return (principal, validateToken as JwtSecurityToken);
        }
    }

    public class RefreshToken : IRefreshToken
    {
    }

    public class AuthenticationResult : IAuthenticationResult
    {
    }
}
