BCA - Bookkeeping Cloud Application
CSE682 - Invoice Management Web Application in .NET Core
Overview
BCA is a robust Invoice Management System developed using the powerful .NET Core framework and follows the MVC (Model-View-Controller) design pattern. This application facilitates users in performing CRUD operations on invoices, i.e., create, read, update, and delete. It leverages an SQL database for invoice storage, which can be accessed via a connection string specified in the appsettings.json file.

Pre-requisites
.NET SDK
SQL Server or any other database accessible via a connection string.
Visual Studio (Recommended but not mandatory)
Setup Instructions
Clone the Repository:
Clone the BCA repository from GitHub using the following command:

git clone https://github.com/adebolaibiyode/BCA

Navigate to Project Directory:
Restore the dependencies and tools of the project using the command:

dotnet restore

Update Database Connection String:
Open the appsettings.json file and update the DefaultConnection in the ConnectionStrings section with your database connection string:

"ConnectionStrings": {
  "DefaultConnection": "your_connection_string_here"
}

Run Database Migrations:
Apply the pending migrations to update the database schema using the command:

dotnet ef database update

Note: If the Entity Framework CLI is not installed, install it using the following command:

dotnet tool install --global dotnet-ef

Run the Application:
Run the application using the command:

dotnet run

After successfully running the application, open your web browser and navigate to http://localhost:5000 or https://localhost:5001.

Technology Stack
.NET Core
SQL Server
Entity Framework Core
HTML, CSS, Bootstrap
JavaScript (Optional for client-side logic)

Acknowledgement
Special thanks to everyone who contributed to the development of this application. Your effort is highly appreciated!
