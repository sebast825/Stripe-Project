using Api.Extensions;
using Aplication.Interfaces.Payments;
using Aplication.Interfaces.Services;
using Aplication.Services;
using Aplication.UseCases.Auth;
using Aplication.UseCases.Subscriptions;
using AspNetCoreRateLimit;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Payments;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

//dbcontext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository > ();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserLoginHistoryRepository, UserLoginHistoryRepository>();
builder.Services.AddScoped<ISecurityLoginAttemptRepository,SecurityLoginAttemptRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService,JwtService> ();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddSingleton<IEmailAttemptsService,EmailAttemptsService> ();
builder.Services.AddScoped<IUserLoginHistoryService, UserLoginHistoryService>();
builder.Services.AddScoped<ISecurityLoginAttemptService, SecurityLoginAttemptService>();

builder.Services.AddScoped<AuthUseCase>();

builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();

builder.Services.AddScoped<SubscribeUserUseCase>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//--------------------------EXTENSIONS----------------------------------------
builder.Services.AddIpRateLimit(builder.Configuration);
//-----------------------------JWT--------------------------------------------
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerJwt(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DataContext>();
db.Database.EnsureCreated();//create DB if not exist

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();  

app.MapControllers();

app.Run();
