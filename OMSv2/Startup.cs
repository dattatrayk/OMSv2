using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OMSv2
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
            // Enable CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:3000") // Replace with your actual front-end URL
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configure Entity Framework and SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configure JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtSettings:SecretKey"])),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"], // Set issuer
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtSettings:Audience"], // Set audience
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // Remove delay of token expiration
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            //if (!context.Response.HasStarted)
                            //{
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { message = "Authentication failed" });
                                return context.Response.WriteAsync(result);
                            //}
                        },
                        OnChallenge = context =>
                        {
                            //if (!context.Response.HasStarted)
                            //{
                                context.HandleResponse();
                                //context.Response.StatusCode = 401;
                                //context.Response.ContentType = "application/json";
                                var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { message = "You are not authorized" });
                                return context.Response.WriteAsync(result);
                            //}
                        }
                    };
                });

            // Add authorization services
            services.AddAuthorization();

            // Configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Initialize AppSettingProvider
            AppSettingProvider.Initialize(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
