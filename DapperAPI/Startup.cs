using System;
using DapperAPI.Database;
using DapperAPI.QueryController;
using DapperAPI.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DapperAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => 
                    builder.WithOrigins("https://localhost:5006")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            
            services.AddControllers();

            services.AddSingleton(new DatabaseConfig { Name = Configuration["DatabaseName"] });

            services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
            services.AddSingleton<IStudentQueryController, StudentQueryController>();
            services.AddSingleton<ICourseQueryController, CourseQueryController>();
            services.AddSingleton<IGradesQueryController, GradesQueryController>();
            services.AddSingleton<IRandomObjectCreator, RandomObjectCreator>();
            services.AddSingleton<IDatabasePopulator, DataBasePopulator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
 
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
 
            serviceProvider.GetService<IDatabaseBootstrap>().Setup();
            
        }
    }
}