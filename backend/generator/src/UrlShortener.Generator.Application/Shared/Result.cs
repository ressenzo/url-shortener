using System.Text.Json.Serialization;

namespace UrlShortener.Generator.Application.Shared;

public abstract class BaseResult
{
	private readonly List<string> _errors = [];

	[JsonIgnore]
	public bool IsSuccess { get; }

	public IEnumerable<string> Errors => _errors;

	[JsonIgnore]
	public ResultStatus Status { get; }

	protected BaseResult(
		bool isSuccess,
		IEnumerable<string> errors,
		ResultStatus status)
	{
		IsSuccess = isSuccess;
		_errors.AddRange(errors);
		Status = status;
	}
}

public class Result : BaseResult
{
	private Result(
		bool isSuccess,
		IEnumerable<string> errors,
		ResultStatus status)
			: base(isSuccess, errors, status)
	{ }

	public static Result InternalServerError() =>
		new(
			isSuccess: false,
			errors: ["An error has happened while processing request"],
			status: ResultStatus.ValidationError);
}

public class Result<T> : BaseResult where T : class
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

	public static Result<T> ValidationError(string error) =>
		new(
			content: null,
			isSuccess: false,
			[error],
			ResultStatus.ValidationError);

	public static Result<T> NotFound(string error) =>
		new(
			content: null,
			isSuccess: false,
			[error],
			ResultStatus.NotFound);
}

public enum ResultStatus
{
	Success,
	ValidationError,
	InternalServerError,
	NotFound
}
