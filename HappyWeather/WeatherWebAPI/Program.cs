using dotenv.net;
using WeatherWebAPI.Middlewares;
using WeatherWebAPI.Services.WeatherService;

namespace WeatherWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotEnv.Load();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.WithOrigins("https://happyweathers.netlify.app", "http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            builder.Services.AddScoped<ExceptionHandlerMiddleware>();
            builder.Services.AddMemoryCache();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthorization();
            app.UseCors(o => o.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}