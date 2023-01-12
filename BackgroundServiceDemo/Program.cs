using BackgroundServiceDemo.Services;
using BackgroundServiceDemo.Services.InterfacesAndServices;
using BackgroundServiceDemo.ServiceSecond;
using BackgroundServiceDemo.ServiceSecond.InterfacesAndServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<MyBackgroundService>();
builder.Services.AddHostedService<MyBackgroundServiceSecond>();
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddSingleton<IBackgroundTaskQueueSecond, BackgroundTaskQueueSecond>();
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