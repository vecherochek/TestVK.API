using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Services;
using TestVK.API.DAL.Repositories;
using TestVK.API.DAL.Repositories.Contexts;
using TestVK.API.Middlewaries;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
var connections = builder.Configuration.GetConnectionString("userDbConnection");
//var connections = builder.Configuration.GetConnectionString("userDbConnection_docker_compose");

services.AddDbContext<UserInfoDbContext>(o => o.UseNpgsql(connections));

services.AddTransient<UserRepository>();
services.AddTransient<UserStateRepository>();
services.AddTransient<UserGroupRepository>();
services.AddTransient<UserService>();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();