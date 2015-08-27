# Introduction

There is an overhead starting a new N-tier project and the purpose of this repository is to solve that. 

This project is based on the ideas of [Christos S](http://en.gravatar.com/chsakell) and his article [ASP.NET MVC Solution Architecture – Best Practices](http://chsakell.com/2015/02/15/asp-net-mvc-solution-architecture-best-practices/).

## Getting Started
- Clone the project
- Right click on the Project.Web and set that as your default project.

## Renaming the Project

- Rename the solution
- Rename each project
- Go into the properties of each project and change the assembly name. (use Resharper or something similar to fix all the namespaces)
- Rename ProjectContext.cs and ProjectSeedData.cs class (in Data project)
- (optional) Change connection string name
	- Rename ConnectionString name in ProjectContext.cs class (may have renamed the class by this point)
	- Update web.config in the web project.
- More instruction coming soon.

## TODO
- Add the web project with Form based authentication
- Look into setting up WebApi with Token based authentication

## Troubleshooting
- If you get any dll errors, try cleaning the solution, check the bin folder for any old dlls and manually delete if required. Build and Try again.