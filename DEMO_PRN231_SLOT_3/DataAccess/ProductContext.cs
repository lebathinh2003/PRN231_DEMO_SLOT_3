﻿using DEMO_PRN231_SLOT_3.Models;
using Microsoft.EntityFrameworkCore;

namespace DEMO_PRN231_SLOT_3.DataAccess;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Product");
    }

 
}
