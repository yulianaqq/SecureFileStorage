using Microsoft.EntityFrameworkCore;
using SecureFileStorage.Data;
using SecureFileStorage.Repositories;
using SecureFileStorage.Repositories.Interfaces;
using SecureFileStorage.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<FileService>();


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();