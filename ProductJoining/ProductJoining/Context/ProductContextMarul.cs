﻿using Microsoft.EntityFrameworkCore;
using ProductJoining.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace  ProductJoining.Context
{
    public class ProductContextMarul : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<KategoriAddresses> KategoriAddresses { get; set; }
        public DbSet<Markets> Markets { get; set; }
        public DbSet<ProductAddress> ProductAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Unit { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ProductPoolDb;Trusted_Connection=True;");
        }
    }
}
