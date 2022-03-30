using System.Collections.Generic;

namespace Cleemy.DTO
{
    public class ErrorsDto
    {
        public IEnumerable<ErrorItemDto> Errors { get; set; }
    }
}