using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    public class Producer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProducerId { get; set; }

        [Required]
        public string ProducerName { get; set; }

        public string Bio { get; set; }

        public string Company { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, ItemReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
        public ICollection<Movie>? Movies { get; set; }
    }
}
