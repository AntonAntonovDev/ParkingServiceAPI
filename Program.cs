using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingServiceApi.Data;
using Serilog;
//using static System.Runtime.InteropServices.JavaScript.JSType;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var logPath = Path.Combine("logs", environment, "log_.txt"); // подкаталог по среде + шаблон для дат
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.File(
        logPath,
        rollingInterval: RollingInterval.Day,   // новые файлы каждый день
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ParkingServiceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(cfg =>
{
    // Добавляем сборки с профилями маппинга
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cfg =>
    {
        cfg.WithOrigins(builder.Configuration["AllowedOrigins"]);
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });
    options.AddPolicy(name: "AnyOrigin",
    cfg =>
    {
        cfg.AllowAnyOrigin();
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapGet("/error", 
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore = true)]    () => 
    Results.Problem());
app.MapGet("/error/test",
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore = true)]    () => 
    { throw new Exception("Test"); });
//app.MapGet("/cod/test",
//    [EnableCors("AnyOrigin")]
//[ResponseCache(NoStore = true)] () =>
//    Results.Content("<script>" +
//"window.alert('Your client support JavaScript!" +
//    "\\r\\n\\r\\n" +
//    $"Server time: {DateTime.UtcNow.ToString("o")}" +
//    "</script>" +
//    "<noscript> Your ckient does not support JavaScript</noscript>",
//    "text/html"));
app.MapGet("/cod/test",
    [EnableCors("AnyOrigin")]
[ResponseCache(NoStore = true)]
() =>
    {
        var serverTime = DateTime.UtcNow.ToString("o");

        var html = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>JS Test</title>
</head>
<body>
    <script>
        // Серверное время вставлено как строка
        const serverTime = '{serverTime}';
        // Клиентское вычисление
        const clientTime = new Date().toISOString();

        window.alert(`Your client supports JavaScript!

Server time: ${serverTime}
Client time: ${{clientTime}}`);
    </script>
    <noscript>Your client does not support JavaScript</noscript>
</body>
</html>";

        return Results.Content(html, "text/html; charset=utf-8");
    });
app.MapControllers();

app.Run();
