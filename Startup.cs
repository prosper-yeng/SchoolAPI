using Microsoft.OpenApi.Models;
using SchoolAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SchoolAPI.Services;
using SchoolAPI.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;




namespace SchoolAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // ConfigureServices method is used to configure services required by the application.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
             // Default Policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://exaple1:44351", "http://example2:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var key = Encoding.ASCII.GetBytes(JWTHelper.MyKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

           

           services.AddControllers();
            // Register PasswordHasher for SchoolUser
            
            services.AddSwaggerGen(c =>
            {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApi", Version = "v1" });
            });
        }

        // Configure method is used to configure the application's request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Production error handling
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebApi v1");
            });

            // Add middleware components to the request pipeline.
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Configure routing, authentication, authorization, etc.
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            // Add CSP header
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self';");
                await next();
            });
          //app.UseCors("CorsPolicy"); // Apply CORS policy
          // with a named pocili
          // in general
            //app.UseCors();
            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }); 

           
          
           
        }
    }
}
