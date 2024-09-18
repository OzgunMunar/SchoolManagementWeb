using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.Dynamic;

namespace SchoolSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public Guid NationalityGuid { get; set; }
        public string ProfilePictureURL { get; set; }
        public Guid AddressGuid { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserGuid { get; set; }
        public Guid UpdatedByUserGuid { get; set; }
        public bool IsActive { get; set; }
    }
}
