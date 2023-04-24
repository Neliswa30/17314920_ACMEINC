using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG7311_Task2.Models.AcmeIncTask2db
{
    [Table("product")]
    public partial class Product
    {
        public Product()
        {
            ShoppingCarts = new HashSet<Cart>();
        }

        [Key]
        [Column("Prd_id")]
        public int Proid { get; set; }
        [Column("prdName")]
        [StringLength(255)]
        public string ProName { get; set; }
        [Column("prdPrice", TypeName = "smallmoney")]
        public decimal? ProPrice { get; set; }
        [Column("pDescription")]
        [StringLength(255)]
        public string ProDescription { get; set; }

        [InverseProperty(nameof(Cart.Pro))]
        public virtual ICollection<Cart> ShoppingCarts { get; set; }
    }
}
