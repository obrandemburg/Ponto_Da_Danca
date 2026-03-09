using Microsoft.EntityFrameworkCore;
using PontoDaDanca.Data;
using PontoDaDanca.Endpoints;
using PontoDaDanca.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependência dos Services
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Mapeamento dos Endpoints
app.MapUsuarioEndpoints();

app.Run();