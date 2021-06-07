# Critique

- use `Microsoft.Extensions.Configuration` for API key instead of static class with constants
- static `HttpAccess` class is anti pattern, use `IHttpClientFactory` in a scoped client
- `location = await UsaCityFinder.GetCity(location);` is mutable. Immutable is preferred
