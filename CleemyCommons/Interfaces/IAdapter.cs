namespace CleemyCommons.Interfaces
{
    public interface IAdapter<S, D>
    {
        D Convert(S source);
    }
}
