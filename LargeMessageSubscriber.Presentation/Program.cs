using LargeMessageSubscriber.Application;
using LargeMessageSubscriber.Domain.Settings;
using LargeMessageSubscriber.Infrastructure.DataAccess;
using LargeMessageSubscriber.Infrastructure.MessageBroker;
using LargeMessageSubscriber.Presentation.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddDataAccessInfrastructure();
builder.Services.AddMessageBrokerInfrastructure();
builder.Services.AddSwaggerGen();
builder.Services.Configure<InfluxDbSettings>(builder.Configuration.GetSection("InfluxDb"));
builder.Services.AddHostedService<RabbitMqMessageConsumer>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();