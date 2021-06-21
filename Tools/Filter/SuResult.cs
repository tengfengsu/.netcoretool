using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tools.Dto;

namespace Tools.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NoWarpResultAttribute : Attribute
    {

    }

    public class SuResult : ActionFilterAttribute
    {
        private static readonly Type _ResultType = typeof(Result);
        /// <inheritdoc />
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(x => x is NoWarpResultAttribute))
            {
                await base.OnResultExecutionAsync(context, next);
                return;
            }
            var result = context.Result as ObjectResult;
            if (result == null)
            {
                var noResult = new ObjectResult(Result.FromSuccess());
                noResult.StatusCode = 200;
                context.Result = noResult;
            }
            else if (result.Value?.GetType() != _ResultType)
            {
                context.Result = new ObjectResult(Result.FromData(result.Value));
            }
            await base.OnResultExecutionAsync(context, next);
        }
    }
}
