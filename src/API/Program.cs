using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using EasyRh.Domain;
using EasyRh.Application;
using EasyRh.Infra;
using EasyRh.Infra.DataAccess.Migrations;
using EasyRh.Infra.Extensions;
using EasyRh.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adicionando o ExceptionFilters como classe principal de erro do sistema
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

//Fazendo as injeções de dependencia
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfraestruture(builder.Configuration);
builder.Services.AddDomain(builder.Configuration);

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

MigrationDataBase();

app.Run();

void MigrationDataBase()
{
    var connectionString = builder.Configuration.ObtainConnectionString();

    //Criando um services scope pra pegar o services provider
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DataBaseMigration.Migrate(connectionString, serviceScope.ServiceProvider);
}
