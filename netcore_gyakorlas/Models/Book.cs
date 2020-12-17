using System.ComponentModel.DataAnnotations.Schema;

namespace netcore_gyakorlas.Models
{
    public class Book : AbstractEntity
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public int PublishedYear { get; set; }
        public int PageNumber { get; set; }
        public string ISBN { get; set; }
        public int ageLimit { get; set; }
    }
}
