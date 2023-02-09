using AutoMapper;
using GeekShooping.CartApi.Config;
using GeekShooping.CartApi.Model.Context;
using GeekShooping.CartApi.Model.Repository;
using GeekShooping.CartApi.RabbitMQSender;
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
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// necessário está criado primeiro no final colocar no arquivo do program
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQCheckoutConsumer>(); 

builder.Services.AddControllers();

// para consumir um microserviço da aplicação
builder.Services.AddHttpClient<ICouponRepository, CouponRepository>(s => s.BaseAddress =
                new Uri(builder.Configuration["ServiceUrls:CouponAPI"]));

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeelShooping.CartApi" });
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

