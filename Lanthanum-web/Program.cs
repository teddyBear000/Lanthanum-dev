using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Lanthanum_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                foreach (var user in context.Users.Include(a => a.Articles))
                {
                    Console.WriteLine($"User: {user.FirstName}");

                    foreach (var article in user.Articles)
                    {
                        Console.WriteLine($" - Article: " + article.Headline);
                    }
                }
            }
            Console.WriteLine();

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
