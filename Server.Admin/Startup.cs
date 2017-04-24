using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Admin.Entity;
using Server.Admin.Mappers;
using System.Linq;
using System.Reflection;

namespace Server.Admin
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Data Source=bun-v-server;Initial Catalog=IdentityServer;Persist Security Info=True;User ID=sa;Password=as;";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityServerUser, IdentityRole>()
                .AddEntityFrameworkStores<MyContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)));

            services.AddMvc();

            services.AddAutoMapper(config =>
            {
                config.CreateMap<IdentityServer4.Models.Client, Server.Admin.Models.ClientViewModels.ClientViewModel>();
            }, /*typeof(Mappers.ClientMapperProfile)*/Assembly.GetEntryAssembly());

            services.AddScoped<ConfigurationDbContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeDatabase(app);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentityServer();

            app.UseIdentity();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
