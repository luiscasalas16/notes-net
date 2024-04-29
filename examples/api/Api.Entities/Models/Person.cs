using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    [Table("People", Schema = "Application")]
    public class Person
    {
        [Key]
        [Column("PersonId")]
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string PreferredName { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? SearchName { get; set; } = null;

        public bool IsPermittedToLogon { get; set; } = false;

        public string? LogonName { get; set; } = "NO LOGON";

        public bool IsExternalLogonProvider { get; set; } = false;

        public byte[]? HashedPassword { get; set; } = null;

        public bool IsSystemUser { get; set; } = false;

        public bool IsEmployee { get; set; } = false;

        public bool IsSalesperson { get; set; } = false;

        public string? UserPreferences { get; set; } = null;

        public string? PhoneNumber { get; set; }

        public string? FaxNumber { get; set; }

        public string? EmailAddress { get; set; }

        public byte[]? Photo { get; set; } = null;

        public string? CustomFields { get; set; } = null;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? OtherLanguages { get; set; } = null;

        public int LastEditedBy { get; set; } = 1;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ValidFrom { get; set; } = new DateTime(2013, 1, 1);

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ValidTo { get; set; } = new DateTime(9999, 12, 31);
    }
}