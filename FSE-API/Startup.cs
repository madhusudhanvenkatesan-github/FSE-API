using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FSE_API.Utilities;
using FSE_API.Service;
using FSE_API.Database;
using FSE_API.Repository;

namespace FSE_API
{
    [ExcludeFromCodeCoverage]
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
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    });
            services.AddAsyncDocumentSession(Configuration);
            services.AddScoped<IUser, User>();
            services.AddScoped<IProjectTask, ProjectTask>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskProjectService, TaskProjectService>();
            services.AddCors(corsOption => corsOption.AddPolicy("ReactPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();

            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
