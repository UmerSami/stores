using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace OrderDynamics.Stores.Web.Middleware
{
    public class SampleMiddleware {
        private readonly RequestDelegate _next;

        public SampleMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            await _next.Invoke(context);
        }
    }
}
