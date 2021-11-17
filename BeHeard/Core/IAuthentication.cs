using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface IAuthentication
    {
        IImmutableDictionary<string, IRefreshToken> UserRefreshTokensReadOnlyDictionary { get; }
        IAuthenticationResult GenerateTokens(string username, Claim[] claims, DateTime now);
        IAuthenticationResult Refresh(string refreshToken, string accessToken, DateTime now);
        void RemoveExpiredRefreshTokens(DateTime now);
        void RemoveRefreshTokenByUserName(string userName);
    }
}
