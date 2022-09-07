using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTokenMS.helpers;
using TestTokenMS.Repository;
using TestTokenMS.Services;

namespace TestTokenMS
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
            //******8
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSingleton<IConfiguration>(provider => Configuration);
            var appSettingSection = Configuration.GetSection("AppSettings");
            // services.AddDbContext <userdbcontext>(options => options.UseSqlServer(@"Data Source=DESKTOP-NO86JCG;Initial Catalog=ganesha;Integrated Security=True"));

            // services.AddDbContext<userdbcontext>();//--options => options.UseSqlServer(Configuration.GetConnectionString("db_SECP_CMS_MS")));
            //services.AddDbContext<userdbcontext>();
            services.Configure<helperAppSettings>(appSettingSection);

            services.AddTransient<IAuthService, AuthService>();
            //RE
            services.AddHttpContextAccessor();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            //RE
            //*********

            services.AddControllers();


            //***********8
            services.AddTransient<IAuthService, AuthService>();
            services.AddHttpContextAccessor();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            var key = System.Text.Encoding.ASCII.GetBytes(appSettingSection.GetSection("Secret").Value);
            var sk = new SymmetricSecurityKey(key);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = sk,
                ValidateIssuer = true,
                ValidIssuer = "http://ScoriaIT/mainsubsys",
                ValidateAudience = true,
                ValidAudience = "http://ScoriaIT/subsys",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            services.AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = "TestKey";
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
             .AddJwtBearer(x =>
             {
                 x.SaveToken = true;
                 x.RequireHttpsMetadata = false;
                 x.TokenValidationParameters = tokenValidationParameters;

             });
            //***********8
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

            //app.UseAuthorization();
            ////***********Adding Authentication & Authorization 
            app.UseAuthentication();
            app.UseAuthorization();
            ////***********Adding Authentication & Authorization 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
