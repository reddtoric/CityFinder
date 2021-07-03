# CityFinder

Code2gether June challenge: simple API + react website returns city upon user input with country & zip code

Project deployed at [https://app-test-one.azurewebsites.net/](https://app-test-one.azurewebsites.net/). (I use the same Azure web app project for other sandbox projects as well so City Finder may or may not be up.)

<img src="https://media.giphy.com/media/Y3jwbJB8rJUAYaH7qh/giphy.gif" alt="GIF of City Finder" />

## Frameworks/Libraries/APIs used

- [restcountries.eu API](https://restcountries.eu) (list of countries and 2 letter code)
- [zipcodebase.com API](https://zipcodebase.com) (zipcode searches)
- [zipapi.us API](https://zipapi.us) (previously used and only US zipcode searches)
- [React Google Maps API](https://www.npmjs.com/package/@react-google-maps/api)
- [Google Maps API](https://developers.google.com/maps)
- ReactJS
- ASP&#46;NET
- [Material UI](https://material-ui.com/)

## Study Notes

### Critiques

- &#x2713; use `Microsoft.Extensions.Configuration` for API key instead of static class with constants
- &#x2713; static `HttpAccess` class is anti pattern, use `IHttpClientFactory` in a scoped client
- &#x2713; `location = await UsaCityFinder.GetCity(location);` is mutable. Immutable is preferred

### Learning Journal

*I really should use TypeScript  
*Although I could send queries to third party APIs straight from the frontend, I didn't because I want to practice creating APIs.

- `dotnet user-secrets` to store development secrets such as keys
- Use `Microsoft.Extensions.DependencyInjection`, `... .Configurations`, and `... .Options` to access development and production keys
- Practice publishing on Azure
- Set up CI/CD using GitHub action workflows to deploy to Azure App service
- Configure Google Maps API access restrictions
- Set up zipcodebase.com API key in the Azure app configuration
