﻿using Entity.Entities;
using Entity.Entities.Newss;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source = SQL5102.site4now.net; Initial Catalog = DB_A6F980_MMuhammed; User Id = DB_A6F980_MMuhammed_admin; Password = 123456@Mm");
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Service> Services { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>()
                .HasOne(b => b.NewsDetail)
                .WithOne(i => i.News)
                .HasForeignKey<NewsDetail>(b => b.NewsId);
        }
    }
}
