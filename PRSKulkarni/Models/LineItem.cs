using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PRSKulkarni.Models
{
    [Table("LineItem")]
    public class LineItem
    { // this model gives RequestID, ProductID for product and Quantity of products ordered.
        [Key]
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        public Request? Request { get; set; }
        public Product? Product { get; set; }


    }
}

/*

 Create Table LineItem
(
 Id			int Primary key Identity(1,1),
 RequestId	int Constraint fk_LineItem_Request References Request(Id) not null,
 ProductId	int  Constraint fk_LineItem_Product References Product(Id) not null,
 Quantity	int not null

)
* 
 * */

