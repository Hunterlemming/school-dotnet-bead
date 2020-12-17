using System.ComponentModel.DataAnnotations;

namespace netcore_gyakorlas.Models
{
    public abstract class AbstractEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
