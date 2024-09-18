using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class Addresses
    {
        [Key]
        public Guid AddressGuid { get; set; }

        [Required(ErrorMessage = "Full address is required.")]
        [StringLength(150, ErrorMessage = "Full address cannot exceed 150 characters.")]
        public required string FullAddress { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedTime { get; set; } 
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserGuid { get; set; }
        public Guid UpdatedByUserGuid { get; set; }


        
        public virtual ICollection<ApplicationUser> User { get; set; }

    }
}
