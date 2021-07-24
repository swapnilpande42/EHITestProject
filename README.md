# EHITestProject
This project demonstrates use of Asp.Net MVC core CRUD operations using async repository pattern and microsoft DI.

# Organization : swapnilpande42

# Project Structure
The project has below structure;<br/><br/>
1. **EHI.sln** : Solution file of the entire project <br/>
2. **EHI.Web** : Contains MVC core templates (Controller, View, Model) to Create, Edit, Display and Delete Customer information <br/>
3. **EHI.Domain** : Contains Entities and Contracts required for Dependency injection and Entity framework Code first approach. Also it contains **Handler** folder which has some helper method for create, edit, delete customer API's. <br/>
4. **EHI.Repository** : Contains classes required for implementation of repository pattern. It also contains DbContext class written for Code first implementation using EF core.
5. **EHI.UnitTest** : Unit test case project. It has controller level unit test cases implemented <br/><br/>

# How to run
To run the project follow below steps;<br/>
1. Download code and open EHI.sln in Visual studio 19. Make sure it has .Net Core 3.1 installed <br/>
2. Under **appsettings.json**, provide valid database connection string under ConnectionStrings -> customerDb <br/>
3. Open PackageManager console. Make sure you have **EHI.Repository** selected. <br/>
4. Run migration queries to create database. run **add-migration {migrationName}** command. This should setup initial migration. <br/>
5. Once above step executes completely, run **update-database {migrationName}** command. This should create database. Verify usinf SQL server management studio if database is created. <br/>
6. Run project from Visual studio. This should open Customer list view. <br/>
7. Play along few actions such as list, create, delete, details to verify. <br/>

# Design patterns and priciples used
This project uses built in dependency injection feature provided by microsoft along with asyn repository pattern to be used with entity framework unit of work. Also it has some unit test cases written for controller actions using xUnit testing framework. Use "Run test" option under "Test" menu to run unit test cases.


