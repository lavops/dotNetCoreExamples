using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ultimate_anime_api.ModelBinders;

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

        [HttpGet("{id}", Name = "StudioById")]
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

        [HttpPost]
        public IActionResult CreateStudio([FromBody] StudioForCreationDto studio)
        {
            if(studio == null)
            {
                _logger.LogError("StudioForCreationDto object sent from client is null.");
                return BadRequest("StudioForCreationDto object is null.");
            }

            var studioEntity = _mapper.Map<Studio>(studio);
            _repository.Studio.CreateStudio(studioEntity);
            _repository.Save();

            var studioToReturn = _mapper.Map<StudioDto>(studioEntity);

            return CreatedAtRoute("StudioById", new { id = studioToReturn.Id }, studioToReturn);
        }

        [HttpGet("collection", Name = "StudioCollection")] // "collection/({ids})"
        public IActionResult GetStudioCollection([FromQuery] IEnumerable<Guid> ids) // [ModelBinder(BinderType = typeof(ArrayModelBinder))]
        {
            if(ids == null)
            {
                _logger.LogError("Parameter ids is null.");
                return BadRequest("Parameter ids is null.");
            }

            var studioEntities = _repository.Studio.GetByIds(ids, trackChanges: false);

            if(ids.Count() != studioEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection.");
                return NotFound();
            }

            var studiosToReturn = _mapper.Map<IEnumerable<StudioDto>>(studioEntities);
            return Ok(studiosToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateStudioCollection([FromBody] IEnumerable<StudioForCreationDto> studioCollection)
        {
            if(studioCollection == null)
            {
                _logger.LogError("Studio collection sent from client is null.");
                return BadRequest("Studio collection is null.");
            }

            var studioEntities = _mapper.Map<IEnumerable<Studio>>(studioCollection);
            foreach(var studio in studioEntities)
            {
                _repository.Studio.CreateStudio(studio);
            }
            _repository.Save();

            var studioCollectionToReturn = _mapper.Map<IEnumerable<StudioDto>>(studioEntities);
            var ids = string.Join(",", studioCollectionToReturn.Select(s => s.Id));

            return CreatedAtRoute("StudioCollection", new { ids }, studioCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudio(Guid id)
        {
            var studio = _repository.Studio.GetStudio(id, trackChanges: false);
            if (studio == null)
            {
                _logger.LogInfo($"Studio with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _repository.Studio.DeleteStudio(studio);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudio(Guid id, [FromBody]StudioForUpdateDto studio)
        {
            if(studio == null)
            {
                _logger.LogError("StudioForUpdateDto object sent from client is null");
                return BadRequest("StudioForUpdateDto object is null");
            }

            var studioEntity = _repository.Studio.GetStudio(id, trackChanges: false);
            if (studioEntity == null)
            {
                _logger.LogInfo($"Studio with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _mapper.Map(studio, studioEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
