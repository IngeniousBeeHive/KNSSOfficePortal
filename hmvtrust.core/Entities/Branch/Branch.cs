using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class Branch : AppBase, IEquatable<Branch>, IComparer<Branch>, IComparable<Branch>
    {

        [Required]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "BranchName does not accept special charater.")]
        public string BranchName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "BranchCode does not accept special charater.")]
        public string BranchCode { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "PinCode must be between 6 digits")]
        [RegularExpression("^[0-9 ]+$", ErrorMessage = "PinCode can be only in numbers.")]
        public string PinCode { get; set; }

        public int Compare(Branch x, Branch y)
        {
            return x.BranchName.CompareTo(y.BranchName);
        }

        public int CompareTo(Branch other)
        {
            return this.BranchName.CompareTo(other.BranchName);
        }

        public bool Equals(Branch other)
        {
            return this.Id == other.Id;
        }
    }
}
