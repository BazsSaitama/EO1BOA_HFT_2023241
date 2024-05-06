using EO1BOA_HFT_2023241.Endpoint.Services;
using EO1BOA_HFT_2023241.Logic;
using EO1BOA_HFT_2023241.Logic.Interfaces;
using EO1BOA_HFT_2023241.Models;
using EO1BOA_HFT_2023241.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<BakeryDbContext>();
            services.AddTransient<IRepository<Bread>, BreadRepository>();
            services.AddTransient<IRepository<Bakery>, BakeryRepository>();
            services.AddTransient<IRepository<Oven>, OvenRepository>();
            services.AddTransient<IBreadLogic, BreadLogic>();
            services.AddTransient<IBakeryLogic, BakeryLogic>();
            services.AddTransient<IOvenLogic, OvenLogic>();
            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "EO1BOA_HFT_2023241.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "EO1BOA_HFT_2023241.Endpoint v1"));
            }

            app.UseExceptionHandler(x => x.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            
            app.UseCors(x => x
             .AllowCredentials()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithOrigins("http://localhost:39340"));

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
            
        }
    }
}
