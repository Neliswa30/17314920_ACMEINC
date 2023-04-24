using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PROG7311_Task2.Models.AcmeIncTask2db
{
    [Table("administrator")]
    public partial class Admins
    {
        [Key]
        [Column("userSurname")]
        [StringLength(255)]
        public string UserSurname { get; set; }
        [Required]
        [Column("userPassword")]
        [StringLength(255)]
        public string UserPassword { get; set; }
    }
}
