using JWTAuthentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
       .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
       .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddJWTExtention();
//var baseAuthenticationProviderKey = "Bearer";
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})

//    // Adding Jwt Bearer  
//    .AddJwtBearer(baseAuthenticationProviderKey, options =>
//    {
//        options.SaveToken = true;
//        options.RequireHttpsMetadata = false;
//        options.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidateLifetime = true,
//            ValidAudience = "ValidAudience",
//            ValidIssuer = "ValidIssuer ",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IssuerSigningKey"))
//        };
//    });
var app = builder.Build();

await app.UseOcelot();
app.UseAuthentication();
app.UseAuthorization();
app.Run();