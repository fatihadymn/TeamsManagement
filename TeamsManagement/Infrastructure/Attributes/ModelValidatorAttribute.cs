﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TeamsManagement.Infrastructure.Attributes
{
    public class ModelValidatorAttribute : ActionFilterAttribute
    {
        private readonly ILogger<ModelValidatorAttribute> _logger;

        public ModelValidatorAttribute(ILogger<ModelValidatorAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var firstMessage = context.ModelState.Values.Where(x => x.Errors.Count > 0)
                                                            .SelectMany(x => x.Errors)
                                                            .Select(x => new
                                                            {
                                                                Message = (!string.IsNullOrEmpty(x.ErrorMessage) || x.Exception == null) ? x.ErrorMessage : x.Exception.Message
                                                            })
                                                            .FirstOrDefault();

                _logger.LogWarning($"Model is not valid. {firstMessage?.Message}");

                context.Result = new BadRequestObjectResult(new
                {
                    message = firstMessage?.Message
                });
            }
        }
    }
}
