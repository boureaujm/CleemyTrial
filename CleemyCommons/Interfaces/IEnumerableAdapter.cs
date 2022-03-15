using System.Collections.Generic;

namespace CleemyCommons.Interfaces
{
    public interface IEnumerableAdapter<S, D>
    {
        D Convert(S source);
        IEnumerable<D> Convert(IList<S> list);
    }
}
