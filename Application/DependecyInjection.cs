using FluentValidation;
using IzumuClientes.API.Services;
using IzumuClientes.API.Validators;
using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Interfaces;
using IzumuClientes.Application.Services;
using IzumuClientes.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace IzumuClientes.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IValidator<ClientRequestDTO>, ClientValidator>();
            services.AddScoped<IValidator<PhoneRequestDTO>, PhoneValidator>();
            services.AddScoped<IValidator<EmailRequestDTO>, EmailValidator>();
            services.AddScoped<IValidator<AddressRequestDTO>, AddressValidator>();

            return services;
        }
    }
}
