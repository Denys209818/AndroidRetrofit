using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RetrofitLesson.WEB.Data;
using RetrofitLesson.WEB.Data.Identity;
using RetrofitLesson.WEB.Mappers;
using RetrofitLesson.WEB.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AccountMapper));
builder.Configuration.AddUserSecrets<EFContext>();

builder.Services.AddDbContext<EFContext>((DbContextOptionsBuilder optsBuilder) => {
    optsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>((IdentityOptions opts) => { 
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<EFContext>()
    .AddDefaultTokenProviders();

string pr_key = builder.Configuration.GetSection("private_key").Value;

builder.Services.AddAuthentication((AuthenticationOptions opts) => {
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer((JwtBearerOptions opts) => {
        opts.SaveToken = true;
        opts.RequireHttpsMetadata = false;
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(pr_key))
        };
    });

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors((CorsPolicyBuilder opts) => {
    opts.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});
app.UseAuthorization();

app.MapControllers();

app.SeedAll();

app.Run();
