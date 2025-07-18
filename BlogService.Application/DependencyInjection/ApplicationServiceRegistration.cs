using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(typeof(ApplicationServiceRegistration).Assembly);
            return services;
        }
    }
}
