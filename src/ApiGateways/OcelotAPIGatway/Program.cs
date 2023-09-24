using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddOcelot();
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true,true);
var app = builder.Build();
app.UseRouting();


//app.MapGet("/", () => "Hello World!");
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});
await app.UseOcelot();
app.Run();
