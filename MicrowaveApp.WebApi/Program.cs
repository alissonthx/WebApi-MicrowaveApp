using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:7292") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

//JWT Authentication
builder.Services.AddAuthentication()
.AddJwtBearer("main-scheme", jwtOptions =>
{
    jwtOptions.Authority = builder.Configuration["Api:Authority"];
    jwtOptions.Audience = builder.Configuration["Api:Audience"];
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, 
        ValidateAudience = false, 
        ValidateIssuerSigningKey = true,
        ValidAudiences = builder.Configuration.GetSection("Api:ValidAudiences").Get<string[]>(),
        ValidIssuers = builder.Configuration.GetSection("Api:ValidIssuers").Get<string[]>(),
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Api:Key"]!))
    };

    jwtOptions.MapInboundClaims = false;
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
