# CityFinder

Code2gether June challenge: API + simple react website returns city upon user input with country & zip code

## Third party resources used

- [https://zipapi.us](https://zipapi.us)

### Study Notes

#### Critiques

- &#x2713; use `Microsoft.Extensions.Configuration` for API key instead of static class with constants
- &#x2713; static `HttpAccess` class is anti pattern, use `IHttpClientFactory` in a scoped client
- &#x2713; `location = await UsaCityFinder.GetCity(location);` is mutable. Immutable is preferred

#### Learning Journal

- `dotnet user-secrets` to store development secrets such as keys
- Use `Microsoft.Extensions.DependencyInjection`, `... .Configurations`, and `... .Options` to access development and production keys
- Practice publishing on Azure
