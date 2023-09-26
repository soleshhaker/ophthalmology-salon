using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Ophthalmology.Models;
using System.Data;
using static Utility.Enums;
using System.Security.Claims;

namespace OphthalmologySalon.Areas.Admin.Controller
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class VisitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Returns all visits</summary>
        /// <returns>Returns all visits</returns>
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

        /// <summary>Try to get a visit by id</summary>
        /// <returns>Returns visit or exception if it's not found</returns>
        [HttpGet("AdminVisitById/{id?}", Name = "AdminVisitById")]
        public IActionResult VisitById(int id)
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
        /// <summary>Post a visit</summary>
        /// <returns>Returns created at route or not found exception if something goes wrong</returns>
        [HttpPost]
        public IActionResult Visit(Visit visit)
        {
            try
            {
                _unitOfWork.Visit.Add(visit);
                _unitOfWork.Save();

                var visitReadDto = _mapper.Map<VisitReadDTO>(visit);

                return CreatedAtRoute(nameof(VisitById), new { visitReadDto.Id }, visitReadDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>Update a visit of given id</summary>
        /// <returns>Returns created at route or not found exception if something goes wrong</returns>
        [HttpPost("UpdateVisit")]
        public IActionResult UpdateVisit(Visit visit, int id)
        {
            try
            {
                var visitFromDb = _unitOfWork.Visit.GetFirstOrDefault(x => x.Id.Equals(id), includeProperties: "ApplicationUser");
                if (visitFromDb != null)
                {
                    visitFromDb.Start = visit.Start;
                    visitFromDb.End = visit.End;
                    visitFromDb.ApplicationUserId = visit.ApplicationUserId;
                    visitFromDb.VisitType = visit.VisitType;
                    visitFromDb.VisitStatus = visit.VisitStatus;
                    visitFromDb.Cost = visit.Cost;
                    visitFromDb.AdditionalInfo = visit.AdditionalInfo;

                    _unitOfWork.Visit.Update(visitFromDb);
                    _unitOfWork.Save();

                    var visitReadDto = _mapper.Map<VisitReadDTO>(visitFromDb);

                    return CreatedAtRoute("AdminVisitById", new { visitReadDto.Id }, visitReadDto);
                }
                else
                {
                    return NotFound("Visit not found.");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>Deletes a visit of given id</summary>
        /// <returns>Returns ok or not found exception if something goes wrong</returns>
        [HttpDelete]
        public IActionResult DeleteVisit(int visitId)
        {
            try
            {
                var visitFromDb = _unitOfWork.Visit.GetFirstOrDefault(x => x.Id == visitId);
                if (visitFromDb != null)
                {
                    _unitOfWork.Visit.Remove(visitFromDb);
                    _unitOfWork.Save();
                    return Ok();
                }
                else
                {
                    return NotFound("Visit not found.");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}