using Lanthanum.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Options;
using Lanthanum.Web.Services;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Lanthanum.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auth
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Authentication/LogIn");
                    options.AccessDeniedPath = new PathString("/Authentication/LogIn");
                });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("DefaultConnection"))
            {
                UserID = Configuration["Database:User"],
                Password = Configuration["Database:Password"]
            };

            services.AddDbContext<ApplicationContext>(
                options => options.UseMySql(
                    builder.ConnectionString,
                    new MySqlServerVersion(new Version(8, 0, 26)),
                    x => x.MigrationsAssembly("Lanthanum.Data")
                )
            );
            
            // Email sender service configuration
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGridOptions"));
            services.Configure<SendGridOptions>(options =>
            {
                options.ApiKey = Configuration["SendGridApiKey"];
            });

            // Session config
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); //set session expire time
                options.Cookie.IsEssential = true;
            });

            // DI
            services.AddTransient<DbRepository<Article>>();
            services.AddTransient<DbRepository<Comment>>();
            services.AddTransient<DbRepository<User>>();
            services.AddTransient<DbRepository<KindOfSport>>();
            services.AddScoped<DbRepository<FooterTabItem>>();
            services.AddSingleton<AuthService>();
            services.AddScoped<FooterService>();
            services.AddSingleton<IEmailSenderService, SendGridService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
            
        }
    }
}
