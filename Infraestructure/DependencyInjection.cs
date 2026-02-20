using IzumuClientes.Domain.Interfaces;
using IzumuClientes.Infraestructure.Data;
using IzumuClientes.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ITypeDocumentRepository, TypeDocumentRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            return services;
        }
    }
}
