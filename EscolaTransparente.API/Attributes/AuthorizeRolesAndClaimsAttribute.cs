using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EscolaTransparente.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeRolesAndClaimsAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        private readonly string[] _claims;

        public AuthorizeRolesAndClaimsAttribute(string[] roles = null, string[] claims = null)
        {
            _roles = roles;
            _claims = claims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Verifica se o usuário está autenticado
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Verifica roles
            if (_roles != null && _roles.Any())
            {
                var hasRole = _roles.Any(role => context.HttpContext.User.IsInRole(role));
                if (!hasRole)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

            // Verifica claims
            if (_claims != null && _claims.Any())
            {
                var hasClaim = _claims.Any(claim =>
                {
                    var parts = claim.Split(':');
                    if (parts.Length != 2) return false;
                    return context.HttpContext.User.HasClaim(parts[0], parts[1]);
                });

                if (!hasClaim)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }
} 