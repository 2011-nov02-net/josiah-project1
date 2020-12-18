# Store Application

## Description

This is a simple web based application that allows you to manage several aspects of a generic store

## Technologies used
* ASP.NET Core 3.1
* Entity Framework Core
* SSMS/T-SQL
* Azure CI/CD
* Azure Web Services
* SonarCloud
* xUnit - version 2.4.0

## Features
* View customers
* Search customers
* View customer order history
* Create customer
* View locations
* View location inventory
* view location order history
* Place order at location containing any number of available products at the location

## To-do list

* Improve UI for placing orders
* Fix URL being populated with order data
* Implemented ability to add inventory to a location

## Getting Started

Live version (down at the moment): https://liberatore-training-app.azurewebsites.net/

Build it yourself:
* ```git clone https://github.com/2011-nov02-net/josiah-project1```
* migrate the code to a database using ```dotnet ef migrations add``` and ```dotnet ef database update```
* run with ```dotnet run```

## Usage

Navigate through the app by clicking on the links at the top of the page

## License
This project uses the [MIT License](https://www.mit.edu/~amini/LICENSE.md)
