using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SkateboardNeverDie.Services.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

#if DEBUG
                    webBuilder.UseUrls("https://*:5003");
#endif
                });
    }
}
