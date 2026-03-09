using Microsoft.EntityFrameworkCore;
using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.Endpoints;
using Ponto_Da_Danca.Services;
using Ponto_Da_Danca.Endpoints;
using Ponto_Da_Danca.Services;

var builder = WebApplication.CreateBuilder(args);

// === 1. REGISTRAR O SWAGGER ===
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do Banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependência dos Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TurmaService>();

var app = builder.Build();

// === 2. ATIVAR O SWAGGER NO AMBIENTE DE DESENVOLVIMENTO ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapeamento dos Endpoints
app.MapUsuarioEndpoints();
app.MapTurmaEndpoints();

app.Run();