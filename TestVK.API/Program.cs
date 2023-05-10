using Microsoft.AspNetCore.Authentication.Negotiate;
using TestVK.API.BLL.Services;
using TestVK.API.DAL.Repositories;
using TestVK.API.DAL.Repositories.Contexts;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddDbContext<UserInfoDbContext>();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();