using Serilog;
using Serilog.Enrichers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHeaderPropagation(options => options.Headers.Add("my-custom-correlation-id"));
// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.WithCorrelationIdHeader("my-custom-correlation-id");
});

builder.Services.AddControllers();
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

app.UseSerilogRequestLogging();  //Log the Request information also
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
