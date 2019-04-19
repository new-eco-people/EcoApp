using System.Reflection;
using Application.Entities.UserEntity.Command.RegisterUser;
using Application.Infrastructure.AutoMapper;
using Application.Infrastructure.RequestResponsePipeline;
using AutoMapper;
using FluentValidation.AspNetCore;
using Infrastructure.Extensions;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Extensions;
using Persistence.Repository;
using Web.Ui.Filters;

namespace Web.Ui
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Database and Microsoft Identity
            services.ConfigureDatabaseConnections(
                Configuration.GetConnectionString("DefaultConnection"),
                "Web.Ui"
            );

            //Add Mediator
            services.AddMediatR();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services                
                // Handling Application Exceptions on the Web
                .AddMvc(options => {
                    // Kindly Remove the next line of code
                    // options.Filters.Add(typeof(ValidateModelStateAttribute));
                    options.Filters.Add(typeof(WebCustomExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                // For performing validation of user data before using in the application
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserValidator>());
            
            // Kindly Remove
            // services.Configure<ApiBehaviorOptions>(options =>
            // {
            //     options.SuppressModelStateInvalidFilter = true;
            // });

            
            // Add DataContext implementation of Application interfaces
            services.ImplementApplicationDatabaseInterfaces();

            // Add Infrastructure implementation of Application interfaces
            services.AddInfractureServices();

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });


            // For front-end framework
            // // In production, the React files will be served from this directory
            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = "ClientApp/build";
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();


            // For front-end framework
            // app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseMvc(
            // For front-end framework
            //     routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller}/{action=Index}/{id?}");
            // }
            );

            // For front-end framework Mainly for React, angular api is also avaliable
            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "ClientApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseReactDevelopmentServer(npmScript: "start");
            //     }
            // });
        }
    }
}
