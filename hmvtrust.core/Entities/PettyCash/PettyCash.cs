using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace hmvtrust.core.Entities
{
    public class PettyCash : AppBase
    {
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Description must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        public string Description { get; set; }

        public int Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReceivedDate { get; set; }

        public string AttachedFiles { get; set; }
    }
}
