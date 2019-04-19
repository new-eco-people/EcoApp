# Introduction 

This is the Asp.net core version using clean architecture. This structure was fully inspired by [JasonGT](https://www.youtube.com/watch?v=Zygw4UAxCdg&t=1301s) and the source code is on [GitHub](https://github.com/JasonGT/NorthwindTraders).


Each Folder contains a ReadMe.md where applicable (because some folder are auto generated thus deleting the ReadMe.md) to explain the purpose of that folder.

As such please feel free to ask me any question where you find confusing.

In the future I hope to integrete Docker to the application for easy Intergration and Delivery.

## Getting Started
Follow these instructions to get the project up and running

### Prerequisites
The following tools are required:

* [Visual Studio Code](https://code.visualstudio.com/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)
* SQL Server for the Database (Default) or any Database of your choice (You need to download the Nuget Package for that Database and configure the Persistence/Extensions/DatabaseExtension.cs file on line 15).

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Create new migration
     ```
     dotnet ef migrations add init
     ```
  5. Create or update database
     ```
     dotnet ef database update
     ```
  6. For front-end users, first install the node_modules into the Web.Ui/ClientApp folder. So move into the ClientApp and run
     ```
     npm install
     ```
  7. Move into the Web.Ui folder
     ```
     dotnet watch run
     ```

Happy Coding...

Cheers Excellence

