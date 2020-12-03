using likeButtonApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace likeButtonApi
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

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Articles API" });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddDbContext<DBContext>(
                                               options => options.UseSqlite(Configuration.GetConnectionString("SqliteConnectionString")
                                             ));


            //Resilience (not enable in sqllite)
            //services.AddDbContext<DbContext>(options =>
            //{
            //    options.UseSqlite(Configuration["ConnectionString"],
            //    sqliteOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly(
            //            typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

            //        //Configuring Connection Resiliency:
            //        sqlOptions.
            //            EnableRetryOnFailure(maxRetryCount: 5,
            //            maxRetryDelay: TimeSpan.FromSeconds(30),
            //            errorNumbersToAdd: null);

            //    });

            // implementation de cache
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration =
            //           Configuration.GetConnectionString("Conexao_Redis");
            //    options.InstanceName = "Demo_RedisCache";
            //});

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Articles API");
            });

            app.UseRouting();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
