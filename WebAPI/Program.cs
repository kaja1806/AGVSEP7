using System.Data.SqlClient;
using Database.SQLHelper;
using WebAPI.Controllers;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add configuration settings
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<SqlConnection>();
builder.Services.AddScoped<SqlConnectionClass>();
builder.Services.AddScoped<IStatorService, StatorService>();
builder.Services.AddScoped<ISegmentService, SegmentService>();
builder.Services.AddScoped<ICalculationResultService, CalculationResultService>();
builder.Services.AddScoped<IAgvService, AgvService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<FileContext>();

var app = builder.Build();

//CORS added for webAPI

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());


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