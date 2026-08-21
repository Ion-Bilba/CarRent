using CarRent.API.Middlewares;
using CarRent.Application;
using CarRent.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(
             new JsonStringEnumConverter());
     });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//https://localhost:7088/swagger

var app = builder.Build();


if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Run();