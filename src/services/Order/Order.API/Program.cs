using Microsoft.OpenApi.Models;
using Order.Infra;
using Order.Services;


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
builder.Services.AddServiceModules();
builder.Services.AddInfraModules();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
