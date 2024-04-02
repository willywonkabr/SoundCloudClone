using Microsoft.EntityFrameworkCore;
using SoundCloudClone.API;
using SoundCloudClone.Application.Conta;
using SoundCloudClone.Repository;
using SoundCloudClone.Repository.Conta;
using SoundCloudClone.Repository.Streaming;
using SoundCloudClone.Repository.Transacao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("musicaApiServer", client =>
{
    client.BaseAddress = new Uri("http://localhost:8080");
}).AddPolicyHandler(RetryPolicyConfiguration.GetRetryPolicy());

builder.Services.AddDbContext<SoundCloudContext>(database =>
{
    database.UseInMemoryDatabase("SoundCloudDatabase");
});

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<BandaRepository>();
builder.Services.AddScoped<PlanoRepository>();

builder.Services.AddScoped<UsuarioService>();

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
