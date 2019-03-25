Simple Calculator
-----------------

This application is structured into different projects / layers.

Web Services / REST API  - This is grouped under CalculatorAPI folder
Entity Framework / SQL Server - Data access resides at this layer. Entity Framework uses Code First approach to data access.
SimpleCalculator.Tests - All Unit Tests are in this layer
SimpleCalculatorConsole - The Console application is in this layer. The Scripts folder contain the stored procedure script.

How to run this application
===========================

Run the SimpleCalculatorConsole console application first which will create the Database ResultDB. Once this is done, you can execute Unit Tests and API's. 
Run the script uspAddResult.sql under scripts folder in SimpleCalculatorConsole project in SQL SERVER EXPRESS before running the API.

Additional Notes
-----------------
This application is developed in VS.NET 2015 and using .NET Framework 4.5.2 and database using SQL Server Express 2012.

This application uses the following external libraries and components :-

1) NUnit for Unit Testing
2) Ninject for Dependency Injection
3) Moq for Mock Testing
4) SQL Server Express



