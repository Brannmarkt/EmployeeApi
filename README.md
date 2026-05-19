# Employee Service

This repository contains a sample REST API built with C# that interacts with a Microsoft SQL database table named "Employee".

## Clone the repository

```bash
git clone https://github.com/Brannmarkt/EmployeeApi.git
```

## Database Setup

1) Open SQL Server Management Studio.

2) Open and execute the SQL script located at EmployeeService/Database/Init.sql to create the TestDb database, the Employee table and seed it with test data.

3) Open the Web.config file in the project.

4) Locate the <connectionStrings> section and update the "DbServer" connection string to match your local SQL Server instance name.

## Local Running
1) Open the .sln file in Visual Studio.

2) Set EmployeeService project as startup project.

3) Run the application

## Testing
You can test the API using Postman. Replace <PORT> with your own.

1) Get Employee Hierarchy (GET)
Returns the employee and all their subordinates in a JSON.

```bash
http://localhost:<PORT>/EmployeeService.svc/GetEmployeeById?id={id}
```

2) Change Employee Status (PUT)
Updates the Enable property of an employee.

```bash
http://localhost:<PORT>/EmployeeService.svc/EnableEmployee?id=3
```

Body (Raw JSON):

```bash
{
    "enable": 0
}
```
