using Microsoft.AspNetCore.Identity;

namespace SchoolSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public Guid NationalityGuid { get; set; }
        public string ProfilePictureURL { get; set; }
        public Guid AddressGuid { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserGuid { get; set; }
        public Guid UpdatedByUserGuid { get; set; }
        public bool IsActive { get; set; }


        public virtual Addresses Addresses { get; set; }
        public virtual Nationalities Nationalities { get; set; }

    }
}
