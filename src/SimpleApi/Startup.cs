using System.Collections.Generic;
using FluentValidation;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleAPI.Commands;
using SimpleAPI.Domain;
using SimpleAPI.PipelineBehaviors;
using SimpleAPI.Repositories;
using SimpleAPI.Repositories.Cached;
using SimpleAPI.Repositories.Interface;

namespace SimpleApi
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
            services.AddLazyCache();

            //AssemblyScanner.FindValidatorsInAssembly(typeof(Startup).Assembly)
            //    .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddSingleton<IRepository<Book>, BookRepository>();
            services.Decorate<IRepository<Book>, CachedBookRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAppCache cache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
