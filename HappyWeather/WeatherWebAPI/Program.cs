using dotenv.net;
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
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

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