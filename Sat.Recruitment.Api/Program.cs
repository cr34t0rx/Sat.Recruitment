using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Api.Databases;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Database;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Services;

var builder = WebApplication.CreateBuilder(args);

//automapper
builder.Services.AddAutoMapper(typeof(Program));
var mapper = new MapperConfiguration(mc =>
{
    mc.CreateMap<User, UserModel>();
    mc.CreateMap<UserModel, User>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);

//db context
builder.Services.AddDbContext<UserDbContext>(o =>
{
    o.UseSqlite(builder.Configuration["ConnectionStrings:UserDatabase"]);
});

//repository and service
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
