using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tools.Middlerware
{
    public class BlackMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BlackMiddleware> _logger;
        private readonly IBlackService _blackService;
        private readonly BlackOption _options;

        public BlackMiddleware(RequestDelegate next,
            ILogger<BlackMiddleware> logger,
            IBlackService blackService,
            IOptions<BlackOption> options)
        {
            _next = next;
            _logger = logger;
            _blackService = blackService;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"black check");
            var isBlack = await _blackService.IsBlack(context, _options);
            if (isBlack)
            {
                Console.WriteLine($"fail");
                await context.Response.WriteAsync($".........black.........");
            }
            else
            {
                Console.WriteLine($"success");
                await _next(context);
            }
        }
    }
}
