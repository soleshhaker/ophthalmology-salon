using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Ophthalmology.Models;
using System.Security.Claims;
using Utility;
using static Utility.Enums;

namespace OphthalmologySalon.Areas.Customer.Controller
{
    [Area("Customer")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Customer")]
    public class VisitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Returns all visits with current user assigned to them</summary>
        /// <returns>Returns all visits with current user assigned to them</returns>
        [HttpGet("AllVisits")]
        public IActionResult AllVisits()
        {
            string userId = null;

            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                var visits = _unitOfWork.Visit.GetAll(x => x.ApplicationUserId == userId, includeProperties: "ApplicationUser");
                if (visits.Any())
                {
                    return Ok(_mapper.Map<IEnumerable<VisitReadDTO>>(visits));
                }
                else
                {
                    return NotFound("--> Cannot find any visits");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>Returns visit with given id if current user is assigned to them</summary>
        /// <returns>Returns visit with given id if current user is assigned to them</returns>
        [HttpGet("CustomerVisitById/{id?}", Name = "CustomerVisitById")]
        public IActionResult CustomerVisitById(int id)
        {
            string userId = null;

            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                var visit = _unitOfWork.Visit.GetFirstOrDefault(x => x.ApplicationUserId == userId && x.Id.Equals(id), includeProperties: "ApplicationUser");
                return Ok(_mapper.Map<VisitReadDTO>(visit));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>Returns available times for given visit type</summary>
        /// <returns>Returns available times for given visit type</returns>
        [HttpGet("AvailableTime")]
        public ActionResult<List<DateTime>> AvailableTime(VisitType visitType)
        {
            // Define the time frame (e.g., 3 months ahead)
            var lookAheadMonths = 3;
            var endDate = DateTime.Today.AddMonths(lookAheadMonths);

            var visitDuration = GetVisitDuration(visitType);

            var availableTimes = new List<DateTime>();

            // Define the minimum advance booking time (1 hour)
            var minimumAdvanceBookingTime = DateTime.Now.AddHours(1);

            var allVisits = _unitOfWork.Visit.GetAll(v => v.Start <= endDate && v.End >= DateTime.Today);
            allVisits = allVisits.OrderBy(v => v.Start).ToList();

            // Initialize the current date to the next available day at 8 am
            var currentDate = DateTime.Today;
            currentDate = currentDate.AddHours(8);

            // Loop through the time frame
            while (currentDate <= endDate)
            {
                // Define working hours (8 am to 3 pm) for the current day
                var startTime = currentDate.Date.AddHours(8);
                var endTime = currentDate.Date.AddHours(16);

                // Generate 15-minute time slots for the current day
                for (var slotStart = startTime; slotStart < endTime; slotStart = slotStart.AddMinutes(15))
                {
                    // Check if the current slot is at least 1 hour in the future and there's enough time for the visit
                    if (slotStart >= minimumAdvanceBookingTime && slotStart.Add(visitDuration) <= endTime)
                    {
                        // Check if the slot overlaps with any existing visit
                        if (!allVisits.Any(v => slotStart >= v.Start && slotStart < v.End
                        || slotStart.Add(visitDuration) > v.Start && slotStart.Add(visitDuration) <= v.End
                        || slotStart <= v.Start && slotStart.Add(visitDuration) >= v.End))
                        {
                            availableTimes.Add(slotStart);
                        }
                    }
                }

                // Move to the next day
                currentDate = currentDate.AddDays(1);
            }

            return Ok(availableTimes);
        }

        /// <summary>Post a visit</summary>
        /// <returns>Returns created at route or not found exception if something goes wrong</returns>
        [HttpPost]
        public IActionResult Visit(VisitCreateDTO visitCreateDTO)
        {
            string userId = null;
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                var visit = _mapper.Map<Visit>(visitCreateDTO);
                visit.End = visit.Start.Add(GetVisitDuration(visit.VisitType));
                visit.VisitStatus = VisitStatus.Pending;
                visit.Cost = GetVisitCost(visit.VisitType);
                visit.ApplicationUserId = userId;

                _unitOfWork.Visit.Add(visit);
                _unitOfWork.Save();

                var visitReadDto = _mapper.Map<VisitReadDTO>(visit);

                return CreatedAtRoute(nameof(CustomerVisitById), new { visitReadDto.Id }, visitReadDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

#region HelperFunctions
        private TimeSpan GetVisitDuration(VisitType visitType)
        {
            switch (visitType)
            {
                case VisitType.RoutineEyeExam:
                    return TimeSpan.FromMinutes(15);
                case VisitType.ComprehensiveEyeExam:
                    return TimeSpan.FromHours(1);
                case VisitType.EmergencyEyeCare:
                    return TimeSpan.FromHours(1);
                case VisitType.CataractEvaluation:
                    return TimeSpan.FromMinutes(30);
                case VisitType.ContactLensFitting:
                    return TimeSpan.FromMinutes(30);
                default:
                    throw new ArgumentException("Invalid VisitType");
            }
        }

        private float GetVisitCost(VisitType visitType)
        {
            switch (visitType)
            {
                case VisitType.RoutineEyeExam:
                    return 100f;
                case VisitType.ComprehensiveEyeExam:
                    return 300f;
                case VisitType.EmergencyEyeCare:
                    return 400f;
                case VisitType.CataractEvaluation:
                    return 150f;
                case VisitType.ContactLensFitting:
                    return 100f;
                default:
                    throw new ArgumentException("Invalid VisitType");
            }
        }
#endregion
    }
}