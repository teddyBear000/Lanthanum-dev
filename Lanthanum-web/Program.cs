using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Lanthanum_web.Models;

namespace Lanthanum_web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (KindOfSportRepository repo = new KindOfSportRepository(new ApplicationContext()))
            {

                repo.AddItem(new KindOfSport { Name = "Tanzi s hula-hupom" });
            }
            CreateHostBuilder(args).Build().Run();

            

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
