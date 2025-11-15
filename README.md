A simple Task Tracking REST API built with ASP.NET Core, Entity Framework Core, and SQL Server.

This is an early-stage backend project with only two working features at the moment.

✔️ Current Functionality

✅ 1. Register a User

Allows creation of a new user with:

Username

Password (with BCrypt hashing )

The user is then saved to the database.

✅ 2. Get All Users

Returns a list of all users currently stored in the database.

🚧 Planned Improvements

These features are not implemented yet but are planned:

🔐 Authentication & Security

Salting passwords

Add login endpoint

Generate JWT tokens

Protect routes using authorization

🛢️ Database Enhancements

Add additional fields (CreatedAt, roles, etc.)

Improve validation

🌐 Expanded API Functionality

Update user

Delete user

Create Task

Update Task

Add profile data

Add pagination for retrieving users


🛠 Tech Stack
C#

ASP.NET Core Web API

Entity Framework Core

SQL Server (LocalDB)

▶️ How to Run
--------------------------------------------------
1.Clone the repository

2.Open the solution in Visual Studio

3.Update the connection string in appsettings.json

4.Run the project

Swagger UI will appear at:

https://localhost:{port}/swagger/index.html
