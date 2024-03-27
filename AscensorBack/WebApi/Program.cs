using Business.Interfaces;
using Business.Services;
using Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IElevatorService, ElevatorService>();
builder.Services.AddScoped<IElevatorJob, ElevatorJob>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.WithOrigins("https://localhost:7076", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
