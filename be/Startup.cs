using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lavax711.Core;
using lavax711.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace lavax711
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
            services.AddMvc(
                options =>{
                    options.Filters.Add(new ExceptionAttribute());
                }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            #if DEBUG
            app.UseCors(builder =>{
                builder.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
            });
            #endif

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.Use(async(context,next) =>
            {
                if(!context.Request.Path.ToString().StartsWith("/api/passport"))
                {
                    var _token ="";
                    if(context.Request.Headers.TryGetValue("token",out var tokens) && tokens.Count > 0)
                    {
                        _token = tokens[0];
                    }
                    if(_token != "123456789")
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }
                    
                    var user = new User();
                    user.id=1;
                    user.name="lavax";
                    context.Items.Add("token",_token);
                    context.Items.Add("user",user);
                }
                await next();
            });
            
            app.UseMvc();
        }
    }
}
