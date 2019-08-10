using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class LibraryLog:AppBase
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "BookNo does not accept special charater.")]
        public int BookNo { get; set; }

        [Required(ErrorMessage = "Book Name cannot be blank")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Book Name must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "MemberName cannot be blank")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "MemberName must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        public string MemberName { get; set; }

        [Required]
        [RegularExpression("^[0-9 ]+$", ErrorMessage = "MobileNo does not accept special charater.")]
        public long MobileNo { get; set; }       
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckedOutDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnedDate { get; set; }
    }
}
