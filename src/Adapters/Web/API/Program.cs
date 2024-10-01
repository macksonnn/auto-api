using AutoMais.Core.Common.Startup;
using AutoMais.Core.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterCommonServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterEndpointDefinitions(); //TODO: To be defined how to implement this in the future
app.UseHttpsRedirection();
//app.UseAuthorization();

app.Run();
