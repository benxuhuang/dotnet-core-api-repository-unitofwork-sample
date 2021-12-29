using MyMusic.Core;
using MyMusic.Data;
using Microsoft.EntityFrameworkCore;
using MyMusic.Services;
using MyMusic.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure the connection of database
var connStr = builder.Configuration.GetValue<string>("ConnectionStrings:Default");
builder.Services.AddDbContext<MyMusicDbContext>(options =>
 options.UseSqlServer(connStr,
  x => x.MigrationsAssembly("MyMusic.Data")));

// Configure Dependency injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMusicService, MusicService>();
builder.Services.AddScoped<IArtistService, ArtistService>();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
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
