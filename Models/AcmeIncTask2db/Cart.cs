using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG7311_Task2.Models.AcmeIncTask2db
{
    [Table("shoppingCart")]
    public partial class Cart
    {
        public Cart(string username, int? proid)
        {
            Username = username;
            Proid = proid;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        [StringLength(255)]
        public string Username { get; set; }
        [Column("proid")]
        public int? Proid { get; set; }

        [ForeignKey(nameof(Proid))]
        [InverseProperty(nameof(Product.ShoppingCarts))]
        public virtual Product Pro { get; set; }
        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(User.ShoppingCarts))]
        public virtual User UsernameNavigation { get; set; }
    }
}
