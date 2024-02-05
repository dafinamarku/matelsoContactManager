using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer
{
    [Table("Contacts")]
    public class Contact
    {
        private string _salutation;
        private string _firstname;
        private string _lastname;
        private string _displayname;
        private DateTime? _birthdate;

        [Key, Required]
        public Guid Id { get; set; }
        [MinLength(3, ErrorMessage = "{0} must be longer than 2 characters.")]
        public string Salutation
        {
            get => _salutation;
            set => _salutation = value;
        }
        [MinLength(3, ErrorMessage = "{0} must be longer than 2 characters.")]
        public string Firstname 
        {
            get => _firstname;
            set => _firstname = value;
        }
        [MinLength(3, ErrorMessage = "{0} must be longer than 2 characters.")]
        public string Lastname 
        {
            get => _lastname;
            set => _lastname = value;
        }
        public string Displayname 
        {
            get => _displayname;
            set => _displayname = string.IsNullOrEmpty(value) ? $"{_salutation} {_firstname} {_lastname}" : value;  
        }
        public DateTime? Birthdate {
            get => _birthdate;
            set => _birthdate = value.HasValue ? value.Value.ToUniversalTime() : value;
        }
        public DateTime CreationTimestamp { get; private set; } = DateTime.UtcNow;
        public DateTime LastChangeTimestamp { get; set; } = DateTime.UtcNow;
        public bool NotifyHasBirthdaySoon
        {
            get
            {
                if (Birthdate.HasValue)
                {
                    DateTime today = DateTime.Today;
                    DateTime birthdayThisYear = new DateTime(today.Year, Birthdate.Value.Month, Birthdate.Value.Day);
                    int numDays = (birthdayThisYear - today).Days;

                    return numDays > 0 && numDays <= 14;
                }
                return false;
            }
        }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "{0} is not valid.")]
        public string Email { get; set; }
        [RegularExpression("^\\+?[1-9][0-9]{7,14}$", ErrorMessage = "{0} is not valid.")]
        public string? Phonenumber { get; set; }

    }
}