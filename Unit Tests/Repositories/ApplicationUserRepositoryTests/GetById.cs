using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Ophthalmology.Models;
using System;
using Utility;

namespace UnitTests.Repositories.ApplicationUserRepositoryTests
{
    public class GetById
    {
        [Fact]
        public void GetUserById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
           .UseInMemoryDatabase(databaseName: RandomDBName.GetRandomName())
           .Options;

            using (var dbContext = new ApplicationDBContext(options))
            {
                dbContext.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "newUserId",
                    Role = SD.Role_Doctor,
                    Name = "newUserName"
                });
                dbContext.SaveChanges();
            }

            //Act
            ApplicationUser user;
            using (var dbContext = new ApplicationDBContext(options))
            {
                var userRepo = new ApplicationUserRepository(dbContext);
                user = userRepo.GetFirstOrDefault(x => x.Id == "newUserId");
            }
            //Assert
            Assert.True(user != null);
        }
    }
}
