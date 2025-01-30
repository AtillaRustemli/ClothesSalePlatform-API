using ClothesSalePlatform;

var builder = WebApplication.CreateBuilder(args);
// Get the PORT from the environment variable (used by Render)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

// Configure the app to listen on this port
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
var config = builder.Configuration;
builder.Services.Register(config);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name");
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
