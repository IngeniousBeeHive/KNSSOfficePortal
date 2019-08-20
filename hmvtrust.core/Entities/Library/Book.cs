using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class Book : AppBase, IEquatable<Book>, IComparer<Book>, IComparable<Book>
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "BookNo does not accept special charater.")]
        public int BookNo { get; set; }

        [Required(ErrorMessage = "Book Name cannot be blank")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Book Name must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        public string BookName { get; set; }

        [Required]       
        public string AuthorName { get; set; }

        public BookCategory CategoryId { get; set; }

        public int Compare(Book x, Book y)
        {
            return x.BookNo.CompareTo(y.BookNo);
        }

        public int CompareTo(Book other)
        {
            return this.BookNo.CompareTo(other.BookNo);
        }

        public bool Equals(Book other)
        {
            return this.Id == other.Id;
        }
    }
}
