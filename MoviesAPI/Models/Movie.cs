using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        [Required]
        public string MovieName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfRelease { get; set; }

        public string Plot { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 
        public Producer Producer { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Actor>? Actors { get; set; }
    }
}
