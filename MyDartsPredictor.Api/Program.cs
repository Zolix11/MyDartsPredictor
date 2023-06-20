using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));*/
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IUserSevice, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddScoped<IResultService, ResultService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/dotnethf-50e34";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/dotnethf-50e34",
            ValidateAudience = true,
            ValidAudience = "dotnethf-50e34",
            ValidateLifetime = true
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the JSON Web Token (JWT) in the 'Authorization' header using the Bearer scheme. \r\n\r\n" +
                      "Example: \"Bearer <your_token_here>\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
