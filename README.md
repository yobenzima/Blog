<h1 align="center">ASP.NET Core Blog</h1>

<p align="center">Blog Project Created in ASP.NET Core</p>

## Links
https://github.com/yobenzima/Blog

## Screenshots


## Brief Introduction
The application is built using ASP.NET Core 5. 
This is both the API and front end. 
The front end connects to the REST API using HttpClient calls.
Microsoft SQL Server is used for the back-end database. 

## Project Structure
There are 5 projects that make up the application. All these projects would have easily been separated by a folder structure.
1. Blog.API - this project contains the controllers that serve the data to the web application
2. Blog.Contracts - this project defines the contracts/interfaces
3. Blog.Entities - this project contains the business models
4. Blog.Repository - this project implements contracts / interfaces by acting as a connection layer to the underlying Microsoft SQL Server database. The project also provides a single RepositoryWrapper class to provide a single entry point for all (future) repository objects. 
This is injected into the services using a one liner: services.AddScoped<IRepositoryWrapper, RepositoryWrapper>() as opposed to injecting all repositories separately
5. Blog.Web - this is the front end web application that users interract with

The structure was preferred because it breaks down the project into smaller pieces. This would come in handy in several ways. Some of which are:
1. Different smaller teams or individuals can work separately on the sub-projects making integration easier.
2. The separation lays good ground work for bigger projects - the smaller sub-projects can be developed and tested seperately as they grow.
3. It introduces loose coupling e.g. the Blog.API only uses one reference - Blog.Repository. References to Blog.Contracts and Blog.Entities are resolved through one reference to Blog.Repository.

## Third Party Libraries
1. Bootstrap - used for styling the user interface
2. Automapper - used to map Models to DataTransfer objects. The idea was to provide a clear separation between models and objects that provide the data to the user interface. This helps control what fields are displayed and changes to the models / API (say new fields are added) do not affect the front end.

## Build Instructions
1. Open solution in Visual Studio 2019 or later
2. Restore Nuget Packages
3. Build solution
4. Deploy REST webservice to (IIS)
5. In the ContactController and PostController controllers in Blog.Web project change the "http://localhost:8085" in mAPIBaseUrl variable to point to the url where REST API is deployed. 
   NOTE: The "/api" part must be left intact

## Built With
- ASP.NET Core
- .NET C#
- Entity Framework Core
- Microsoft SQL Server
- HTML 5
- Bootstrap
- Automapper 

## Future / To Do List
- Add proper styling in front end
- Implement Authentication and Authorisation - to only allow Admin to create posts (now anyone can create and read posts)
- Create separate user interfaces using React/Angular/VueJS or any other front end User Interface library
- Configure BaseUrl (mAPIBaseUrl) in appsettings.json. This is now hard-coded into the controllers
