using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using EmailService.Application.Email.Queries;
using EmailService.Application.Implementation;
using EmailService.Application.Interfaces;
using EmailService.Application.MappingsProfile;
using EmailService.Domain.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreSystem
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
        {//options.UseSqlServer("Data Source=(local);Initial Catalog=EmailService; Persist Security Info=True;Integrated Security=SSPI;")); //
            services.AddDbContext<EmailDbContext>(options => options.UseInMemoryDatabase("EmailDb"));
            services.AddMediatR(typeof(GetAllEmailsQueryHandler).GetTypeInfo().Assembly);
            services.AddScoped<IContextProvider, EmailDbContextProvider>();
            services.AddScoped<IEmailService, EmailService.Application.Implementation.EmailService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EmailMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddControllers();
            services.AddSwaggerGen();

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

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
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
