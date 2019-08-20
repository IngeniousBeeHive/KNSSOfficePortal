using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
   
    public class Member : AppBase
    {
        [Required]
        public Branch BranchId { get; set; }

        [Required(ErrorMessage = "Family Number cannot accept special character or blank")]
        [RegularExpression("^[0-9 ]+$")]
        public int FamilyNo { get; set; }

        [Required]
        public MemberType MemberTypeId { get; set; }

        [Required(ErrorMessage = "Member Name cannot be blank")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Member Name must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        public string MemberName { get; set; }  
       
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateofBirth { get; set; }

        public int Age { get; set; }

        [Required(ErrorMessage = "Mobile cannot be blank")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mobile number must be a 10 digit number")]
        [RegularExpression(@"^[0-9]+$")]
        public string MobileNo { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "Alternate Mobile number must be a 10 digit number")]
        [RegularExpression(@"^[0-9]+$")]
        public string AlternateMobileNo { get; set; }

        public string Email { get; set; }
        public bool Status { get; set; }
        public string BloodGroup { get; set; }

        public string Address { get; set; }
        public string Photo { get; set; }


    }
}
