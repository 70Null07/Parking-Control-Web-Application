using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWebApplicationHTTP.Models;

namespace TestWebApplicationHTTP.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UsersInfo { get; set; }

        public DbSet<Place> Places { get; set; }
        
        public DbSet<AvgParkingTime> AvgParkingTimes { get; set; }

        public DbSet<AvgLoadByHour> AvgLoadByHours { get; set; }
        //public DbSet<TestWebApplicationHTTP.Models.User> User { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvgLoadByHour>().HasNoKey();
            modelBuilder.Entity<AvgParkingTime>().HasNoKey();
        }
    }
}
