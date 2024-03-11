using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRSKulkarni.Models
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string  Code { get; set; }


        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [Required]
        [StringLength(255)]
        public string Address { get; set; }


        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(5)]
        public string Zip { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        //[JsonIgnore]
        public List<Product>? Products { get; set; }
    }
}
/*
 * CREATE TABLE Vendor(
Id int primary key not null identity(1,1),
Code Varchar(10) Unique not null,
Name Varchar(255) not null ,
Address Varchar(255) not null,
City Varchar(255) not null,
State Varchar(2) not null,
Zip Varchar(5) not null,
Phone Varchar(12) null,
Email Varchar(100) null

)
 */