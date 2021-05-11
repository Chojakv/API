using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
        }
        
        public static string GetUsername(this HttpContext httpContext)
        {
            return httpContext.User.Claims.SingleOrDefault(x => x.Type == "Username")?.Value;
        }
        

    }
}