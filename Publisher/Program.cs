var builder = WebApplication.CreateBuilder(args);

// Add DBContext to services
builder.Services.AddDbContext<PublisherContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AppDb")));

// Add MassTransit with RabbitMQ as the message broker
builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "Dev", includeNamespace: false));

    x.AddEntityFrameworkOutbox<PublisherContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(1);

        o.UseSqlServer();
        o.UseBusOutbox();
    });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.AutoStart = true;
        
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPublishingGroup();

app.Run();