using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Domain.Entities
{
    [Table("Product")]
    public class Product
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field 'Title' is required")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field 'Image' is required")]
        [StringLength(50)]
        public string Image { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PricePerItem { get; set; }

        [Required(ErrorMessage = "Field 'Quantity' is required")]
        public int Quantity { get; set; }

        [StringLength(1000, ErrorMessage = "Too much symbols. Please, enter less than 1000")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; } = new HashSet<Order>();
    }
}
