using _0_Framework.Application;
using _0_Framework.Application.Email;
using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Infrastructure;
using A_Test.Configuration;
using AccountManagement.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using InventoryManagement.Presentation.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using ShopManagement.Presentation.Api;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ServiceHost
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
            services.AddHttpContextAccessor(); //Before Asp.net core 3  It was like this:  services.AddScoped<IHttpContextAccessor,HttpContextAccessor>();
            var connectionString = Configuration.GetConnectionString("LampshadeDb");
            ShopManagementBootstrapper.Configure(services, connectionString);
            DiscountManagementBootstrapper.Configure(services, connectionString);
            InventoryManagementBootstrapper.Configure(services, connectionString);
            BlogManagementBootstrapper.Configure(services, connectionString);
            CommentManagementBootstrapper.Configure(services, connectionString);
            AccountManagementBootstrapper.Configure(services, connectionString);

            //#######---for testing----
            var connectionString_2 = Configuration.GetConnectionString("LampshadeDb_2");
            A_Test_Bootstrapper.Configure(services, connectionString_2);
            //#######------------------

            //This line is neede to set the right format for persian characters in meta tags in the page source:
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            //services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
            //services.AddTransient<ISmsService, SmsService>();
            //services.AddTransient<IEmailService, EmailService>();


            //---------we need this lines to add Cookies to httpContex---------
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });
            //-----------------------------------------------------------------

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea",
                builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

                options.AddPolicy("Shop",
                builder => builder.RequireRole(new List<string> { Roles.Administrator }));

                options.AddPolicy("Discount",
                builder => builder.RequireRole(new List<string> { Roles.Administrator }));

                options.AddPolicy("Account",
                builder => builder.RequireRole(new List<string> { Roles.Administrator }));
            });


            services.AddRazorPages()
                .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea"); //--->This means: in the 'Administration' area , dont let any acount to come in the '/' folder , exept those acounts there are in the 'AdminArea' policy .
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop"); //--->This means: in the 'Administration' area , dont let any acount to come in the '/Shop' folder , exept those acounts there are in the 'Shop' policy .
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
                })
                .AddApplicationPart(typeof(ProductController).Assembly) 
                .AddApplicationPart(typeof(InventoryController).Assembly); //این دو قسمت به ما این اجازه را میدهند که کنترلر ها رو از یک اسمبلی دیگر لود کنیم
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
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication(); //-------------->we need this line to add Cookies to httpContex

            app.UseStaticFiles();

            app.UseCookiePolicy(); //---------------->we need this line to add Cookies to httpContex

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers(); // این برای این است که ای پی آی ها رو هم بخونه
                //endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
