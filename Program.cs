global using SimpleEmailApp.Model;
global using SimpleEmailApp.Services;
using SimpleEmailApp.ConfgureSetting;
using Serilog;
using SimpleEmailApp.CorrelationService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

//builder.Host.UseSerilog();
var Logger = new LoggerConfiguration()
     .ReadFrom.Configuration(builder.Configuration)
     .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Logger);



builder.Services.AddSwaggerGen();

/// <summary>
/// /below confiure method is add appsettings.development.json values to  appsettings.json and 
/// configure host(smtp server settings) for us .
/// also congigure method is made by mailsetting c# class properties and fill with mailsetting in 
/// appsettings.development.json file 
/// we can use appsettings.json instead appsettings.development.json .
/// </summary>
//builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("MailSetting"));

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("MailSetting"));

builder.Services.ConfigureWritable<AppSetting>(builder.Configuration.GetSection("MailSetting"));

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("MailSetting",
                       optional: true,
                       reloadOnChange: true);
});

builder.Services.AddScoped<IMailService, MailService>();

/// <summary>
/// correlation Id
/// </summary>

//builder.Services.AddCorrelationIdGeneratorService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddCorrelationIdMiddleware();



app.MapControllers();

app.Run();
