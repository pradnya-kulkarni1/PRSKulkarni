using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSKulkarni.Models
{
    [Table ("Request")]
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength (100)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Justification { get; set; }

        [Required]
        public DateTime DateNeeded { get; set; }

        [Required]
        [StringLength(25)]
        public string DeliveryMode { get; set; } = "Pickup";

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "New";

        [Required]
        public decimal Total { get; set; } = 0;

        [Required]
        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string? ReasonForRejection { get; set; }

        public User? User { get; set; }



    }
}
/*
 * 
 * Create Table Request (

Id				int Primary Key Identity (1,1),
UserID			int Constraint fk_Request_User References [User](Id),
Description		Varchar(100) not null, 
Justification	Varchar(255) not null,
DateNeeded		Date not null,
DeliveryMode	Varchar(25) Default 'Pickup' not null,
Status			Varchar(20) not null Default 'New',
Total			Decimal(10,2) not null Default 0, 
SumittedDate	Date not null default Getdate(),
ReasonForRejection Varchar(100) null


)

 * */