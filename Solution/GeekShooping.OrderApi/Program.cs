using AutoMapper;
using GeekShooping.CartApi.Model.Repository;
using GeekShooping.OrderApi.Context;
using GeekShooping.OrderApi.MessageConsumer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MySql
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
// necessário criar primeiro na model MySqlContext
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 15))));
// necessário criar primeiro na Config AutoMapper
// para trasformar VO em mapper

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

//  AddSingleton vai rodar MySQL uma vez só garantido que não vai salvar duas vezes.
var builderConnectringString = new DbContextOptionsBuilder<MySQLContext>();
builderConnectringString.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 15)));

builder.Services.AddSingleton(new OrderRepository(builderConnectringString.Options));
builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();
builder.Services.AddControllers();

// adicionando depois que identity server estive pronto

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        //pegar a url no projeto identity server 
        options.Authority = "https://localhost:4435";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };

    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        // é necessario pegar configuraion Scope do Identity Server
        policy.RequireClaim("scope", "geek_shopping");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// definido os requisitos de segurança 
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeelShooping.OrderApi" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token!",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
            In = ParameterLocation.Header
        },
        new List<string> ()
        }
    });
});
// Add services to the container.
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


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

