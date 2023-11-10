using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentAuthorService, CurrentAuthorService>();
        services.AddHttpContextAccessor();

        services.AddHealthChecks();
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}