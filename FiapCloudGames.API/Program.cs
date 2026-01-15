using FiapCloudGames.Api.Extensions;
using FiapCloudGames.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddDatabase(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddApiDocumentation()
                .AddJWTConfig(builder.Configuration)
                .AddApiCors();
;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CorrelationIdMiddleware>()
    .UseMiddleware<ExceptionMiddleware>()
    .UseMiddleware<RequestLoggingMiddleware>()
    .UseMiddleware<JwtMiddleware>()
    .UseCors("DefaultCors")
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
