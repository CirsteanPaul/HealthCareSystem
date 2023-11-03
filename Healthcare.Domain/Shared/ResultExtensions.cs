namespace Healthcare.Domain.Shared;

public static class ResultExtensions
{
    public static T Match<T>(this Result result, Func<Result, T> onSuccess, Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess(result) : onFailure(result.Error);
    }
    
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
    }
}