using BitstampSimulador.Application.Simulacoes;
using BitstampSimulador.Domain.Interfaces;
using BitstampSimulador.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IServicoSimulacao, ServicoSimulacao>();
builder.Services.AddSingleton<MongoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
