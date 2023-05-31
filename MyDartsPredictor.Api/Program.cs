using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IUserSevice, UserService>();

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseOpenApi();
app.UseSwaggerUi3();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();
