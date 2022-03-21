using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }

        [Required]
        public string ActorName { get; set; }

        public string Bio { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, ItemReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
        public ICollection<Movie>? Movies { get; set; }
    }
}
