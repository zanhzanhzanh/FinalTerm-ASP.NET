﻿using FinalTerm.Models;

namespace FinalTerm.Database {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Order>()
                .Property(e => e.CashBackAmount)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
