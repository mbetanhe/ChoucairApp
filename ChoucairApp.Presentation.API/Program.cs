
using ChoucairApp.Core.Application;
using ChoucairApp.Core.Application.Interfaces.Identity;
using ChoucairApp.Core.Application.Models;
using ChoucairApp.Core.Domain.Settings;
using ChoucairApp.Infrastructure;
using ChoucairApp.Infrastructure.Data;
using ChoucairApp.Infrastructure.Services;
using ChoucairApp.Presentation.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChoucairApp.Presentation.API
{
    public class Program
    {
        protected const string Politica_CORS = "Politica.Anonima";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region servicios
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ChoucairDbContext>();
            builder.Services.AddServicesFromApplication();
            builder.Services.AddServicesFromInfrastructure(builder.Configuration);
            builder.Services.AddSeedBdFromInfrastructure();
            #endregion

            #region JWT
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddScoped<IApplicationDbContextSeed, ApplicationDbContextSeed>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });
            #endregion

            #region Seguridad
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(Politica_CORS, conf =>
                {
                    conf.AllowAnyOrigin();
                    conf.AllowAnyMethod();
                    conf.AllowAnyHeader();
                });
            });
            #endregion

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI(options =>
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Choucair API V1");
                //});
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Choucair API V1");
            });

            app.UseHttpsRedirection();

            app.UseCors(Politica_CORS);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler();
            app.MapControllers();

            app.Run();
        }
    }
}
