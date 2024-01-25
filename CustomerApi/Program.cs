using Azure.Core;
using Azure;
using CustomerApi.Casos_de_Usos;
using CustomerApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing=>routing.LowercaseUrls=true);

builder.Services.AddDbContext<CustomersDBContext>(sqlserverbuilder =>
sqlserverbuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUpdateCustomerUseCase,UpdateCustomerUseCase>(); //las dependencias scoped son aquellas que se instancia cada vez que se hace un request

//"Server=GWTN141-10\\MANUDESKTOP;Database=Entity_Framework_ProyectoUno;Integrated Security=True;TrustServerCertificate=True"
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthorization();

//usar esto para el Cors
app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());





app.MapControllers();





app.Run();
