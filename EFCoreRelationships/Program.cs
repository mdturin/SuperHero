global using EFCoreRelationships.Data;
global using EFCoreRelationships.Interfaces;
global using EFCoreRelationships.Services;
global using EFCoreRelationships.Models;

using Microsoft.EntityFrameworkCore;
using EFCoreRelationships.Builder;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>( options =>
{
    var config = builder.Configuration;
    options.UseSqlServer( config.GetConnectionString( "DefaultConnection" ) );
} );

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<CharacterBuilder>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
