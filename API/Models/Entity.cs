using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Entity
    {
        [Key]
        public string Id { get; set; }
    }
}
