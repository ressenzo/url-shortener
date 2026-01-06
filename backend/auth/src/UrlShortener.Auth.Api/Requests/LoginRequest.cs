namespace UrlShortener.Auth.Api.Requests;

public record LoginRequest(
	string Email,
	string Password
);
