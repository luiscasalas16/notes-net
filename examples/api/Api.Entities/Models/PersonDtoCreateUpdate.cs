using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class PersonDtoCreateUpdate
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, ErrorMessage = "Full name can't be longer than 50 characters")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Preferred name is required")]
        [StringLength(50, ErrorMessage = "Preferred name can't be longer than 50 characters")]
        public string PreferredName { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(20, ErrorMessage = "Phone number can't be longer than 20 characters")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Fax number is required")]
        [StringLength(20, ErrorMessage = "Fax number can't be longer than 20 characters")]
        public string? FaxNumber { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [StringLength(256, ErrorMessage = "Email address can't be longer than 256 characters")]
        public string? EmailAddress { get; set; }
    }
}
