using LargeMessageSubscriber.Application;
using LargeMessageSubscriber.Domain.Settings;
using LargeMessageSubscriber.Infrastructure.DataAccess;
using LargeMessageSubscriber.Infrastructure.MessageBroker;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddDataAccessInfrastructure();
builder.Services.AddMessageBrokerInfrastructure();
builder.Services.AddSwaggerGen();
builder.Services.Configure<InfluxDbSettings>(builder.Configuration.GetSection("InfluxDb"));
//builder.Services.AddOptions<InfluxDbSettings>().Bind(builder.Configuration.GetSection("InfluxDb")).ValidateDataAnnotations();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
