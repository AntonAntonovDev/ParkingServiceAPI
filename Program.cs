using Microsoft.EntityFrameworkCore;
using ParkingServiceApi.Data;
using Serilog;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var logPath = Path.Combine("logs", environment, "log_.txt"); // ���������� �� ����� + ������ ��� ���
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.File(
        logPath,
        rollingInterval: RollingInterval.Day,   // ����� ����� ������ ����
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
    // ��������� ������ � ��������� ��������
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

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
