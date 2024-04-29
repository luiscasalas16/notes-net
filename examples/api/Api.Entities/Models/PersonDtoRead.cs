using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class PersonDtoRead
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string PreferredName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? FaxNumber { get; set; }

        public string? EmailAddress { get; set; }
    }
}
