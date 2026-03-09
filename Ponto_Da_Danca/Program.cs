using Microsoft.EntityFrameworkCore;
using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.Endpoints;
using Ponto_Da_Danca.Services;
using PontoDaDanca.Endpoints;
using PontoDaDanca.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependência dos Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TurmaService>(); // <- Adicione o novo Service

var app = builder.Build();

// Mapeamento dos Endpoints
app.MapUsuarioEndpoints();
app.MapTurmaEndpoints(); // <- Adicione os novos endpoints

app.Run();