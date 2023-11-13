# ASPNET_Core7_WebAPI_JWT
ASP .NET Core Project for learning. Every one should be able to use this templae to build a ASP .NET Core web API with PostgreSQL database and Security with JWT.
In this example will show about:
1. Authentication and Register
2. Role Base Permission
3. Policy Base Permission
4. Custom Policy Base Permission
5. Claim Base Permission

### Key Functions 
1. Security with JWT
2. Entity Framework Core
3. Swagger for API's Endpoint
4. API-Versioning
5. Serial-Log
6. AutoMapper

## Getting Started
These instructions will get you to setup the project, install sdk and add package (CLI or Package manager console).

### Prerequisites
- Visual Studio 2022 or higher 
- .NET 7.x SDK  
- Npgsql.EntityFrameworkCore.PostgreSQL 7.0.11 (https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/)

### Installing
1.  Install .net SDK 7<br>
[Download .NET SDK here.](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
2.  Create new Web API's project<br>
`dotnet new webapi –-name ASPNET_Core7_WebAPI_JWT`
3.  Add package
     - Authentication JWT
       `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`
     - Entity Framework Core 7.0.13<br>
       `dotnet add package Microsoft.EntityFrameworkCore`<br>
       `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`
     - API-Versioning
       `dotnet add package Asp.Versioning.Mvc.ApiExplorer`
     - Serial-Log
       `dotnet add package Serilog.AspNetCore`
     - AutoMapper
       `dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection`
5.  Migrate Model to Database<br>
     - Command Line<br>
      Add Migration `dotnet ef migrations add AddStudentToDB`<br>
      Update to DB `dotnet ef database update`
     - Package Manager Console<br>
      Add Migration`add-migration AddStudentToDB`<br>
      Update to DB `update-database`
## Languages and Tools
<div>
  <img src="https://github.com/devicons/devicon/blob/master/icons/dotnetcore/dotnetcore-original.svg" title="dotnet core" alt="dotnet core" width="40" height="40"/>&nbsp;
  <img src="https://codeopinion.com/wp-content/uploads/2017/10/Bitmap-MEDIUM_Entity-Framework-Core-Logo_2colors_Square_Boxed_RGB.png" title="dotnet core" alt="dotnet core" width="40" height="40"/>&nbsp;
  <img src="https://github.com/devicons/devicon/blob/master/icons/csharp/csharp-original.svg" title="csharp" alt="csharp" width="40" height="40"/>&nbsp;
  <img src="https://github.com/devicons/devicon/blob/master/icons/postgresql/postgresql-original.svg" title="postgresql" alt="postgresql" width="40" height="40"/>&nbsp;
  <img src="https://play-lh.googleusercontent.com/3C-hB-KWoyWzZjUnRsXUPu-bqB3HUHARMLjUe9OmPoHa6dQdtJNW30VrvwQ1m7Pln3A" title="jwt" alt="jwt" width="40" height="40"/>&nbsp;
</div>
