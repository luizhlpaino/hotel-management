using Application.Guest;
using Application.Ports.Guest;
using Application.Ports.Room;
using Application.Room;
using Data;
using Data.Guest;
using Data.Room;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region IoC
builder
    .Services
        .AddScoped<IGuestManager, GuestManager>();
builder
    .Services
        .AddScoped<IGuestRepository, GuestRepository>();
builder
    .Services
        .AddScoped<IRoomManager, RoomManager>();
builder
    .Services
        .AddScoped<IRoomRepository, RoomRepository>();
#endregion

#region DB Wiring Up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder
    .Services
        .AddDbContext<HotelDbContext>((
            options => options.UseNpgsql(connectionString)
        ));
#endregion

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
