using AutoMapper;
using Contracts;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ultimate_anime_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public StudioController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetStudios()
        {
            var studios = _repository.Studio.GetAllStudios(trackChanges: false);

            var studiosDto = _mapper.Map<IEnumerable<StudioDto>>(studios);

            return Ok(studiosDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudio(Guid id)
        {
            var studio = _repository.Studio.GetStudio(id,trackChanges: false);
            if (studio == null)
            {
                _logger.LogInfo($"Studio with id: {id} doesn't exist in the database");
                return NotFound();
            }
            else
            {
                var studioDto = _mapper.Map<StudioDto>(studio);

                return Ok(studioDto);
            }
            
        }
    }
}
