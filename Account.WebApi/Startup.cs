using System;
using Account.Data;
using Account.Data.Repositories;
using Account.Services.Interfaces;
using Account.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Account.WebApi
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
            services.AddControllers();
            services.AddDbContext<AccountContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("AccountDB")));
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<IAccountInfoService, AccountInfoService>();
            services.AddScoped<IAccountInfoRepository, AccountInfoRepository>();
            services.AddScoped<INewAccountService, NewAccountService>();
            services.AddScoped<INewAccountRepository, NewAccountRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "AccountOpenAPI",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Account API",
                        Version = "1"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/AccountOpenAPI/swagger.json",
                    "Account API");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
