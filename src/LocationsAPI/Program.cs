using LocationsAPI.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                sqliteOptions =>
                    sqliteOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery) // Use split query for better performance
            )
        );

        // Add services to the container.
        builder
            .Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        // Add CORS
        builder.Services.AddCors();

        var app = builder.Build();

        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.MapControllers();

        app.Run();
    }
}
