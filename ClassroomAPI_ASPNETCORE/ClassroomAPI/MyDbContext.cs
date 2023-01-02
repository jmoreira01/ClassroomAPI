using ClassroomAPI.Models;
using ClassroomAPI.Services;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClassroomAPI
{
    public class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ClassroomApi; Integrated Security = True;");
        }

        
    }
}
