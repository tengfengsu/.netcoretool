using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tools.Middlerware
{
    public static class BlackMiddlewareExtension
    {
        public static IApplicationBuilder UseBlack(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BlackMiddleware>();
        }

        public static IApplicationBuilder UseBlack(this IApplicationBuilder app, BlackOption options)
        {
            return app.UseMiddleware<BlackMiddleware>(Options.Create(options));
        }
    }

    public static class BlackServiceExtension
    {
        public static IServiceCollection AddBlack(this IServiceCollection services)
        {
            return services.AddSingleton<IBlackService, BlackService>();
        }

        public static IServiceCollection AddBlack(this IServiceCollection services, Action<BlackOption> configure)
        {
            services.Configure(configure);
            return services.AddBlack();
        }
    }
}
