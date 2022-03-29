using System.Collections.Generic;

namespace CleemyCommons.Model
{
    public class ErrorsDto
    {
        public IEnumerable<ErrorItemDto> Errors { get; set; }
    }
}