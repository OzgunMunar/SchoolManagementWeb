using SchoolSystem.Application.Builders;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Application.Interfaces
{
    public interface IUserBuilder
    {
        public UserBuilder SetFirstName(string firstName);
        public UserBuilder SetLastName(string lastName);
        public UserBuilder SetAddress(string address);
        public UserBuilder SetBirthDate(DateTime birthDate);
        public UserBuilder SetGender(string gender);
        public UserBuilder SetProfilePictureURL(string profilePictureURL);
        public UserBuilder SetNationalityGuid(Guid nationalityGuid);
        public UserBuilder SetPhoneNumber(string phoneNumber);
        public UserBuilder SetCreatedByUserGuid(Guid createdByUserGuid);
        public UserBuilder SetUpdatedByUserGuid(Guid updatedByUserGuid);
        public UserBuilder SetIsActive(bool isActive);
        public ApplicationUser BuildUser();
    }
}
