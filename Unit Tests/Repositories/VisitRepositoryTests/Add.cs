using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Ophthalmology.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Repositories.VisitRepositoryTests
{
    public class Add
    {
        [Fact]
        public void AddVisit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
           .UseInMemoryDatabase(databaseName: RandomDBName.GetRandomName())
           .Options;

            using (var dbContext = new ApplicationDBContext(options))
            {
                var visitRepo = new VisitRepository(dbContext);

                //Act
                Visit visit;
                visitRepo.Add(new Visit()
                {
                    Id = 1,
                    Cost = 100,
                    Start = DateTime.Now,
                    VisitType = Utility.Enums.VisitType.RoutineEyeExam,
                    ApplicationUserId = "testId"
                });
                dbContext.SaveChanges();
                visit = visitRepo.GetAll().FirstOrDefault();
                //Assert
                Assert.True(visit != null);
            }
        }
        [Fact]
        public void AddVisitWithNegativeCost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
           .UseInMemoryDatabase(databaseName: RandomDBName.GetRandomName())
           .Options;

            using (var dbContext = new ApplicationDBContext(options))
            {
                var visitRepo = new VisitRepository(dbContext);

                //Act
                var visit = new Visit()
                {
                    Id = 1,
                    Cost = -100,
                    Start = DateTime.Now,
                    VisitType = Utility.Enums.VisitType.RoutineEyeExam,
                    ApplicationUserId = "testId"
                };

                var validationContext = new ValidationContext(visit);
                var validationResults = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(visit, validationContext, validationResults, true);

                if (isValid)
                {
                    visitRepo.Add(visit);
                    dbContext.SaveChanges();
                    visit = visitRepo.GetAll().FirstOrDefault();
                }
                else
                {
                    visit = null;
                }
                //Assert
                Assert.True(visit == null);
            }
        }
    }
}
