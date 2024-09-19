using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [StringLength(25, ErrorMessage = "This field can't exceed 25 character.")]
        public required string FirstName { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "This field can't exceed 25 character.")]
        public required string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public required string Gender { get; set; }
        [Required]
        public required Guid NationalityGuid { get; set; }

        [StringLength(100, ErrorMessage = "This field can't exceed 100 character")]
        public string? ProfilePictureURL { get; set; }

        [Required(ErrorMessage = "This field can't be null")]
        [StringLength(200, ErrorMessage = "This field can't exceed 200 character.")]
        public required string Address { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserGuid { get; set; }
        public Guid UpdatedByUserGuid { get; set; }
        public bool IsActive { get; set; }

        [ValidateNever]
        public Nationalities Nationalities { get; set; }

    }
}
