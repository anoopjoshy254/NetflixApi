using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetflixApi;
using NetflixApi.Modules.Auth.Services;
using NetflixApi.Modules.Users.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with JWT Support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Netflix API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

// Configure DbContext with MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "server=localhost;database=netflix;user=root;password=root;";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "NetflixApi",
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "NetflixApi",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? builder.Configuration["Jwt:Secret"] ?? "SuperSecretKeyForNetflixCloneBackend12345!"))
    };
});

builder.Services.AddAuthorization();

// DI for Users Module
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

// DI for Auth Module
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();
builder.Services.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Team Member 3 Modules (Existing)
builder.Services.AddScoped<NetflixApi.Modules.Streaming.Services.IStreamingService, NetflixApi.Modules.Streaming.Services.StreamingService>();
builder.Services.AddScoped<NetflixApi.Modules.Streaming.Services.IHistoryService, NetflixApi.Modules.Streaming.Services.HistoryService>();
builder.Services.AddScoped<NetflixApi.Modules.Streaming.Services.IDownloadService, NetflixApi.Modules.Streaming.Services.DownloadService>();
builder.Services.AddScoped<NetflixApi.Modules.Watchlist.Services.IWatchlistService, NetflixApi.Modules.Watchlist.Services.WatchlistService>();
builder.Services.AddScoped<NetflixApi.Modules.Reviews.Services.IReviewService, NetflixApi.Modules.Reviews.Services.ReviewService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add Authentication and Authorization to the pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
