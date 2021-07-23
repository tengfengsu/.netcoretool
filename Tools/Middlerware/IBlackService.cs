using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Tools.Middlerware
{
    public interface IBlackService
    {
        Task<bool> IsBlack(HttpContext httpContext, BlackOption options);
    }
}