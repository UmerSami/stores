using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core;
using OrderDynamics.Stores.Web.Infrastructure.Configuration;
using OrderDynamics.Stores.Web.Infrastructure.Middleware;
using OrderDynamics.Stores.Web.Infrastructure.Services;

namespace OrderDynamics.Stores.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv) {
            SetupConfiguration();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddOptions();
            services.Configure<ConfigurationOptions>(Configuration);

            services.AddMvc();

            services.AddScoped<IFakeShipmentService, FakeShipmentService>();

            services.AddScoped<IApiClientFactory, WebApiClientFactory>();
            services.AddScoped<IRequestCredentialsProvider, BasicRequestCredentialsProvider>();
            services.AddScoped<IRequestVersionProvider, RequestVersionProvider>();
            services.AddScoped<IRequestBuilder, RequestBuilder>();
            services.AddScoped<IHttpClientProvider, TransientHttpClientProvider>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app) {
            app.UseMiddleware<SampleMiddleware>();
            app.UseStaticFiles();
            app.UseMvc(ConfigureRoutes);
        }

        private void SetupConfiguration() {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.secret.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private static void ConfigureRoutes(IRouteBuilder routeBuilder) {
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
