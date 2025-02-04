using ClassroomAPI;
using ClassroomAPI.Services;
using FluentAssertions.Common;
using System;

var builder = WebApplication.CreateBuilder(args);

//Registo da Interface e do Servi�o dos �lunos
builder.Services.AddScoped<IStudentService, StudentsService>();
builder.Services.AddDbContext<MyDbContext>();

// Add services to the container.

builder.Services.AddControllers();
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

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
