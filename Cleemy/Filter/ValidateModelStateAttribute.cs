using Cleemy.Configuration;
using Cleemy.DTO;
using Cleemy.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Cleemy.ActionFilters
{
    /// <summary>
    /// A solution for emulate fluentValidation validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidateModelStateAttribute<T> : IActionFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidateModelStateAttribute(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var param = actionContext.ActionArguments.SingleOrDefault(p => p.Value is T);

            var dto = (T)param.Value;

            IEnumerable<ErrorItemDto> errors;
            if (dto != null)
            {
                errors = _validator.Validate(dto);
            }
            else
            {
                errors = new List<ErrorItemDto> { new ErrorItemDto {
                    Reason = Constants.CST_MESSAGE_INVALID_FORMAT
                    }
                };
            }

            var response = new ApiResponse<ErrorsDto>
            {
                Succeed = false,
                Result = new ErrorsDto
                {
                    Errors = errors
                }
            };

            if (errors.Count() > 0)
                actionContext.Result = new BadRequestObjectResult(response);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}