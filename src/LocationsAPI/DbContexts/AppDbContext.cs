using System.Text.Json;
using LocationsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LocationsAPI.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserLocation> UserLocations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            var stringListConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, options),
                v => JsonSerializer.Deserialize<List<string>>(v, options) ?? new List<string>()
            );

            modelBuilder
                .Entity<UserLocation>()
                .Property(ul => ul.PictureGuids)
                .HasConversion(stringListConverter)
                .HasColumnType("json");

            modelBuilder.Entity<UserLocation>().ToTable("UserLocations");
        }
    }
}
