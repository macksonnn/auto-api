using AutoMais.Ticket.Api.Extensions;
using Core.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterServices();

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

//Register the default Core error handling middleware
app.UseLoggingMiddleware();
app.RegisterEndpointDefinitions();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseErrorMiddleware();

app.Run();
