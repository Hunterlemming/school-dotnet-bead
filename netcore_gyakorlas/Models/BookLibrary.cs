using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore_gyakorlas.Models
{
    public class BookLibrary : AbstractEntity
    {
        public Book Book { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        
        public Library Library { get; set; }
        [ForeignKey("Library")]
        public int LibraryId { get; set; }
        
        public int Count { get; set; }
    }
}
