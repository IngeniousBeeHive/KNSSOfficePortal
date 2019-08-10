using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class User : AppBase
    {
        [Required(ErrorMessage = "First Name cannot be blank")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "First name must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z ]+$")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Middle name must be between 1 to 50 characters")]
        [RegularExpression(@"^[a-zA-Z'. ]+$")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be blank")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must be between 1 to 100 characters")]
        [RegularExpression("^[a-zA-Z'. ]+$")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        public string Password { get; set; }

        [Required(ErrorMessage = "Email cannot be blank")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email must be of format test@domain.com, max length of 50 characters")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mobile cannot be blank")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mobile number must be a 10 digit number")]
        [RegularExpression(@"^[0-9]+$")]
        public string Mobile { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "AlternateContact number must be a 10 digit number")]
        public string AlternateContact { get; set; }

        public bool IsLocked { get; set; }
        public DateTime? LastLoginTime { get; set; }

        public string ProfilePhoto { get; set; }

        ISet<Role> roles = new Iesi.Collections.Generic.LinkedHashSet<Role>();
        [Required(ErrorMessage = "Roles cannot be blank")]
        public ISet<Role> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        public bool IsInternal { get; set; }
    }
}
