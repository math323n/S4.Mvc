using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using S4.Entities.Models.Context;

using System;

namespace S4.Mvc.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Store connection string to database
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // Add context using connection string
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connection));

            services.AddMvc();
            services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddEntityFrameworkStores<NorthwindContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddControllers();
            services.AddLogging();

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule<ServiceModule>();
            builder.Populate(services);

            IContainer container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}