using Healthy.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Healthy.Domain.Entities
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
        public Gender Gender { get; set; }
        public string? PhoneCodeArea { get; set; }
        public string? PhoneNumber { get; set; }

        public void FormatDates()
        {
            CreatedDate = DateTime.SpecifyKind(CreatedDate.Value, DateTimeKind.Utc);
            if (UpdatedDate.HasValue)
            {
                CreatedDate = DateTime.SpecifyKind(UpdatedDate.Value, DateTimeKind.Utc);
            }
            CreatedDate = DateTime.SpecifyKind(BirthDayDate.Value, DateTimeKind.Utc);
        }
    }
}
