RestfulObjects-WSA
==================

This project consists of:

* A Windows Store App (metro/modern UI) to act as a generic viewer for Restful Objects
* a Restful Objects applib for accessing Restful Objects web services

For testing purposes it also includes a Restful Objects app hosting a domain object model for the AdventureWorks (2005) example database


## Solutions and Projects

The codebase consists of:

* `RestfulObjects.Applib` solution
   - the client-side API to the Restful Objects webservice
   - `RestfulObjects.ApplibPCL`
      - an application library for accessing RestfulObjects APIs
         - is a .NET portable client library
         - defines the `RoClient` interface
   - `RestfulObjects.Applib.WinRT`
      - implementation of `RoClient` interface for Windows RT
   - `RestfulObjects.Applib.Restsharp`
      - implementation of `RoClient` interface for .NET 4+
   - supporting unit and integration tests

* `Prism` projects
   - copies of [Prism for WinRT](http://prismwindowsruntime.codeplex.com/releases), Final Stable release (Apr 24 2013)
     - `Prism/Prism.PubSubEvents`
     - `Prism/Prism.StoreApps`
   - provided by Microsoft Pattern & Practices   

* `Client` solution
   - the `Prism` projects
   - the `RestfulObjects.applib` projects
   - `RestfulObjects.WSA`, the generic client
      - references the RestfulObjects and Prism projects

* `Server` solution
   - `AdventureWorks` domain object model project
      - downloaded from [NO website](http://nakedobjects.codeplex.com/releases/view/100899) ([zip file](http://nakedobjects.codeplex.com/downloads/get/609253))
   - `RunRestfulObjectsServer` project (running RO 1.1.0)
      - runs RO web service, port 9292
   - `RunMVC` project
      - runs NO MVC webapp, port 8282


## Prereqs

### Dev env
- Visual Studio 2012, or 2010 with Service Pack 1 installed.
- NuGet 1.6+
  - upgraded VS2012 to NuGet 2.2
- ASP.NET MVC 4
  - I think is built into VS2012; anyway, is there in File>New Project
- Tools > Options > Package Manager > Allow Nuget to download missing packages during build


### Developer license

To deploy/run WSA apps locally, you require a (free) developer license.  This in turn requires a (free) Microsoft account.

If you do not have a Microsoft account, then sign up for one [here](https://signup.live.com/signup.aspx?lic=1).



### SQL Express or SQL Server

* if SQL Express
  - [Microsoft .NET Framework 3.5 Service pack 1 (Full Package)](http://www.microsoft.com/en-gb/download/details.aspx?id=25150)
  - DB engine: [`SQLExpress2008R2 SP2`](http://www.microsoft.com/en-gb/download/details.aspx?id=30438)
  - Tools: [`SQLEXPRWT_x64_ENU.exe`](http://download.microsoft.com/download/0/4/B/04BE03CD-EAF3-4797-9D8D-2E08E316C998/SQLEXPRWT_x64_ENU.exe)

### AdventureWorks database
- [AdventureWorks 2005 version](http://msftdbprodsamples.codeplex.com/releases/view/4004)
- [AdventureWorksDB.msi](http://msftdbprodsamples.codeplex.com/downloads/get/11753)
- install `AdventureWorksDB.msi`
  - to `C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Data`
  - in MS SQL Studio, attach to the AdventureWorks.mdf

### AW Domain model

The domain model itself is part of the `Server` project, see above.

## UI Design Sketch for RestfulObjects.WSA

The following are the original sketches as to how the application might evolve.

![](https://github.com/danhaywood/restfulobjects-wsa/blob/master/specs/mockups/Slide1.PNG?raw=true)

![](https://github.com/danhaywood/restfulobjects-wsa/blob/master/specs/mockups/Slide2.PNG?raw=true)

## Stories

A number of stories that build upon the original UI design sketches are available [here](https://github.com/danhaywood/restfulobjects-wsa/tree/master/specs/stories).  


## Building/Deploying the Code

### Dev license

When performing a deploy, a (free) developers license is required.  This in turn requires a (free) Microsoft account (see prereqs, above).

Details and screenshots on the dev license prompt are [here](http://www.c-sharpcorner.com/UploadFile/7e39ca/how-to-renew-developer-license-for-windows-store-apps/)


### Workaround to the failure to delete `resources.pri` file

If do a full clean/rebuild/deploy, the deploy always fails:

- initially with an error 'operation cannot be performed on a file with a user-mapped section open'
- then (if attempt again) with an error indicating that the file `bin\debug\AppX\resources.pri` file could not be deleted.

However, this file *can* be deleted manually from Windows Explorer if shift-delete (force delete); the deploy should then succeed.

It's worth keeping Windows Explorer open at this location.

## Implementation Notes

### Client

The client was initially copied from the Prism guidance's HelloWorldWithContainer quickstart.  Then, bits and pieces from the Prism reference implementation (AWShopper) have been incorporated.

When installing Json.Net, had to set copyLocal=false

### Server

#### RunMVC

is ready to run (on port 8282)

#### Configuring RO (the `RunRestfulObjectsServer` project)

is ready to run (on port 9292)

was created as follows:

- new project: ASP.Net Empty Web app
- updated Web.Config
  - copy `ConnectionString` for Model from `RunMVC` to `RunRestfulObjectsServer`'s
- added reference to `AdventureWorksModel`
- copied from RunMVC's RunWeb class
  - MenuServices, ContributedActions, Persistor
- Project Properties > Web > run on port 9292
- set as startup project


