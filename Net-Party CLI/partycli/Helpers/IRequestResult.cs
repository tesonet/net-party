namespace partycli.Helpers
{
    public interface IRequestResult<T>
    {
        bool Success { get; }
        T Result { get; }
        string ErrorMessage { get; }
    }
}
