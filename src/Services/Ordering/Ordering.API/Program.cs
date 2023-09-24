using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.API.Extensions;
using Ordering.Infrastructure.Persistence;
using MassTransit;
using EventBus.Messages.Events;
using EventBus.Messages.Common;
using Ordering.API.EventBaseConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckOutQueue, c =>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);

        });
    });
});
//General configuration
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();

var app = builder.Build();
app.MigrateDatabase<OrderContext>((context, service) =>
{
    var logger = service.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed
                .SeedAsync(context, logger)
                .Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseAuthorization();

app.MapControllers();

app.Run();
