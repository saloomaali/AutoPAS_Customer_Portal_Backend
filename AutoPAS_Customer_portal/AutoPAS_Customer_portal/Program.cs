using AutoPAS_Customer_portal.Data;
using AutoPAS_Customer_portal.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocalDbContext>(options => 
options.UseMySQL(builder.Configuration.GetConnectionString("localConnctionString")));

builder.Services.AddDbContext<VMDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("vmConnectionString")));


builder.Services.AddScoped<CustomerPortalInterface, CustomerPortalRepository>();
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
