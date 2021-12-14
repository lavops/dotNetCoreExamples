﻿using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "GetAnimeForStudio")]
        public IActionResult GetAnimeForStudio(Guid studioId, Guid id)
        {
            var studio = _repository.Studio.GetStudio(studioId, trackChanges: false);
            if (studio == null)
            {
                _logger.LogInfo($"Studio with id: {studioId} doesn't exist in the database");
                return NotFound();
            }
            else
            {
                var anime = _repository.Anime.GetAnime(studioId, id, trackChanges: false);
                if(anime == null)
                {
                    _logger.LogInfo($"Anime with id: {id} doesn't exist in the database");
                    return NotFound();
                }
                var animeDto = _mapper.Map<AnimeDto>(anime);

                return Ok(animeDto);
            }
        }

        [HttpPost]
        public IActionResult CreateAnimeForStudio(Guid studioId, AnimeForCreationDto anime)
        {
            if(anime == null)
            {
                _logger.LogError("AnimeForCreationDto object sent from client is null.");
                return BadRequest("AnimeForCreationDto object is null.");
            }

            var studio = _repository.Studio.GetStudio(studioId, trackChanges: false);
            if (studio == null)
            {
                _logger.LogInfo($"Studio with id: {studioId} doesn't exist in the database");
                return NotFound();
            }

            var animeEntity = _mapper.Map<Anime>(anime);
            _repository.Anime.CreateAnimeForStudio(studioId, animeEntity);
            _repository.Save();

            var animeToReturn = _mapper.Map<AnimeDto>(animeEntity);
            return CreatedAtRoute("GetAnimeForStudio", new { studioId, id = animeToReturn.Id }, animeToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimeForStudio(Guid studioId, Guid id)
        {
            var studio = _repository.Studio.GetStudio(studioId, trackChanges: false);
            if (studio == null)
            {
                _logger.LogInfo($"Studio with id: {studioId} doesn't exist in the database");
                return NotFound();
            }

            var anime = _repository.Anime.GetAnime(studioId, id, trackChanges: false);
            if (anime == null)
            {
                _logger.LogInfo($"Anime with id: {id} doesn't exist in the database");
                return NotFound();
            }
            _repository.Anime.DeleteAnime(anime);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimeForStudio(Guid studioId, Guid id, [FromBody]AnimeForUpdateDto anime)
        {
            if(anime == null)
            {
                _logger.LogError("AnimeForUpdateDto object sent from client is null");
                return BadRequest("AnimeForUpdateDto object is null");
            }

            var studio = _repository.Studio.GetStudio(studioId, trackChanges: false);
            if (studio == null)
            {
                _logger.LogInfo($"Studio with id: {studioId} doesn't exist in the database");
                return NotFound();
            }

            var animeEntity = _repository.Anime.GetAnime(studioId, id, trackChanges: true);
            if (animeEntity == null)
            {
                _logger.LogInfo($"Anime with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _mapper.Map(anime, animeEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateAnimeForCompany(Guid studioId, Guid id, [FromBody]JsonPatchDocument<AnimeForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null");
                return BadRequest("patchDoc object is null");
            }

            var studio = _repository.Studio.GetStudio(studioId, trackChanges: false);
            if (studio == null)
            {
                _logger.LogInfo($"Studio with id: {studioId} doesn't exist in the database");
                return NotFound();
            }

            var animeEntity = _repository.Anime.GetAnime(studioId, id, trackChanges: true);
            if (animeEntity == null)
            {
                _logger.LogInfo($"Anime with id: {id} doesn't exist in the database");
                return NotFound();
            }

            var animeToPatch = _mapper.Map<AnimeForUpdateDto>(animeEntity);
            patchDoc.ApplyTo(animeToPatch);
            _mapper.Map(animeToPatch, animeEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
