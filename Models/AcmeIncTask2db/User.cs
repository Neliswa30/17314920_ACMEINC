using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG7311_Task2.Models.AcmeIncTask2db
{
    [Table("customerUser")]
    public partial class User
    {
        public User()
        {
            ShoppingCarts = new HashSet<Cart>();
        }

        [Key]
        [Column("username")]
        [StringLength(255)]
        public string Username { get; set; }
        [Column("userFirstname")]
        [StringLength(255)]
        public string UserFirstname { get; set; }
        [Column("userLastname")]
        [StringLength(255)]
        public string UserLastname { get; set; }
        [Required]
        [Column("userPassword")]
        [StringLength(255)]
        public string UserPassword { get; set; }

        [InverseProperty(nameof(Cart.UsernameNavigation))]
        public virtual ICollection<Cart> ShoppingCarts { get; set; }
    }
}
