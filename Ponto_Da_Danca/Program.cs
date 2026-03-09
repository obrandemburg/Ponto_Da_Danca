using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi; // IMPORTANTE: No .NET 10 o ".Models" foi removido
using Scalar.AspNetCore;
using FluentValidation;
using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.Endpoints;
using Ponto_Da_Danca.Services;

var builder = WebApplication.CreateBuilder(args);

// === 1. CONFIGURAR AUTENTICAÇÃO JWT ===
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// ===  REGISTRAR FLUENTVALIDATION ===
// Isso varre o assembly atual e registra todas as classes que herdam de AbstractValidator
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// === 2. REGISTRAR O OPENAPI NATIVO (.NET 10) ===
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // 1. Cria a definição do esquema Bearer
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header
        };

        // 2. Adiciona o esquema aos componentes do documento
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["Bearer"] = jwtSecurityScheme;

        // 3. Aplica o requisito de segurança globalmente usando a nova classe de referência
        document.Security ??= new List<OpenApiSecurityRequirement>();
        document.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
        });

        return Task.CompletedTask;
    });
});

// === 3. CONFIGURAÇÃO DE BANCO E SERVIÇOS ===
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TurmaService>();

var app = builder.Build();

// === 4. ATIVAR A NOVA INTERFACE NO MODO DESENVOLVIMENTO ===
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

// Mapeamento dos Endpoints
app.MapAuthEndpoints();
app.MapUsuarioEndpoints(); 
app.MapTurmaEndpoints();

app.Run();