using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PROG7311_Task2.Models.AcmeIncTask2db;

#nullable disable

namespace PROG7311_Task2.Models.AcmeIncTask2db
{
    public partial class AcmeIncDbContext : DbContext
    {
        public AcmeIncDbContext()
        {
        }

        public AcmeIncDbContext(DbContextOptions<AcmeIncDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet <Admins> Administrators { get; set; }
        public virtual DbSet<User> CustomerUsers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> ShoppingCarts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-43BN61N2;Initial Catalog=prog7311task2db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.UserSurname)
                    .HasName("PK__administ__7170040000C57C11");

                entity.Property(e => e.UserSurname).IsUnicode(false);

                entity.Property(e => e.UserPassword).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__customer__F3DBC57362D678C1");

                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.UserFirstname).IsUnicode(false);

                entity.Property(e => e.UserLastname).IsUnicode(false);

                entity.Property(e => e.UserPassword).IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Proid)
                    .HasName("PK__product__5BBAE2CDDD2CB2E6");

                entity.Property(e => e.pDescription).IsUnicode(false);

                entity.Property(e => e.prdName).IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Pro)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.Proid)
                    .HasConstraintName("FK__shoppingC__proid__5DCAEF64");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__shoppingC__usern__5CD6CB2B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
