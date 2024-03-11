using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSKulkarni.Models
{
    [Table("User")]
    public class User
    {
        // attributes -- decorations
        //are classes and they have constructors
        [Key]
        public int Id { get; set; }
       
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Firstname { get; set; }

        //[NotMapped]

        //public string FullName { get { return Firstname + " " + Lastname; }}
        //[Required]
        [StringLength(20)]
        public string Lastname { get; set; }

        [StringLength(12)]
        public string? Phone { get; set; }
        [StringLength(75)]
        public string? Email { get; set; }
        public bool Reviewer { get; set; }
        public bool Admin { get; set; }


        public List<Request>? Requests { get; set; }

    }
    /*
     * create table [User]
(
 Id int primary key identity(1,1), 
 Username nVarchar(20) not null unique,
 Password nVarchar(10) not null,
 Firstname nVarchar(20) not null,
 Lastname nVarchar(20) not null,
 Phone nVarchar(12) null,
 Email Varchar(75) null,
 Reviewer bit not null,
 Admin bit not null
)

     */
}
