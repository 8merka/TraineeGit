using Microsoft.AspNetCore.Authentication.Negotiate;
using TraineeWEB.Data;
using TraineeWEB.Services;
using TraineeWEB.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Information("Starting application");
var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;


var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true).Build();
var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<EFFilmoSearchDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FilmoSearch")));
builder.Services.AddTransient<IFilmService, EFFilmService>();
builder.Services.AddTransient<IActorService, EFActorService > ();
builder.Services.AddTransient<IReviewService, EFReviewService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler("/Home/Error");
//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
