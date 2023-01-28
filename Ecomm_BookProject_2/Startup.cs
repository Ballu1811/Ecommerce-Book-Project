using Ecomm_BookProject_2.DataAccess.Data;
using Ecomm_BookProject_2_DataAccess.Repository;
using Ecomm_BookProject_2_DataAccess.Repository.IRepository;
using Ecomm_BookProject_2_Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_BookProject_2
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // AddScope Method for adding interface classes
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add Identity User
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();

            // Add Email Sender
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddControllersWithViews();

            // Add Cookie Page for Authorized Controller
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.AccessDeniedPath = $"/Idetity/Account/AccessDenied";
                options.LogoutPath = $"/Identity/Account/Logout";
            });

            // Add Facebook signin Authentication settings
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "1214742725738505";
                options.AppSecret = "834af678f0ade1a6d28c773f756f9153";
            });

            // Add Google Signin Authentication setup
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "575453643629-thtdocaf7jh9preac0cmblqj9soidnj3.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

            // Add Session Setup
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add Stripe Settings
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            // Configure for Email Verification mail
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            app.UseSession();  // write this line of code for using session..

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
