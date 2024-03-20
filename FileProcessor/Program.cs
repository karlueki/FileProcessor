
using FileProcessor.Interface;
using FileProcessor.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "ApiKey";
        options.DefaultChallengeScheme = "ApiKey";
    })
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>("ApiKey", null);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiKeyPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add("ApiKey");
        policy.RequireAuthenticatedUser();
    });
});



builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor

// Register services using dictionary
builder.Services.AddSingleton<ICsvFileDetailProcessor, CSVFileDetailProcessor>();
builder.Services.AddSingleton<IJsonFileDetailProcessor,JsonFileDetailProcessor>();



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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
