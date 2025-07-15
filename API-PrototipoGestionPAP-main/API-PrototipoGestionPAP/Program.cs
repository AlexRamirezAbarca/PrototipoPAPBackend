using API_PrototipoGestionPAP.Controllers;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Services.Mantenedores;
using API_PrototipoGestionPAP.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UsuariosController>();
builder.Services.AddScoped<PersonasController>();
builder.Services.AddScoped<PermisosController>();

//Inicio Mantenedores
builder.Services.AddScoped<EjeObjetivoPNService>();
builder.Services.AddScoped<ObjetivoPoliticaPNService>();
builder.Services.AddScoped<ObjetivoMetaPNService>();
//Fin Mantenedores

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy
            .WithOrigins("https://web-app-prototipogestionpap20250108223848.azurewebsites.net", "http://localhost:5278")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configurar la autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// Configuración de Swagger con soporte para JWT
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Prototipo Gestión PAP",
        Version = "v1"
    });

    // Definir el esquema de seguridad para JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese 'Bearer' [espacio] y el token JWT.\r\nEjemplo: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Requerir el token para cada endpoint (opcional)
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Usar la política CORS configurada
app.UseCors("MyCorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Prototipo Gestión PAP v1");
});
app.UseDeveloperExceptionPage();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
