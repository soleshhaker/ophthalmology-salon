using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using System.Security.Claims;
using Utility;
using static Utility.Enums;

namespace OphthalmologySalon.Areas.Doctor
{
    [Area("Doctor")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Doctor")]
    public class VisitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AllVisits()
        {
            try
            {
                var visits = _unitOfWork.Visit.GetAll(null, includeProperties: "ApplicationUser");
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
        [HttpPost("VisitStatus")]
        public IActionResult VisitStatus(int id, VisitStatus visitStatus)
        {
            try
            {
                var visit = _unitOfWork.Visit.GetFirstOrDefault(x => x.Id == id, includeProperties: "ApplicationUser");
                if (visit != null)
                {
                    visit.VisitStatus = visitStatus;
                    _unitOfWork.Visit.Update(visit);
                    _unitOfWork.Save();
                    return Ok(_mapper.Map<VisitReadDTO>(visit));
                }
                else
                {
                    return NotFound("--> Cannot find visit");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("VisitCost")]
        public IActionResult VisitCost(int id, float cost, string? additionalInfo)
        {
            try
            {
                var visit = _unitOfWork.Visit.GetFirstOrDefault(x => x.Id == id, includeProperties: "ApplicationUser");
                if (visit == null)
                {
                    return NotFound("--> Cannot find visit");
                }

                visit.Cost += cost;
                visit.AdditionalInfo = additionalInfo == null ? visit.AdditionalInfo : additionalInfo;
                _unitOfWork.Visit.Update(visit);
                _unitOfWork.Save();
                return Ok(_mapper.Map<VisitReadDTO>(visit));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
