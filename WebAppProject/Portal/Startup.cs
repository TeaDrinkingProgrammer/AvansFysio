using Portal.Models;
using DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Domain;
using NToastNotify;

namespace Portal {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddScoped<IPatientRepo, DbPatientRepo>();
            services.AddScoped<IEmployeeRepo, DbEmployeeRepo>();
            services.AddScoped<IAppointmentRepo, DbAppointmentRepo>();
            services.AddScoped<ITreatmentPlanRepo, DbTreatmentPlanRepo>();
            services.AddScoped<IPatientFileRepo, DbPatientFileRepo>();
            services.AddScoped<ITreatmentRepo, DbTreatmentRepo>();
            services.AddScoped<IVektisRepo, ApiVektisRepo>();
            services.AddScoped<UserManager<IdentityUser>>();
            services.AddScoped<SignInManager<IdentityUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<IdentitySeeder>();
            services.AddHttpContextAccessor();

            services.AddDbContext<FysioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZëË0123456789 ";
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.ConfigureApplicationCookie(options => {
                options.AccessDeniedPath = new PathString("/Authentication/AccessDenied");
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = new PathString("/Authentication/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.Configure<IdentityOptions>(opts => {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
            });
            services.AddAuthorization(options => {
                options.AddPolicy("RequirePhysicalTherapist",
                    //TODO how to work with claims, we have no third party.
                    policy => policy.RequireClaim("PhysicalTherapist"));
                options.AddPolicy("RequireEmployee", policy => policy.RequireClaim(ClaimTypes.Authentication, "Employee"));
                options.AddPolicy("RequirePatient", policy => policy.RequireClaim(ClaimTypes.Authentication, "Patient"));
                options.AddPolicy("RequireTherapist", policy => policy.RequireClaim(ClaimTypes.Authentication, "TherapistEmployee"));
            });


            services.AddControllersWithViews().AddNToastNotifyNoty(new NotyOptions {
                ProgressBar = false,
                Layout = "topCenter",
                Timeout = 2000
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentitySeeder identitySeeder) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNToastNotify();


            identitySeeder.SeedEmployees();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
