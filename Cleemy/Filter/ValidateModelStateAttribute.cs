using Cleemy.Model;
using CleemyCommons.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Cleemy.ActionFilters
{
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

            if (dto is null)
                return;

            var errors = _validator.Validate(dto);

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