
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterCommonServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    //app.UseSwagger();
    //app.UseSwaggerUI(c => {
    //    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Automais SmartTicket v1");
    //    c.RoutePrefix = "docs";
    //});
//}
app.UseSwagger();
app.UseSwaggerUI();

app.RegisterEndpointDefinitions("Automais SmartTicket");
app.UseHttpsRedirection();
//app.UseAuthorization();

app.Run();
