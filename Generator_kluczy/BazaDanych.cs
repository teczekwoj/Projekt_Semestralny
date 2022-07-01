using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Generator_kluczy
{
    public class BazaDanych : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = BazaDanych.db");
        }

        public DbSet<Keys> Keys { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Deleted> Deleted { get; set; }
    }

    public class Keys
    {
        [Key]
        public string Key { get; set; }

        public int ProductID { get; set; }
        public int StatusID { get; set; }
    }

    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Status
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Deleted
    {
        [Key]
        public string Key { get; set; }

        public int ProductID { get; set; }
        public int StatusID { get; set; }
    }
}
