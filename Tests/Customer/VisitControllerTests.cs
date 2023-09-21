using AutoMapper;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Moq;
using Ophthalmology.Models;
using Ophthalmology_Salon.Areas.Customer.Controller;
using Ophthalmology_Salon.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Tests.Customer
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
                dbContext.SaveChanges();
                // Arrange
                var mockUnitOfWork = new Mock<UnitOfWork>(dbContext);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, "testUserId"),
                };
                var identity = new ClaimsIdentity(claims, "TestAuthType");
                var principal = new ClaimsPrincipal(identity);

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Object.HttpContext = new DefaultHttpContext { User = principal };
                
                var controller = new VisitController(mockUnitOfWork.Object, _mapper)
                {
                    ControllerContext = mockControllerContext.Object
                };

                var availableTimes = controller.AvailableTime(Utility.Enums.VisitType.RoutineEyeExam);
                var availableTimesResult = ((ObjectResult)availableTimes.Result).Value as List<DateTime>;
                var randomTimeIndex = new Random().Next(0, availableTimesResult.Count);
                controller.Visit(new VisitCreateDTO() { Start = availableTimesResult[randomTimeIndex], VisitType = Utility.Enums.VisitType.RoutineEyeExam });

                // Act
                var result = controller.AllVisits();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.IsAssignableFrom<IEnumerable<VisitReadDTO>>(okResult.Value);
            }
        }
        [Fact]
        public void VisitById_ReturnsOkResult_WhenVisitsExist()
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
                dbContext.SaveChanges();

                var mockUnitOfWork = new Mock<UnitOfWork>(dbContext);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, "testUserId"),
                };
                var identity = new ClaimsIdentity(claims, "TestAuthType");
                var principal = new ClaimsPrincipal(identity);

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Object.HttpContext = new DefaultHttpContext { User = principal };


                var controller = new VisitController(mockUnitOfWork.Object, _mapper)
                {
                    ControllerContext = mockControllerContext.Object
                };

                var availableTimes = controller.AvailableTime(Utility.Enums.VisitType.RoutineEyeExam);
                var availableTimesResult = ((ObjectResult)availableTimes.Result).Value as List<DateTime>;
                var randomTimeIndex = new Random().Next(0, availableTimesResult.Count);

                var visitActionResult = controller.Visit(new VisitCreateDTO() { Start = availableTimesResult[randomTimeIndex], VisitType = Utility.Enums.VisitType.RoutineEyeExam });
                var visitValue = ((ObjectResult)visitActionResult).Value as VisitReadDTO;

                var result = controller.VisitById(visitValue.Id);

                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.IsAssignableFrom<VisitReadDTO>(okResult.Value);
            }
        }
    }
}