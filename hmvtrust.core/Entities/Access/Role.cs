using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class Role : AppBase, IEquatable<Role>, IComparer<Role>, IComparable<Role>
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name must be between 1 to 50 characters")]
        [RegularExpression("^[a-zA-Z ]+$")]
        public string Name { get; set; }

        public int Compare(Role x, Role y)
        {
            return x.Name.CompareTo(y.Name);
        }

        public int CompareTo(Role other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(Role other)
        {
            return this.Id == other.Id;
        }
    }
}
