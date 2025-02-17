# CookBook

## About project
CookBook is a web application that enables effective management of culinary recipes. The application aims to make it easier to store and organize recipes in a database and to allow adding new recipes. Thanks to graphical interface, the application is very easy to use, allowing users to quickly find, edit and add recipes to their collection. All repository methods were tested in the project by xUnit. 

## Construction and communication of application
- Application type: **web application**
- Architecture: **monolithic (MVC)**
- Communication: **HTTP request/response**

## Technologies
- **ASP.NET Core (MVC)**
- **C#**
  - asynchronous programming (async/await)
  - MVC, Dependency Injection, DTO, Repository patterns
  - LINQ queries
  - mapping profiles
  - configuration management with file *appsettings.json*
- Database: **Microsoft SQL Server**
- ORM: **Entity Framework Core**
  - Migrations
- Unit testing: **xUnit**
- **HTML**
- **CSS**
- **JavaScript**
- **Bootstrap components**

## Features of the project
Adding and following a new recipe, editing, deleting, listing recipes, displaying recipes. Recipes are added to the MSSQL database. The current date is displayed in the navigation bar at the top.

## Images
Home page:
![Home page](photos/0.png)

Recipes page:
![Recipes page](photos/1.png)

Creating a new recipe:
![Create recipe page](photos/2.png)

Recipes after adding new one:
![Recipes page after adding new recipe](photos/3.png)

Recipe details page:
![Recipe details page](photos/4.png)

Editing recipe details:
![Edit recipe page](photos/5.png)

Deleting recipe:
![Delete recipe page](photos/6.png)

Favourite recipes after following:
![Favourite recipes page](photos/7.png)

Searched recipes:
![Searched recipes page](photos/7.2.png)

Application in mobile version: <br/>
![MV1](photos/7.3.png)

![MV2](photos/8.png)

![MV3](photos/9.png)

![MV4](photos/10.png)

![MV5](photos/11.png)

![MV6](photos/12.png)

![MV7](photos/13.png)

![MV8](photos/14.png)

Unit testing with xUnit:
![Tests](photos/15.png)
