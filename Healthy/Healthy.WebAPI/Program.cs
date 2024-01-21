using Healthy.Business.Services.Definitions;
using Healthy.Business.Services.Implementations;
using Healthy.Business.Specifications.Definitions;
using Healthy.Business.Specifications.Implementations;
using Healthy.Business.UseCases.Definitions;
using Healthy.Business.UseCases.Implementations;
using Healthy.Infraestructure;
using Healthy.Infraestructure.Repository.Definitions;
using Healthy.Infraestructure.Repository.Implementations;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddTransient<IEditUserUseCase, EditUserUseCase>();
builder.Services.AddTransient<IDeleteUserUseCase, DeleteUserUseCase>();
builder.Services.AddTransient<IUserSpecification, UserSpecification>();
builder.Services.AddTransient<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddTransient<IGetUserByIdUseCase, GetUserByIdUseCase>();
builder.Services.AddTransient<IServiceUsers, ServiceUsers>();
builder.Services.AddScoped<IUserRepostory, UserRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Healthy.Business")));

builder.Services.AddDbContext<HealthyDbContext>(option => 
option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")), ServiceLifetime.Scoped);

//builder.Services.AddDbContext<HealthyDbContext>(option => 
//option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")), ServiceLifetime.Scoped);
builder.Services.AddCors((option) =>
{
    option.AddPolicy("AllowCors", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowCors");
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
