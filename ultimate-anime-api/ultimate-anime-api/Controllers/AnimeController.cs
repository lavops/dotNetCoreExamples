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
    [Route("api/studio/{studioId}/anime")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AnimeController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetAnimeForStudio(Guid studioId)
        {
            var studio = _repository.Studio.GetStudio(studioId, trackChanges: false);
            if(studio == null)
            {
                _logger.LogInfo($"Studio with id: {studioId} doesn't exist in the database");
                return NotFound();
            } else
            {
                var anime = _repository.Anime.GetAnimes(studioId, trackChanges: false);
                var animeDto = _mapper.Map<IEnumerable<AnimeDto>>(anime);

                return Ok(animeDto);
            }
        }
    }
}
