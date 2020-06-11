using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Recipe.Services.Auth;
using Recipe.Services.Mapping;
using Recipe.Data;

namespace Recipe.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<RecipeContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("RecipeConnection")
                ));
            services.AddDbContext<AuthContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("RecipeConnection")
                ));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddSwaggerDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Recipes API";
                };
            });

            if (HostEnvironment.IsProduction())
            {
                services.AddTransient<IAuthenticationService, AuthenticationService>();
                
                var key = Configuration.GetValue<string>("token::secret");
                
                services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseOpenApi();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipes API");
            });
        }
    }
}