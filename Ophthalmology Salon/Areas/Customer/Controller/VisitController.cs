using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Ophthalmology.Models;
using System.Security.Claims;

namespace ophthalmology_salon.Areas.Customer.Controller
{
    [Area("customer")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllVisits")]
        public IActionResult GetAllVisits()
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
        [HttpGet("GetVisit/{id?}", Name = "GetVisitById")]
        public IActionResult GetVisitById(int id)
        {
            try
            {
                var visit = _unitOfWork.Visit.GetFirstOrDefault(x => x.Id.Equals(id), includeProperties: "ApplicationUser");
                return Ok(_mapper.Map<VisitReadDTO>(visit));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("BookVisit")]
        public IActionResult BookVisit(VisitCreateDTO visitCreateDTO)
        {
            string userId = null;
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                var visit = _mapper.Map<Visit>(visitCreateDTO);
                visit.VisitStatus = Utility.Enums.VisitStatus.Pending;
                visit.ApplicationUserId = userId;

                _unitOfWork.Visit.Add(visit);
                _unitOfWork.Save();

                var visitReadDto = _mapper.Map<VisitReadDTO>(visit);

                return CreatedAtRoute(nameof(GetVisitById), new { Id = visitReadDto.Id }, visitReadDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
