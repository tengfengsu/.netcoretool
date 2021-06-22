using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tools.Filter
{
    public class ArgumentAuthenticationAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var args = context.ActionArguments;

            var value = args.Values.FirstOrDefault();

            var auth = (Authentication)value;
            if (auth != null)
            {
                var user = new User() { Code = auth.Code };
                var claims = AuthenticationExtension.Serialize(user);
                var contextUser = new ClaimsPrincipal(new ClaimsIdentity(claims, ApiTokenOptions.Scheme));
                context.HttpContext.User = contextUser;
            }

            if (context.HttpContext.User == null)
            {
                throw new UnAuthenticationException($"未登录");
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }

    public class UnAuthenticationException : Exception
    {
        public UnAuthenticationException(string msg) : base(msg)
        {

        }
    }
    public class ApiTokenOptions : AuthenticationSchemeOptions
    {
        public const string Scheme = "BasicAuth";
    }
    public class Authentication
    {
        public string Code { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public string Code { get; set; }
    }
    public static class AuthenticationExtension
    {
        public static List<Claim> Serialize(User user)
        {
            var dices = JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(user));
            var claims = dices?
                .Where(x => x.Value != null)
                .Select(dic => new Claim(dic.Key, dic.Value))
                .ToList();
            return claims;
        }

        public static User Deserialize(List<Claim> claims)
        {
            User user = null;
            if (claims.Any())
            {
                var dic = claims.Select(claim => new { Type = claim.Type, Value = claim.Value });
                user = JsonSerializer.Deserialize<User>(JsonSerializer.SerializeToUtf8Bytes(dic));
            }
            return user;
        }
    }
}
