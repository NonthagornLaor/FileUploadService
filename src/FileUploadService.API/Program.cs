using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Serilog;
using JWTAuthentication;
using Serilog.Events;
using Sendmail.Models;
using Sendmail;
using Sendmail.Interface;

var builder = WebApplication.CreateBuilder(args);

string environmentName = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile(Path.Combine("appsettings.json"), false, true);
builder.Configuration.AddJsonFile(Path.Combine($"appsettings.{environmentName}.json"), true, true);


var _logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Debug()
        .CreateLogger();
builder.Logging.ClearProviders();
Log.Information("---------------- - Starting Program---------------------");
builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddJWTExtention();
var _emailconfig = builder.Configuration.GetSection("EmailConfig").Get<EmailConfig>();
builder.Services.AddSingleton(_emailconfig);
//var _emailconfig  = builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
//builder.Services.AddSingleton(_emailconfig);
builder.Services.AddScoped<ISendmailAsync, SendmailAsync>();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
