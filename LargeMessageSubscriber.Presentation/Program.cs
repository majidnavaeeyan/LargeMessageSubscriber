using LargeMessageSubscriber.Application;
using LargeMessageSubscriber.Domain.Settings;
using LargeMessageSubscriber.Infrastructure.DataAccess;
using LargeMessageSubscriber.Infrastructure.MessageBroker;
using LargeMessageSubscriber.Presentation.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddDataAccessInfrastructure();
builder.Services.AddMessageBrokerInfrastructure();
builder.Services.Configure<InfluxDbSettings>(builder.Configuration.GetSection("InfluxDb"));
builder.Services.AddHostedService<RabbitMqMessageConsumer>();
builder.Services.AddSwaggerGen(q =>
{
  q.AddSecurityDefinition("Bearer",
       new OpenApiSecurityScheme
       {
         In = ParameterLocation.Header,
         Name = "Authorization",
         Type = SecuritySchemeType.ApiKey,
       });


  q.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer" }
                            }, new List<string>() }
                    });
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();