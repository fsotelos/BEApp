using Healthy.Business.Specifications.Definitions;
using Healthy.Domain.Constants;
using Healthy.Domain.Entities;
using Healthy.Infraestructure;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Healthy.Business.Specifications.Implementations
{
    public class UserSpecification : IUserSpecification
    {
        private readonly HealthyDbContext _db;
        public UserSpecification(HealthyDbContext healthyDbContext)
        {
            _db = healthyDbContext;
        }
        public IEnumerable<string> Valid(User user)
        {
            if (user == null)
                yield return Constants.UserSpecificationUserNullError;

            if (!IsValidEmail(user.Email))
                yield return Constants.UserSpecificationEmailError;

            if (string.IsNullOrWhiteSpace(user.FirstName))
                yield return Constants.UserSpecificationFirstNameNullEmptyOrWithSpace;

            if (string.IsNullOrWhiteSpace(user.LastFirstName))
                yield return Constants.UserSpecificationLastFirstNameNullEmptyOrWithSpace;

            if (string.IsNullOrWhiteSpace(user.NickName))
                yield return Constants.UserSpecificationNickNameNullEmptyOrWithSpace;

            if (InvalidateBirthDate(user))
                yield return Constants.UserSpecificationMinBirthDay;

            if (ExistEmail(user))
                yield return Constants.UserSpecificationDuplicatedEmail;

            if (ExistNickName(user))
                yield return Constants.UserSpecificationDuplicatedNickName;

            if (!ExistUserAndRequireEdit(user) && user.Id != 0)
                yield return Constants.UserSpecificationNotExistEditingUser;

            if (!IsValidPassword(user.Password))
                yield return Constants.UserSpecificationInvalidPassword;

            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
                yield return Constants.UserSpecificationInvalidPhoneNumber;
        }
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            if (password.Length < 8)
                return false;

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialCharacter = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpperCase = true;
                else if (char.IsLower(c))
                    hasLowerCase = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                    hasSpecialCharacter = true;
            }

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialCharacter;
        }

        private bool ExistUserAndRequireEdit(User user)
        {
            return _db.Users.Any(x => x.Id  == user.Id && user.Id > 0);
        }
        private bool ExistEmail(User user)
        {
            return _db.Users.Any(x => x.Email == user.Email && x.Id != user.Id);
        }

        private bool ExistNickName(User user)
        {
            return _db.Users.Any(x => x.NickName == user.NickName && x.Id != user.Id);
        }
        private bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        private bool InvalidateBirthDate(User user)
        {
            return user.BirthDayDate > DateTime.Now.AddYears(-Constants.UserSpecificationMinYearsOld);
        }
    }
}
