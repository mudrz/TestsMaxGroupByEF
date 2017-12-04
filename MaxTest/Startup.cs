using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxTest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MaxTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddDbContext<LiteContext>(options => options.UseSqlite("Data Source=LiteContext.db"));
            // services.AddDbContext<PostgreContext>(
            //     options => options.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"))
            // );

            services.AddMiniProfiler(options =>
                                    {
                                        options.RouteBasePath = "/profiler";
                                    })
                    .AddEntityFramework();
        }
        public void Configure(IApplicationBuilder app,
                            IHostingEnvironment env,
                            LiteContext liteContext
                            // PostgreContext postgreContext
                            )
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiniProfiler();

            DataInit.InitializeData(liteContext);
            // DataInit.InitializeData(postgreContext);

            app.UseMvc();


        }
    }
}
