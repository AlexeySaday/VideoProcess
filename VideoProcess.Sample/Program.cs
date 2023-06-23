using VideoProcess.Sample.Extensions;

var builder = WebApplication
    .CreateBuilder(args)
    ;

builder.Configuration.AddJsonFile("Settings.json");

// Add services to the container.

builder.Services
    .AddOptions()
    .AddLogging()
    .AddHttpContextAccessor() 
    .AddCors()
    .AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDiskApi(builder.Configuration.GetSection("YandexDisk"));

var app = builder.Build();

app.UseCors(policyBuilder => policyBuilder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);  

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
