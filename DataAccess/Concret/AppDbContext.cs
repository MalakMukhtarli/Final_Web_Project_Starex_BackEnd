using Entity.Entities;
using Entity.Entities.Branches;
using Entity.Entities.Cities;
using Entity.Entities.Contacts;
using Entity.Entities.Countries;
using Entity.Entities.Declarations;
using Entity.Entities.HomePages;
using Entity.Entities.Newss;
using Entity.Entities.Questions;
using Entity.Entities.Service;
using Entity.Entities.Tariffs;
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
        public DbSet<NewsDetail> NewsDetail { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionNavbar> QuestionNavbars { get; set; }
        public DbSet<Intro> Intros { get; set; }
        public DbSet<HowWorks> HowWorks { get; set; }
        public DbSet<Advantages> Advantages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<CountryContact> CountryContacts { get; set; }
        public DbSet<BranchContact> BranchContacts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<DistrictTariff> DistrictTariffs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>()
                .HasOne(b => b.NewsDetail)
                .WithOne(i => i.News)
                .HasForeignKey<NewsDetail>(b => b.NewsId);
        }
    }
}
