using Api.Extensions;
using Api.Middlewares;
using Aplication.Interfaces.Payments;
using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Aplication.Services;
using Aplication.UseCases.Auth;
using Aplication.UseCases.Billing;
using Aplication.UseCases.Subscriptions;
using Aplication.UseCases.Users;
using AspNetCoreRateLimit;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Stripe.Payments;
using Infrastructure.Stripe.Services;
using Infrastructure.Stripe.Webhooks;
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

builder.Services.AddScoped<ISubscriptionPaymentRecordService, SubscriptionPaymentRecordService>();
builder.Services.AddScoped<ISubscriptionPaymentRecordRepository, SubscriptionPaymentRecordRepository>();

builder.Services.AddScoped<AuthUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();

builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
builder.Services.AddScoped<IStripeCustomerService, StripeCustomerService>();

builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();

builder.Services.AddScoped<SubscribeUserUseCase>(); 
builder.Services.AddScoped<GetCustomerBillingPortalUrlUseCase>(); 

builder.Services.AddScoped<IStripeWebhookHandler, SubscriptionCreatedHandler>(); 
builder.Services.AddScoped<IStripeWebhookHandler, SubscriptionUpdatedHandler>();
builder.Services.AddScoped<IStripeWebhookHandler, SubscriptionDeletedHandler>(); 
builder.Services.AddScoped<IStripeWebhookHandler, InvoicePaymentSuccededHandler>(); 

builder.Services.AddScoped<IStripeBillingService, StripeBillingService>();
builder.Services.AddScoped<IStripeWebhookService, StripeWebhookService>();

builder.Services.AddScoped<IStripeWebhookFactory, WebhookHandlerFactory>();

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors("AllowFrontend");

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DataContext>();
db.Database.EnsureCreated();//create DB if not exist

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();
app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();  

app.MapControllers();

app.Run();
