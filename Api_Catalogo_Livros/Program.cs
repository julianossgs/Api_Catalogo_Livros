using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Filters;
using Api_Catalogo_Livros.Logging;
using Api_Catalogo_Livros.Repositories;
using Api_Catalogo_Livros.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//habilitando o serviço de banco de dados(Banco de dados = APILivrariaDB)
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("LivrariaConnection"))
);

//habilitando o serviço p/ ignorar a referência cíclica
builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

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
