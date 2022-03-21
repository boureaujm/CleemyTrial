using Cleemy.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cleemy.Controllers
{
    public class CleemyBaseController : ControllerBase
    {
        private static ApiResponse<T> CreateApiResponse<T>(T data, bool succeed = true)
        {
            return new ApiResponse<T>
            {
                Succeed = succeed,
                Result = data
            };
        }

        public OkObjectResult Ok<T>(T data)
        {
            ApiResponse<T> apiResponse = CreateApiResponse<T>(data);
            return new OkObjectResult(apiResponse);
        }

        public NotFoundObjectResult NotFound<T>(T data)
        {
            ApiResponse<T> apiResponse = CreateApiResponse(data);
            return new NotFoundObjectResult(apiResponse);
        }

        public ConflictObjectResult Conflict<T>(T data)
        {
            ApiResponse<T> apiResponse = CreateApiResponse(data);
            return new ConflictObjectResult(apiResponse);
        }

        public ObjectResult Created<T>(T data)
        {
            ApiResponse<T> apiResponse = CreateApiResponse(data);
            return new ObjectResult(apiResponse)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public BadRequestObjectResult BadRequest<T>(T data)
        {
            ApiResponse<T> apiResponse = CreateApiResponse(data, succeed: false);
            return new BadRequestObjectResult(apiResponse);
        }

        public ObjectResult InternalError<T>(T data)
        {
            ApiResponse<T> apiResponse = CreateApiResponse(data, succeed: false);
            return new ObjectResult(apiResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}