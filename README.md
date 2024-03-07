# ICS Project School System 
The repository contains a project for the ICS course focused on the creation of a school system.

# Repozitory Organization
```
src
├── SchoolSystem
│ ├── SchoolSystem.DAL
│ ├── SchoolSystem.DAL.Tests
│ └── SchoolSystem.Common.Tests
doc
├── Rider_ERD (ER Diagram From Rider)
├── Draft_ERD (Our Own ER Diagram Draft)
└── Draft_usecase (Our Own Draft of Usecase Diagram)
```
# Dependencies
Basic description of dependencies for correct project build:
 1. .NET8, EntityFrameworkCore  
 2. EntityFrameworkCore.Design 
 3. EntityFrameworkCore.Sqlite 
 4. EntityFrameworkCore.SqlServer 
 5. EntityFrameworkCore.Tools 
 6. Extensions.DependencyInjection.Abstractions 
 7. Extensions.Logging.Console 
 8. xUnit
For more precise information like specific NuGets versions, look in the .csproj of the individual subprojects.


# Build and Test
In subproject SchoolSystem.DAL.Tests are implemented basic tests for entities and their database operations, all tests pass correctly.

# Contributors
Tomas Dolak (xdolak09), Doubravka Simunkova (xsimun05), Jan Krumal (xkruma01), Monika Zahradnikova (xzahra33), Vojtech Teichmann (xteich02)