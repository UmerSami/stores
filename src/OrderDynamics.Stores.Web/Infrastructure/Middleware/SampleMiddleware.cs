using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;
using OrderDynamics.Stores.Web.Infrastructure.Configuration;

namespace OrderDynamics.Stores.Web.Infrastructure.Middleware
{
    public class SampleMiddleware {
        private readonly RequestDelegate _next;
        private readonly IOptions<ConfigurationOptions> _options; 

        public SampleMiddleware(RequestDelegate next, IOptions<ConfigurationOptions> options) {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context) {
            await _next.Invoke(context);
        }
    }
}
