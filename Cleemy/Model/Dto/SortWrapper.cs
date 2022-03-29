using Microsoft.AspNetCore.Mvc;

namespace Cleemy.DTO
{
    public class SortWrapperDto
    {
        [FromQuery(Name = "field")]
        public string Field { get; set; }

        [FromQuery(Name = "direction")]
        public string Direction { get; set; }
    }
}