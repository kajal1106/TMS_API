using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TMS_Microservice.Data;
using TMS_Microservice.Data.Repository;
using TMS_Microservice.Repository;
using TMS_Microservice.Repository.IRepository;

namespace TMS_Microservice
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
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ISubtaskRepository, SubtaskRepository>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("TMSOpenAPISpec",
                  new Microsoft.OpenApi.Models.OpenApiInfo()
                  {
                      Title = "TMS Microservice",
                      Version = "1",
                      Description = "Task Management System API",
                      Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                      {
                          Email = "singh.kajal940@gmail.com",
                          Name = "Kajal Singh"
                      },
                      License = new Microsoft.OpenApi.Models.OpenApiLicense()
                      {
                          Name = "MIT License",
                          Url = new Uri("https://en.wikipedia.org/wiki/MIT_LICENSE")
                      }
                  });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(commentsFullPath);
            });
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseHttpsRedirection();

      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/TMSOpenAPISpec/swagger.json", "TMS API");
        options.RoutePrefix = "";
      });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
