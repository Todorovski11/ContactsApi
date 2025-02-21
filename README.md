# ContactsApi

Setup Instructions

1️⃣ Prerequisites
.NET 8 SDK
Docker
SQL Server

2️⃣ Clone the Repository

git clone https://github.com/your-repo/ContactsApi.git

cd ContactsApi

3️⃣ Install Required NuGet Packages

dotnet restore

If you need to install missing dependencies manually, use:

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package MediatR

dotnet add package FluentValidation

dotnet add package Swashbuckle.AspNetCore



🐳 Running with Docker

1️⃣ Build the Docker Image

docker build -t contacts-api .

2️⃣ Run the Container

docker run -p 8080:8080 --name contacts-api-container contacts-api


⚡ Running the API

If not using Docker, run the API with:

dotnet run --project ContactsApi

or

cd ContactsApi

dotnet run


📜 API Documentation

Swagger UI is available at:

http://localhost:8080/index.html


🔐 Admin Authentication Required

Some API calls require Admin Authorization. These calls need a valid JWT token with admin permissions.


Endpoints that require Admin authentication:

GET /company/{id}

DELETE /country/{id}

To access them:

Login to get a token
{
  "username": "admin",
  "passwordHash": "Admin@123"
}

Include the token in the Authorization header:

Authorization: YOUR_ACCESS_TOKEN


🧪 Running Tests

dotnet test

To run tests with detailed logs:

dotnet test --logger "console;verbosity=detailed"
