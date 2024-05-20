using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Data.Sqlite;
using System.Data;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contâiner.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Adiciona o singleton para a conexão com o banco de dados
builder.Services.AddSingleton<IDbConnection>(sp => new SqliteConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona os handlers para os comandos de movimento e consulta de saldo
builder.Services.AddScoped<IRequestHandler<MovimentoRequest, MovimentoResponse>, MovimentoHandler>();
builder.Services.AddScoped<IRequestHandler<SaldoRequest, SaldoResponse>, SaldoHandler>();

// Adiciona o SwaggerGen para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Contas Correntes", Version = "v1" });
});

var app = builder.Build();

// Configuração do pipeline da request HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
#pragma warning disable CS8602
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 

app.Run();