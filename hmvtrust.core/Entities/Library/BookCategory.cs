using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    
    public class BookCategory : AppBase
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        [RegularExpression("^[0-9 ]+$", ErrorMessage = "Quantity does not accept special charater.")]
        public int Quantity { get; set; }
    }
}
