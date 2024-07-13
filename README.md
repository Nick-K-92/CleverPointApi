# CleverPointApi

1) Open the .sln with Visual Studio 17.10.4+

2) In the appsettings.json file you will find the Connection string to the database.
   The default connection string points to a local Instance of the MSSQL Server.
   Change this to your prefered SQL Server Instance

3) To create the Database open the Package Manager Console and run the "update-database" command.

4) Once the database is created open the SQL studio and execute the data query located in the Data/data.sql file in the solution

5) Run the solution from Visual Studio, you should get a Swagger tab on the browser

6) Enjoy!!!
