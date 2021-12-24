using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace ultimate_anime_api.ActionFilters
{
    public class ValidateAnimeForStudioExistsAtribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateAnimeForStudioExistsAtribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;

            var studioId = (Guid)context.ActionArguments["studioId"];
            var studio = await _repository.Studio.GetStudio(studioId, false);

            if(studio == null)
            {
                _logger.LogInfo($"Studio with id: {studioId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            var id = (Guid)context.ActionArguments["id"];
            var anime = await _repository.Anime.GetAnime(studioId, id, trackChanges);

            if(anime == null)
            {
                _logger.LogInfo($"Anime with id: {studioId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            } else
            {
                context.HttpContext.Items.Add("anime", anime);
                await next();
            }
        }
    }
}
