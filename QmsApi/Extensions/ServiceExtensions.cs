namespace QmsApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                policy.WithOrigins("http://localhost:4200", "https://localhost:7230", "http://localhost:5112", "https://sivindicator.intel.com/")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(hostName => true));

                options.AddPolicy("AllowAll",
                   builder => builder.SetIsOriginAllowed(_ => true)
                   .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            });     
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });
    }
}
