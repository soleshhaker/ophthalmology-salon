using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ophthalmology.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Visit>().HasData(
            //  new Visit
            //  {
            //      Id = 1,
            //      VisitStatus = Utility.Enums.VisitStatus.Approved,
            //      Start = new DateTime(2023, 9, 13, 9, 0, 0),
            //      End = new DateTime(2023, 9, 13, 11, 0, 0),
            //      VisitType = Utility.Enums.VisitType.RoutineEyeExam,
            //  });
        }
    }
}
