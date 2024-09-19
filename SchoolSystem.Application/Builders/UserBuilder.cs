using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Builders
{
    public class UserBuilder : IUserBuilder
    {
        public ApplicationUser _user;
        public UserBuilder()
        {
            try
            {
                _user = Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        public UserBuilder SetFirstName(string firstName) 
        { 
            _user.FirstName = firstName; 
            return this; 
        }
        public UserBuilder SetLastName(string lastName) 
        { 
            _user.LastName = lastName; 
            return this; 
        }
        public UserBuilder SetAddress(string address) 
        { 
            _user.Address = address; 
            return this; 
        }
        public UserBuilder SetBirthDate(DateTime birthDate) 
        { 
            _user.BirthDate = birthDate; 
            return this; 
        }
        public UserBuilder SetGender(string gender) 
        { 
            _user.Gender = gender; 
            return this; 
        }
        public UserBuilder SetProfilePictureURL(string profilePictureURL) 
        { 
            _user.ProfilePictureURL = profilePictureURL; 
            return this; 
        }
        public UserBuilder SetNationalityGuid(Guid nationalityGuid) 
        {
            _user.NationalityGuid = nationalityGuid; 
            return this; 
        }
        public UserBuilder SetPhoneNumber(string phoneNumber) 
        { 
            _user.PhoneNumber = phoneNumber; 
            return this; 
        }
        public UserBuilder SetCreatedByUserGuid(Guid createdByUserGuid) 
        {
            _user.CreatedByUserGuid = createdByUserGuid; 
            return this; 
        }
        public UserBuilder SetUpdatedByUserGuid(Guid updatedByUserGuid) 
        { 
            _user.UpdatedByUserGuid = updatedByUserGuid; 
            return this; 
        }
        public UserBuilder SetIsActive(bool isActive) 
        { 
            _user.IsActive = isActive; 
            return this; 
        }
        public ApplicationUser BuildUser() 
        { 
            return _user; 
        }
        
    }
}
