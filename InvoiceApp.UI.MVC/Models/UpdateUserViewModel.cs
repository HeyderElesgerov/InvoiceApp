using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.UI.MVC.Models
{
    public class UpdateUserViewModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ReNewPassword { get; set; }

        public bool IsAdmin { get; set; }
    }
}
