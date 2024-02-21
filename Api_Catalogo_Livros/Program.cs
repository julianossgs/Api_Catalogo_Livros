using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Filters;
using Api_Catalogo_Livros.Logging;
using Api_Catalogo_Livros.Repositories;
using Api_Catalogo_Livros.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {

        Version = "v1",
        Title = "ApiCatalogoLivros",
        Description = "Catálogo de Livros",
        TermsOfService = new Uri("https://julianossgs.github.io"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "JP Sistemas",
            Email = "julianosgs@yahoo.com.br",
            Url = new Uri("https://julianossgs.github.io")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://julianossgs.github.io")
        }
    });

    {
        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
    }
});

//habilitando o serviço de banco de dados(Banco de dados = APILivrariaDB)
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("LivrariaConnection"))
);

//habilitando o serviço p/ ignorar a referência cíclica
builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

//habilitando o serviço p/ filtro de exceção global
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
});


//habilitando os serviços do Repository
builder.Services.AddScoped<IAutoresRepository, AutoresRepository>();
builder.Services.AddScoped<IEditorasRepository, EditorasRepository>();
builder.Services.AddScoped<ILivrosRepository, LivrosRepository>();

//habilitando o serviço de log
builder.Services.AddScoped<ApiLogginFilter>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));


////////////////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
