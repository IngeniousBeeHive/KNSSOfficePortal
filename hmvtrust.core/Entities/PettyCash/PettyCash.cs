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
        [Required(ErrorMessage = "PaidTo cannot accept special character or blank")]
        public string PaidTo { get; set; }

        public string PaymentMode { get; set; }
        public string TransactionId { get; set; }
        public string EntryBy { get; set; }


        [StringLength(100, MinimumLength = 1, ErrorMessage = "Description must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount cannot accept special character or blank")]
        [RegularExpression("^[0-9 ]+$")]
        public int Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReceivedDate { get; set; }

        public string Status { get; set; }
        public string AttachedFiles { get; set; }
    }
}
