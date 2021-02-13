using System.Diagnostics.CodeAnalysis;
using Library.Book.Service;
using Library.Book.Service.Data;
using Library.Book.Service.Data.Repositories;
using Library.Services.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Library.Book.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string Url = "/swagger/v1/swagger.json";
        private const string Title = "Library OS Book API";
        private const string Version = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc(x=>x.EnableEndpointRouting = false);

            AddSwagger(services);
            AddOptions(services);
            AddContext(services);
            AddRepositories(services);
            AddComponents(services);
            AddMassTransit(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            //app.UseHttpsRedirection();

            // global cors policy
            // app.UseCors(x => x
            //                 .AllowAnyOrigin()
            //                 .AllowAnyMethod()
            //                 .AllowAnyHeader()
            //                 .AllowCredentials());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Url, Title);
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }

        private void AddSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc(Version, new OpenApiInfo {Title = Title, Version = Version}); });
        }

        private void AddContext(IServiceCollection services)
        {
            // context configuration
            var context = Configuration.GetConnectionString("BookContext");
            services.AddDbContext<BookDbContext>(
                o => o.UseSqlServer(context, b => b.MigrationsAssembly("Library.Book.Api")));
            services.AddTransient<DbContext, BookDbContext>();
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
        }

        private void AddComponents(IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
        }

        private void AddOptions(IServiceCollection services)
        {
            services.Configure<MassTransitConfigConstants>(
                Configuration.GetSection(AppConfigConstants.MassTransitConfigSectionName));
        }

        private void AddMassTransit(IServiceCollection services)
        {
            Configuration.GetSection(AppConfigConstants.MassTransitConfigSectionName)
                         .Get<MassTransitConfigConstants>();

            services.AddSingleton<BusConfigurator>();
            services.AddMassTransit(x =>
                {
                    x.AddBus(provider =>
                    {
                        var busConfigurator = provider.GetRequiredService<BusConfigurator>();
                        var busControl = busConfigurator.ConfigureBus((cfg, host) => { });
                        return busControl;
                    });
                }
            );
        }
    }
}