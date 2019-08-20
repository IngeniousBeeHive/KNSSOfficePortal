using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class MemberType : AppBase
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "MemberType does not accept special charater.")]
        public string MemberTypeName { get; set; }

        [Required(ErrorMessage = "Age Limit cannot be blank")]        
        public string AgeLimit { get; set; }
    }
}
