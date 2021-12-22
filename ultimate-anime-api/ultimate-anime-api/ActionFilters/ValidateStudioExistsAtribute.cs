using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace ultimate_anime_api.ActionFilters
{
    public class ValidateStudioExistsAtribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateStudioExistsAtribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var studio = await _repository.Studio.GetStudio(id, trackChanges);

            if(studio == null)
            {
                _logger.LogInfo($"Studio with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            } else
            {
                context.HttpContext.Items.Add("studio", studio);
                await next();
            }
        }
    }
}
