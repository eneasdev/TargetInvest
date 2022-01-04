using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TargetInvest.Application.Interfaces;
using TargetInvest.Application.Services;
using TargetInvest.Domain.Repositories;
using TargetInvest.Infrastructure;
using TargetInvest.Infrastructure.Repositories;

namespace TargetInvest
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
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IVipService, VipService>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IVipRepository, VipRepository>();

            services.AddAutoMapper(typeof(Startup));

            var connectionString = Configuration.GetConnectionString("TargetString");
            services.AddDbContext<TargetContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddControllers();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TargetInvest", Version = "v1" });
            //});

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TargetInvest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
