using PestControl.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace PestControl.Configurations
{
    public static class JobsConfig
    {
        public static IServiceCollection ConfigureBackgroundJobs(this IServiceCollection services)
        {
            services.AddHostedService<MessagingJob>();

            return services;
        }
    }
}
