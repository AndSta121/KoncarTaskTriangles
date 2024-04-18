using KoncarTaskTriangles.Context;
using KoncarTaskTriangles.Domain;
using KoncarTaskTriangles.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KoncarTaskTriangles
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }
        public static string secret { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("sqlite"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITriangleRepository, TriangleRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITriangleService, TriangleService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddMvc();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthentication();


            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=User}/{action=Login}");
            });
        }
    }
}
