using Models.Base.Database;
using System.ComponentModel.DataAnnotations;

namespace Models.Customer.Entities
{
    public class Customer : BaseData
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string Number { get; set; }

        [StringLength(20)]
        public string Complement { get; set; }

        [StringLength(20)]
        public string Neighborhood { get; set; }

        [Required]
        [StringLength(20)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(8)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
