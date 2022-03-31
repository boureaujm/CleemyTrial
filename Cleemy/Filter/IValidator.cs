using Cleemy.DTO;
using System.Collections.Generic;

namespace Cleemy.ActionFilters
{
    public interface IValidator<T>
    {
        IEnumerable<ErrorItemDto> Validate(T obj);
    }
}