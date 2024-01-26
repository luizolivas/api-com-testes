using Microsoft.EntityFrameworkCore;
using introApiWeb.Models;
using introApiWeb.Contexts;
using introApiWeb.Services;
using System.Configuration;
using introApiWeb.RabbitMQ;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<AppDBContext>(opt =>
//    opt.UseInMemoryDatabase("ListaPessoa"));
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer("Data Source=DESKTOP-1NDP0IE; Initial Catalog=teste_api;Integrated Security=False;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;Trusted_Connection=True"));


builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ProdutoPedidoService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
