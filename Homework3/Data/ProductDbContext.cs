﻿using Homework3.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
