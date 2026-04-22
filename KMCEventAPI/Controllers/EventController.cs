using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using KMCEventAPI.Data;
using KMCEventAPI.DTO;
using KMCEventAPI.Model;

namespace KMCEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly EventRepo repo;
        private readonly OrganizerRepo organizerRepo;

        public EventController(IMapper _mapper, EventRepo _repo, OrganizerRepo _organizerRepo)
        {
            mapper = _mapper;
            repo = _repo;
            organizerRepo = _organizerRepo;
        }

        [HttpPost]
        public ActionResult AddEvent(EventWriteDTO dto)
        {
            var organizer = organizerRepo.GetById(dto.OrganizerId);
            if (organizer == null)
                return BadRequest("Organizer not found.");

            var model = mapper.Map<Event>(dto);
            if (repo.Create(model))
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public ActionResult<List<EventReadDTO>> GetEvents()
        {
            var events = repo.GetAll();
            return Ok(mapper.Map<List<EventReadDTO>>(events));
        }

        [HttpGet("{id}")]
        public ActionResult<EventReadDTO> GetEvent(int id)
        {
            var ev = repo.GetById(id);
            if (ev == null)
                return NotFound();

            return Ok(mapper.Map<EventReadDTO>(ev));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEvent(int id, [FromQuery] int organizerId, EventWriteDTO dto)
        {
            var existing = repo.GetById(id);
            if (existing == null)
                return NotFound();

            if (existing.OrganizerId != organizerId)
                return Unauthorized("Only the creator can update this event.");

            mapper.Map(dto, existing);
            existing.EventId = id;
            existing.OrganizerId = organizerId;

            if (repo.Update(existing))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEvent(int id, [FromQuery] int organizerId)
        {
            var existing = repo.GetById(id);
            if (existing == null)
                return NotFound();

            if (existing.OrganizerId != organizerId)
                return Unauthorized("Only the creator can delete this event.");

            if (repo.Remove(existing))
                return Ok();

            return BadRequest();
        }

        [HttpGet("search")]
        public ActionResult<List<EventReadDTO>> Search([FromQuery] string? type, [FromQuery] DateTime? date, [FromQuery] string? venue)
        {
            var events = repo.Search(type, date, venue);
            return Ok(mapper.Map<List<EventReadDTO>>(events));
        }

        [HttpGet("organizer/{organizerId}")]
        public ActionResult<List<EventReadDTO>> GetEventsByOrganizer(int organizerId)
        {
            var events = repo.GetByOrganizer(organizerId);
            return Ok(mapper.Map<List<EventReadDTO>>(events));
        }
    }
}