var builder = WebApplication.CreateBuilder(args);

// Add DBContext to services
builder.Services.AddDbContext<ConsumerContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AppDb")));

// Add MassTransit with RabbitMQ as the message broker
builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<ConsumerContext>(o =>
    {
        o.UseSqlServer();

        o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "Dev", includeNamespace: false));

    var assembly = Assembly.GetEntryAssembly();

    x.AddConsumers(assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();