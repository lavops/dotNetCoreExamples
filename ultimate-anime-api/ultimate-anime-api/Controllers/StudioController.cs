using Contracts;
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

        public StudioController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetStudios()
        {
            try
            {
                var studios = _repository.Studio.GetAllStudios(trackChanges: false);

                return Ok(studios);
            } catch(Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetStudios)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }


    }
}
