using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LocationsAPI.Models
{
    public class UserLocation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? Description { get; set; }

        // Store PictureGuids as a JSON string in MySQL
        [Column(TypeName = "json")]
        public List<string> PictureGuids { get; set; } = new();
    }
}
