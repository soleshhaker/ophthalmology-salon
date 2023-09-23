using AutoMapper;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Moq;
using Ophthalmology.Models;
using OphthalmologySalon.Areas.Doctor;
using OphthalmologySalon.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace IntegrationTests.Doctor
{
    public class VisitControllerTests
    {
        private static IMapper _mapper;

        public VisitControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new VisitProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public void AllVisits_ReturnsOkResult_WhenVisitsExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
           .UseInMemoryDatabase(databaseName: RandomDBName.GetRandomName())
           .Options;

            using (var dbContext = new ApplicationDBContext(options))
            {
                dbContext.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "testUserId",
                    Name = "someName",
                    Role = SD.Role_Customer
                });
                dbContext.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "testDoctorId",
                    Name = "someDoc",
                    Role = SD.Role_Doctor
                });
                dbContext.Visits.Add(new Visit
                {
                    Id = 0,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(15),
                    VisitType = Enums.VisitType.RoutineEyeExam,
                    VisitStatus = Enums.VisitStatus.Pending,
                    Cost = 100,
                    ApplicationUserId = "testUserId"
                });
                dbContext.SaveChanges();
                // Arrange
                var mockUnitOfWork = new Mock<UnitOfWork>(dbContext);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, "testDoctorId"),
                };
                var identity = new ClaimsIdentity(claims, "TestAuthType");
                var principal = new ClaimsPrincipal(identity);

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Object.HttpContext = new DefaultHttpContext { User = principal };

                var controller = new VisitController(mockUnitOfWork.Object, _mapper)
                {
                    ControllerContext = mockControllerContext.Object
                };
                // Act
                var result = controller.AllVisits();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.IsAssignableFrom<IEnumerable<VisitReadDTO>>(okResult.Value);
            }
        }
        [Fact]
        public void VisitStatus_ReturnsOkResult_WhenVisitsExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
         .UseInMemoryDatabase(databaseName: RandomDBName.GetRandomName())
         .Options;

            using (var dbContext = new ApplicationDBContext(options))
            {
                dbContext.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "testUserId",
                    Name = "someName",
                    Role = SD.Role_Customer
                });
                dbContext.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "testDoctorId",
                    Name = "someDoc",
                    Role = SD.Role_Doctor
                });
                dbContext.Visits.Add(new Visit
                {
                    Id = 1,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(15),
                    VisitType = Enums.VisitType.RoutineEyeExam,
                    VisitStatus = Enums.VisitStatus.Pending,
                    Cost = 100,
                    ApplicationUserId = "testUserId"
                });
                dbContext.SaveChanges();
            }
            using (var dbContext = new ApplicationDBContext(options))
            {
                // Arrange
                var mockUnitOfWork = new Mock<UnitOfWork>(dbContext);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, "testDoctorId"),
                };
                var identity = new ClaimsIdentity(claims, "TestAuthType");
                var principal = new ClaimsPrincipal(identity);

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Object.HttpContext = new DefaultHttpContext { User = principal };

                var controller = new VisitController(mockUnitOfWork.Object, _mapper)
                {
                    ControllerContext = mockControllerContext.Object
                };
                // Act
                var result = controller.VisitStatus(1, Enums.VisitStatus.Approved);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.True((okResult.Value as VisitReadDTO).VisitStatus == Enums.VisitStatus.Approved);
                Assert.IsAssignableFrom<VisitReadDTO>(okResult.Value);
            }
        }
        [Fact]
        public void VisitCost_ReturnsOkResult_WhenVisitsExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
         .UseInMemoryDatabase(databaseName: RandomDBName.GetRandomName())
         .Options;

            using (var dbContext = new ApplicationDBContext(options))
            {
                dbContext.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "testUserId",
                    Name = "someName",
                    Role = SD.Role_Customer
                });
                dbContext.ApplicationUsers.Add(new ApplicationUser
                {
                    Id = "testDoctorId",
                    Name = "someDoc",
                    Role = SD.Role_Doctor
                });
                dbContext.Visits.Add(new Visit
                {
                    Id = 1,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(15),
                    VisitType = Enums.VisitType.RoutineEyeExam,
                    VisitStatus = Enums.VisitStatus.Pending,
                    Cost = 100,
                    ApplicationUserId = "testUserId"
                });
                dbContext.SaveChanges();
            }
            using (var dbContext = new ApplicationDBContext(options))
            {
                // Arrange
                var mockUnitOfWork = new Mock<UnitOfWork>(dbContext);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, "testDoctorId"),
                };
                var identity = new ClaimsIdentity(claims, "TestAuthType");
                var principal = new ClaimsPrincipal(identity);

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Object.HttpContext = new DefaultHttpContext { User = principal };

                var controller = new VisitController(mockUnitOfWork.Object, _mapper)
                {
                    ControllerContext = mockControllerContext.Object
                };
                // Act
                var result = controller.VisitCost(1, 30, "new info");

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.True((okResult.Value as VisitReadDTO).Cost == 130);
                Assert.True((okResult.Value as VisitReadDTO).AdditionalInfo == "new info");
                Assert.IsAssignableFrom<VisitReadDTO>(okResult.Value);
            }
        }
    }
}