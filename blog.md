# Simplified File Management for Azure Mobile Apps

We have introduced some enhancements to the .NET file management client that
simplifies what you need to do to take advantage of file management with
offline sync.

## Introducing the "Express" Client API

The "Express" client API essentially provides you with some simple defaults
that mean you don't need to implement any of the usually required interfaces,
such as `IFileSyncHandler` and `IFileDataSource`. It saves files to a location
on the local device's file system and keeps them in sync as changes are
made. You save and access these files using `Stream` objects.

### Setting Up

First up, create yourself a simple Azure Mobile Apps server. If you want to use
the Node.js backend, follow the steps [here](https://github.com/Azure/azure-mobile-apps-node-files),
or for the .NET backend, follow the steps [here](https://azure.microsoft.com/en-us/blog/file-management-with-azure-mobile-apps/#creating-the-server).

Next, create yourself a Windows or Xamarin Azure Mobile Apps project. If you
want a completed application to follow along with, check out the sample app
[here](https://github.com/danderson00/app-service-mobile-dotnet-todo-list-files/tree/master/src/client/MobileAppsFilesSample). Most of the file management specific code is in the
`TodoItemManager` class [here](https://github.com/danderson00/app-service-mobile-dotnet-todo-list-files/blob/master/src/client/MobileAppsFilesSample/TodoItemManager.cs).

All of the functionality is exposed by extension methods on the Azure Mobile Apps
client and table classes. To make the extension methods available, add the
following using statement to the top of your class:

    using Microsoft.WindowsAzure.MobileServices.Files.Express;

### Initialization

Before you can use file management functions, you must first initialize the package:

``` cs
var client = new MobileServiceClient("https://your-mobile-app.azurewebsites.net/");
var store = new MobileServiceSQLiteStore("localstore.db");
store.DefineTable<TodoItem>();
client.InitializeManagedFileSyncContext(store);
await client.SyncContext.InitializeAsync(store, StoreTrackingOptions.AllNotificationsAndChangeDetection);
```
