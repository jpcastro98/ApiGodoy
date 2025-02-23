using System.Text;
using System.Text.Json.Serialization;
using ApiGodoy.Database;
using ApiGodoy.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configurar Base de Datos
builder.Services.AddDbContext<ApiDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectingtoSQLServer"));
});

// Inyección de Dependencias
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(UserService));
builder.Services.AddScoped(typeof(UserDataService));
builder.Services.AddScoped(typeof(SessionHistoryService));
builder.Services.AddScoped(typeof(AuthService));


builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", builder =>
    {
        builder.WithOrigins("https://localhost:4200")
               .AllowCredentials()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
var validAudiences = jwtSettings.GetSection("Audience").Get<string[]>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudiences = validAudiences,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("jwtToken"))
                {
                    context.Token = context.Request.Cookies["jwtToken"];
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CORS"); 
app.UseAuthentication(); 
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.UseSwagger();

app.Run();
