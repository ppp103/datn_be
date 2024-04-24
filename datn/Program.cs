using datn.API;
using datn.Application;
using datn.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => new
            {
                Exception = x.Exception,
                ErrorMessage = x.ErrorMessage
            });

        var baseException = new
        {
            ErrorCode = 400,
            UserMessage = errors.FirstOrDefault()?.ErrorMessage,
            DevMessage = errors.FirstOrDefault()?.ErrorMessage,
            TraceId = "",
            MoreInfo = "",
            Errors = errors
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(baseException, jsonOptions);
        var jsonString = Encoding.UTF8.GetString(jsonBytes);

        return new BadRequestObjectResult(jsonString);
    };
});
// add layer dependency
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
