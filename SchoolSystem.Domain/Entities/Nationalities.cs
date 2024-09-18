using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class Nationalities
    {
        [Key]
        public Guid NationalityGuid { get; set; }

        [Required(ErrorMessage = "It can't be a null value.")]
        [StringLength(15, ErrorMessage = "Nationality Name can not exceed 15 character.")]
        public required string Nationality { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserGuid { get; set; }
        public Guid UpdatedByUserGuid { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}


//# Nationality

//NationalityGuid
//Nationality
//IsActive
//CreatedDate
//UpdatedDate
//CreatedByUserGuid
//UpdatedByUserGuid