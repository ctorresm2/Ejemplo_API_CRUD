using APIpruebas.Models;
using Microsoft.EntityFrameworkCore;
using APIpruebas.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Creando la conexion a la Base de datos, extrayendo la informacion de appsettings.json
builder.Services.AddDbContext<DbapiContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));



builder.Services.AddControllers().AddJsonOptions(opt => {
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    
});


var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();    
    });
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
