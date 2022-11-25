using Microsoft.Extensions.Options;

namespace SimpleEmailApp.ConfgureSetting
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureWritable<T>(
       this IServiceCollection services,
       IConfigurationSection section,
       string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableOptionsMail<T>>(provider =>
            {
                var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
                var environment = provider.GetService<IWebHostEnvironment>();
                var options = provider.GetService<IOptionsSnapshot<T>>();

                return new WritableOptionsMail<T>(environment, options, configuration, section.Key, file);
            });
        }
    }
}
