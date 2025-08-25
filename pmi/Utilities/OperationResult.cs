namespace pmi.Utilities;

public class OperationResult<T>
{
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }

    public T? Entity { get; private set; }

    public static OperationResult<T> Failure(string error) => new OperationResult<T> { Success = false, ErrorMessage = error };
    public static OperationResult<T> Successful(T obj) => new OperationResult<T> { Success = true, Entity = obj };
}