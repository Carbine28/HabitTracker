# HabitTracker

A simple C# applicaton to track the amount of hour blocks spent coding.
The application uses basic CRUD operations on an SQLite database. Information is displayed on the Console.

# Features

* SQLite database connection
  - A SQLite db connection is used to store, and read information
  - A database is created if it does not exist

* Console UI for navigation through number inputs
  - <img src="https://github.com/Carbine28/HabitTracker/blob/master/extra/consoleUI.png" alt="console UI menu"/>

* CRUD operation on DB
  - Users can Create, Read, Update and Delete records found within the DB.
  - Dates are entered in the format (dd/M/yy)
  - Quanity can be any positive integer.
  - 
* Unit Testing
  - Incorporated unit testing with SQLite to test CRUD operations on test Db

# Installation
Download the repo as a zip and extract.
1. Open the `HabitTracker.sln`
2. Build the project to run.
 
# Key Points
- Implemented connection based SQLite databased using <a href="https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli">Microsoft.Data.Sqlite</a>
- Implemented Simple <a herf="https://en.wikipedia.org/wiki/Create,_read,_update_and_delete">CRUD</a> operations with SQLite
