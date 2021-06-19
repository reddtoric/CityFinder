# CityFinder

Code2gether June challenge: simple API + react website returns city upon user input with country & zip code

Project deployed at [https://app-test-one.azurewebsites.net/](https://app-test-one.azurewebsites.net/). (I use the same Azure web app project for other sandbox projects as well so City Finder may or may not be up.)

## Frameworks/Libraries/APIs used

- [zipapi.us API](https://zipapi.us) (US zipcode searches)
- [React Google Maps API](https://www.npmjs.com/package/@react-google-maps/api)
- [Google Maps API](https://developers.google.com/maps)
- ReactJS
- ASP&#46;NET

## Study Notes

### Critiques

- &#x2713; use `Microsoft.Extensions.Configuration` for API key instead of static class with constants
- &#x2713; static `HttpAccess` class is anti pattern, use `IHttpClientFactory` in a scoped client
- &#x2713; `location = await UsaCityFinder.GetCity(location);` is mutable. Immutable is preferred

### Learning Journal

- `dotnet user-secrets` to store development secrets such as keys
- Use `Microsoft.Extensions.DependencyInjection`, `... .Configurations`, and `... .Options` to access development and production keys
- Practice publishing on Azure
- Setup CI/CD GitHub Actions for Azure
- Setup API access restrictions