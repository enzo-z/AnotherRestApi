using Microsoft.EntityFrameworkCore;
using WebApiTest.Data;
using WebApiTest.Controllers;

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration.GetConnectionString("FilmesConnection");

// Add services to the container.
builder.Services.AddControllers();

//Add database context
var serverVersion = ServerVersion.AutoDetect(connString);

builder.Services.AddDbContext<MyAppDbContext>(opt =>
{
        opt.UseMySql(connString, serverVersion);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
