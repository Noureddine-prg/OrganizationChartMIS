# Organization Chart Application

This application utilizes the Telerik Kendo UI for displaying and managing an organizational chart. It is built on ASP.NET Core with a local SQL Server database.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [Telerik UI](https://www.telerik.com/aspnet-core-ui/orgchart) 
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) or higher
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- Visual Studio 2019 or later

### Installation & Setup

1. Clone the Repository:

   ** git clone https://github.com/Noureddine-prg/OrganizationChartMIS.git **

2. Restore NuGet Packages: 

	Ensure you have added Telerik's NuGet feed to your Visual Studio as described below.

3. Set Up the Telerik NuGet Feed:

	- Go to Tools > NuGet Package Manager > Package Manager Settings.
	- Under Package Sources, add a new source:
		- Name: Telerik
		- Source: https://nuget.telerik.com/v3/index.json
	
		- Under Package Source Credentials, add your Telerik credentials:

NuGet.config file. Create if it doesn't appear. 	
 ```
 <packageSourceCredentials>
  <telerik.com>
    <add key="Username" value="[your.telerik.com@email.login]" />
    <add key="ClearTextPassword" value="[your.telerik.com.password.in.clear.text]" />
  </telerik.com>
</packageSourceCredentials>
```
4. Configure Connection String for SQL DB. (appsettings.json)

"ConnectionStrings": {
  "OrgMISConnection": "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=OrgMIS;Integrated Security=True;Pooling=False"
}

5. Add Kendo UI license to www.root/js directory

6. Run the Create Tables queries in the DBQuery folder to get DB tables. Sample data can be added as well in (Test Queries); can be added on your own. 
