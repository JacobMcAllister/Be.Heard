using BeHeard.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BeHeard.Application
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtTokenConfig _jwtTokenConfig; // needs secret

        public AuthenticationMiddleware(RequestDelegate next, JwtTokenConfig jwtTokenConfig)
        {
            _next = next;
            _jwtTokenConfig = jwtTokenConfig;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            // var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var token = context.Request.Cookies["token"];

            if (token != null)
                attachUserToContext(context, userService, token, _jwtTokenConfig);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token, IJwtTokenConfig jwtTokenConfig)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtTokenConfig.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                { 
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var username = jwtToken.Claims.First(x => x.Type.Contains("name")).Value;
                context.Items["User"] = username;

            }
            catch
            {
                // user is not attached to context
            }

        }

    }
}
