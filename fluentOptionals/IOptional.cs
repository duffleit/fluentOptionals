namespace NOptional
{
    public interface IOptional
    {
        bool IsSome { get; }
        bool IsNone { get; }
    }
}