using MyMusic.Core;
using MyMusic.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// var provider = builder.Services.BuildServiceProvider();
// var configuration = provider.GetService<IConfiguration>();
var connStr = builder.Configuration.GetValue<string>("ConnectionStrings:Default");

builder.Services.AddDbContext<MyMusicDbContext>(options =>
 options.UseSqlServer(connStr,
  x => x.MigrationsAssembly("MyMusic.Data")));

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
