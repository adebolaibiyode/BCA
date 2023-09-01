# BCA
Bookkeeping cloud application - CSE682

Invoice Management Web Application in .NET Core
Overview
This is a simple Invoice Management System developed using .NET Core and MVC (Model-View-Controller) design pattern. The application enables users to create, read, update, and delete invoices. It uses a SQL database for storing invoices, and the database is accessed via a connection string defined in the appsettings.json file.

Pre-requisites
.NET SDK
SQL Server or any other database that you can connect via a connection string.
Visual Studio (Optional, but recommended)

Setup Instructions
1. Clone the repository
git clone https://github.com/adebolaibiyode/BCA

2. Navigate to Project Directory
dotnet restore

3. Update Database Connection String
Open appsettings.json and replace the ConnectionString in:
json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "your_connection_string_here"
}

4. Run Database Migrations
dotnet ef database update
Note: If the Entity Framework CLI is not installed, you can install it using the following command:
dotnet tool install --global dotnet-ef

5. Run the application
dotnet run

After running the application, navigate to http://localhost:5000 or https://localhost:5001 in your web browser.

Technology Stack
.NET Core
SQL Server
Entity Framework Core
HTML, CSS, Bootstrap
JavaScript (optional for client-side logic)