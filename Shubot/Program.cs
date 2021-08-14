namespace Shubot
{
    using global::Shubot.Interfaces;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            ExemplifyScoping(host.Services);

            host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services) =>
                        services.AddScoped<IEnvironmentService, EnvironmentService>()
                                .AddScoped<ILoggingService, LoggingService>()
                                .AddTransient<Shubot>());
        } 

        static void ExemplifyScoping(IServiceProvider services)
        {
            IServiceProvider provider = services.CreateScope().ServiceProvider;

            provider.GetRequiredService<Shubot>()
                .MainAsync()
                    .GetAwaiter()
                        .GetResult();
        }
    }
}
