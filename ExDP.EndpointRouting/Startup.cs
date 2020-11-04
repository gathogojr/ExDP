using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using ServiceNS.Models;

namespace ServiceNS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllers();
                routeBuilder.Select().Filter().Expand().Count().OrderBy().SkipToken().MaxTop(100);
                routeBuilder.MapODataRoute(
                    routeName: "odata",
                    routePrefix: "api", // This can be anything you want
                    model: GetEdmModel());
            });
        }

        private IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EnumType<Genre>();
            builder.ComplexType<Address>();
            builder.ComplexType<NextOfKin>();
            builder.EntitySet<Director>("Directors");

            return builder.GetEdmModel();
        }
    }
}
