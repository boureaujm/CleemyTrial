using Cleemy.DTO;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;

namespace Cleemy.Model.Adapter
{
    public class SortWrapperDtoToSortWrapperAdapter : IAdapter<SortWrapperDto, SortWrapper>
    {
        public SortWrapper Convert(SortWrapperDto source)
        {
            if (source == null)
                return null;

            return new SortWrapper
            {
                Direction = source.Direction,
                Field = source.Field,
            };
        }
    }
}