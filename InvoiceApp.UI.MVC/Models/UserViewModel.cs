using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.UI.MVC.Models
{
    public class UserViewModel
    {
        [Required]
        public string Key { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
