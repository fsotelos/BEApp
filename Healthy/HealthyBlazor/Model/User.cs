using Healthy.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace HealthyBlazor.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastFirstName { get; set; }
        public string? LastSecondName { get; set; }
        public DateTime? BirthDayDate { get; set; }
        public string? NickName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string? Password { get; set; }

        [JsonIgnore]
        public string? ConfirmPassword { get; set; }

        public Gender? Gender { get; set; }
        public string? PhoneCodeArea { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
