using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealWorldApp.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddSingleton<ExceptionMiddleware>();




            return services;

        }

            public static WebApplication UseInfrastructure(this WebApplication app)
            {
                app.UseMiddleware<ExceptionMiddleware>();

                return app;
            }

            public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
            {
                var options = new T();
                var section = configuration.GetRequiredSection(sectionName);
                section.Bind(options);

                return options;
            }
        }
    } 

