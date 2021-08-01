using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventStore.Api
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// DI Configuration of .NET
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); 
            
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConfiguration(Configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
                builder.AddApplicationInsights();
            });
            services.AddHttpContextAccessor();
            services.AddResponseCaching();

            //Load options pattern configuration
            LoadConfiguration(services);

            //Load mediator pipeline features
            LoadMediator(services);

            //Load swagger configuration
            LoadSwagger(services);

            //Load authentication configuration
            LoadAuthentication(services);

            services.AddMvc(options =>
                {
                    // order does matter - add custom filters 
                    //options.Filters.Add<TypeOfFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(c =>
            {
                c.WithOrigins("http://localhost:4200"); //TODO - read this from configuration
                c.WithHeaders("authorization", "accept", "content-type", "origin"); //TODO - add custom headers for multi tenancy
                c.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "EventStore Api v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
