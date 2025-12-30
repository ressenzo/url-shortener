namespace UrlShortener.Generator.Domain.Entities;

public abstract class Entity(string? id)
{
	private readonly List<string> _errors = [];

	public string Id { get; set; } = id ?? Guid.NewGuid().ToString()[..8];

	public IEnumerable<string> Errors => _errors;

	public abstract bool IsValid();

	protected void AddError(string error) =>
		_errors.Add(error);
}
