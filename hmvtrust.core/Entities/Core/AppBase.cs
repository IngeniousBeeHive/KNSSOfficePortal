using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core
{
    public class AppBase
    {
        public long Id { get; set; }

        public DateTime CreatedDate   { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate  { get; set; }

        [JsonIgnore]
        public virtual bool IsValid
        {
            get
            {
                return !ValidationHelper.ValidateEntity<AppBase>(this).HasError;
            }
        }

        public virtual IList<ValidationResult> Errors()
        {
            return ValidationHelper.ValidateEntity<AppBase>(this).Errors;
        }
    }
}
