using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using KMCEventAPI.Data;
using KMCEventAPI.DTO;
using KMCEventAPI.Model;

namespace KMCEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly OrganizerRepo repo;

        public OrganizerController(IMapper _mapper, OrganizerRepo _repo)
        {
            mapper = _mapper;
            repo = _repo;
        }

        [HttpPost]
        public ActionResult AddOrganizer(OrganizerWriteDTO dto)
        {
            var model = mapper.Map<Organizer>(dto);
            if (repo.Create(model))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public ActionResult<List<OrganizerReadDTO>> GetOrganizers()
        {
            var list = repo.GetAll();
            return Ok(mapper.Map<List<OrganizerReadDTO>>(list));
        }
    }
}