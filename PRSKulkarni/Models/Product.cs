using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSKulkarni.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int VendorID { get; set; }

        [Required]
        [StringLength(50)]
        public string PartNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string? PhotoPath { get; set; }

        public Vendor? Vendor { get; set; } // Navigation property not in SQL 
 
    }
}
/*
 * CREATE TABLE Product (

ID			Int				Primary key Identity(1,1),

VendorID	int				not null Constraint fk_product_vendor References Vendor(id),

PartNumber	Varchar(50)		not Null Unique,
Name		Varchar (150)	Not Null,
Price		Decimal (10,2)	Not NUll,
Unit		Varchar(255)	Not Null,
PhotoPath	Varchar(255)	Null -- URL 

)
 * 
 */