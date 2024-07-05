
using ChoucairApp.Core.Application;
using ChoucairApp.Core.Application.Interfaces.Identity;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Models;
using ChoucairApp.Infrastructure;
using ChoucairApp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ChoucairApp.Core.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ChoucairApp.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChoucairApp.Presentation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region servicios
            builder.Services.AddServicesFromApplication();
            builder.Services.AddServicesFromInfrastructure(builder.Configuration);
            #endregion

            #region JWT
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ChoucairDbContext>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
