using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using src.Model.Entity;
using src.Model.Dto;
using src.Data;
using Microsoft.EntityFrameworkCore;

namespace src
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
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Note, NoteDto>();
                cfg.CreateMap<NoteDto, Note>();

                cfg.CreateMap<Note, NoteDtoWithoutId>();
                cfg.CreateMap<NoteDtoWithoutId, Note>().ForMember(note => note.Id, opt => opt.Ignore());
            });
 
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();
            services.AddSingleton<IMapper>(sp => mapper);
            

            services.AddDbContext<MyContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql")));
            services.AddControllers();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
