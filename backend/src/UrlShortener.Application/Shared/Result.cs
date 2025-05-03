namespace UrlShortener.Application.Shared;

public class Result
{
    private readonly List<string> _errors = [];

    public bool IsSuccess { get; }
    
    public IEnumerable<string> Errors => _errors;

    public ResultStatus Status { get; }

    protected Result(
        bool isSuccess,
        IEnumerable<string> errors,
        ResultStatus status)
    {
        IsSuccess = isSuccess;
        _errors.AddRange(errors);
        Status = status;
    }
}

public class Result<T> : Result where T : class
{
    public T? Content { get; }

    private Result(
        T? content,
        bool isSuccess,
        IEnumerable<string> errors,
        ResultStatus status) : base(isSuccess, errors, status) =>
        Content = content;

    public static Result<T> Success(T content) =>
        new(
            content,
            isSuccess: true,
            errors: [],
            ResultStatus.Success);

    public static Result<T> ValidationError(IEnumerable<string> errors) =>
        new(
            content: null,
            isSuccess: false,
            errors,
            ResultStatus.ValidationError);
}

public enum ResultStatus
{
    Success,
    ValidationError
}
