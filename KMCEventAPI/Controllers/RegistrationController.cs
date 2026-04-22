using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using KMCEventAPI.Data;
using KMCEventAPI.DTO;
using KMCEventAPI.Model;

namespace KMCEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly RegistrationRepo repo;

        public RegistrationController(IMapper _mapper, RegistrationRepo _repo)
        {
            mapper = _mapper;
            repo = _repo;
        }

        [HttpPost]
        public ActionResult Register(RegistrationWriteDTO dto)
        {
            var participant = new Participant
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone
            };

            var result = repo.Register(dto.EventId, participant);
            if (result == null)
                return BadRequest("Registration failed. Event may be full, inactive, missing, or already registered.");

            return Ok(mapper.Map<RegistrationReadDTO>(result));
        }

        [HttpGet]
        public ActionResult GetRegistrations()
        {
            var list = repo.GetAll();

            var result = list.Select(r => new
            {
                r.RegistrationId,
                r.EventId,
                r.ParticipantId,
                r.RegisteredAt,
                FullName = r.Participant != null ? r.Participant.FullName : "",
                Email = r.Participant != null ? r.Participant.Email : "",
                Phone = r.Participant != null ? r.Participant.Phone : "",
                EventTitle = r.Event != null ? r.Event.Title : ""
            }).ToList();

            return Ok(result);
        }
    }
}