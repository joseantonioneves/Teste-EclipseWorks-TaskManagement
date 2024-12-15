using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            return services;
        }
    }
}
