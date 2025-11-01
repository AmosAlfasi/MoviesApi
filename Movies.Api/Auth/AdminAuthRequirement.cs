using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Movies.Api.Auth
{
    public class AdminAuthRequirement : IAuthorizationHandler, IAuthorizationRequirement
    {
        private readonly string _apiKey;

        public AdminAuthRequirement(string apiKey)
        {
            _apiKey = apiKey;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User.HasClaim(AuthConstants.UserTypeClaimName, AuthConstants.AdminUserClaimName))
            {
                context.Succeed(this);
                return Task.CompletedTask;
            }

            var httpcontext = context.Resource as HttpContext;
            if (httpcontext is null)
            {
                return Task.CompletedTask;
            }
            if (!httpcontext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName,
               out var extractedApiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (_apiKey != extractedApiKey)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var identity = (ClaimsIdentity)httpcontext.User.Identity!;
            identity.AddClaim(new Claim("userid", "headerAuthId"));

            context.Succeed(this);
            return Task.CompletedTask;
        }
    }
}
}
