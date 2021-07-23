using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tools.Middlerware
{

    public class BlackService : IBlackService
    {
        public async Task<bool> IsBlack(HttpContext httpContext, BlackOption options)
        {
            if (!options.IsEnable)
            {
                return false;
            }

            var origin = httpContext.Request.Headers["Origin"].ToString();
            if (options.Blacks.Contains(origin))
            {
                return true;
            }
            return false;
        }
    }
}
