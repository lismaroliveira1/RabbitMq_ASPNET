using MessageBroker.EventBus;
using MessageBroker.EventBus.Interfaces;
using MessageBroker.EventBus.Producer;
using Microsoft.OpenApi.Models;
using Order.Infra;
using Order.Services;
using RabbitMQ.Client;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ctx =>  {
    ctx.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Challenge API",
        Version = "v1",
        Description = "The is API is builded for win the challenge",
        Contact = new OpenApiContact {
            Name = "Lismar Oliveira",
            Email ="englismarOliveira@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/lismar-oliveira-9a93ba94/")
        }
    });
    
    ctx.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {    
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
            },
            new List<string>()
          }
        });
});
builder.Services.AddControllers();
builder.Services.AddSingleton<IBusConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["BusConnection:HostName"]

    };
    factory.AutomaticRecoveryEnabled = true;
    factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(5);
    factory.TopologyRecoveryEnabled = true;

    if (!string.IsNullOrWhiteSpace(builder.Configuration["BusConnection:UserName"]))
        factory.UserName = builder.Configuration["BusConnection:UserName"];

    if (!string.IsNullOrWhiteSpace(builder.Configuration["BusConnection:Password"]))
        factory.Password = builder.Configuration["BusConnection:Password"];
    var retryCount = 10;

    if (!string.IsNullOrWhiteSpace(builder.Configuration["BusConnection:RetryCount"]))
        retryCount = int.Parse(builder.Configuration["BusConnection:RetryCount"]);

    return new BusConnection(factory, retryCount);
});
builder.Services.AddServiceModules();
builder.Services.AddInfraModules();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
   app.UseSwaggerUI(c =>
    {
    	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Motorcycle V1");
    	c.RoutePrefix = "order/docs";
    });

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
