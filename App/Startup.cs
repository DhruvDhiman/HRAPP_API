using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App.Entities;
using Microsoft.EntityFrameworkCore;
using App.Services;
using Microsoft.AspNetCore.Cors;

namespace App
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Db connection service
            var connectionString = Configuration["ConnectionStrings:appDBConnectionString"];
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddCors(
                options => options.AddPolicy("AllowCors",
                    builder =>
                        {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod() 
                                .AllowAnyHeader();
                        })
            );

            // add mvc services
            services.AddMvc();
                //.AddJsonOptions(options => 
                //options.SerializerSettings.ReferenceLoopHandling =
                //Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // register the repository
            services.AddScoped<IAppRepository, AppRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext appContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Internal error. Please try again later.");
                    });
                });
            }
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //mappings
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Employee, Models.EmployeeDto>()
                    .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(
                        src => src.EmployeeSalary.BaseSalary))
                    .ForMember(dest => dest.Deduction401, opt => opt.MapFrom(
                        src => src.EmployeeSalary.Deduction401))
                    .ForMember(dest => dest.DeductionMedicare, opt => opt.MapFrom(
                        src => src.EmployeeSalary.DeductionMedicare))
                    .ForMember(dest => dest.DeductionDental, opt => opt.MapFrom(
                        src => src.EmployeeSalary.DeductionDental));

                cfg.CreateMap<Models.EmployeeForCreationDto, Entities.Employee>();

                cfg.CreateMap<Models.EmployeeForUpdateDto, Entities.Employee>();

                cfg.CreateMap<Entities.Employee, Models.EmployeeForUpdateDto>();

                cfg.CreateMap<Entities.PayChecks, Models.PayCheckDto>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(
                        src => src.Employee.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(
                        src => src.Employee.LastName));

                cfg.CreateMap<Models.PayCheckForCreationDto, Entities.PayChecks>();

            });
            appContext.EnsureSeedData();
            //Enable CORS policy "AllowCors"
            app.UseCors("AllowCors");
            //app.UseCors(options => options.AllowAnyOrigin()
            //                                .AllowAnyMethod()                    
            //);
            app.UseMvc();
            
        }
    }
}
