

  ### Software Challenge Test

<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
  * [Installation](#installation)
* [License](#license)
* [Contact](#contact)


<!-- ABOUT THE PROJECT -->
## About The Project
![Test Image 4](https://github.com/kajal1106/TMS_API/blob/master/TMS_Microservice/images/screenshot.png)

### Built With
This section should list any major frameworks that you built your project using. Leave any add-ons/plugins for the acknowledgements section. Here are a few examples.
* [Asp.net core](https://dotnet.microsoft.com/learn/aspnet/what-is-aspnet-core)
* [Asp.net Web API](https://dotnet.microsoft.com/apps/aspnet/apis)
* [SQL Server](https://jquery.com)
* [Visual Studio 2019]()

### Installation

Follow these steps to get your development environment set up:
1. Clone the repository
```sh
git clone https://github.com/kajal1106/TMS_API.git
```
2. Change into the project directory by running:
```sh
cd Tms_Microservice
```
3. At the root directory, restore required packages by running:
```csharp
dotnet restore
```
4. Next, Go to the Nuget Package manager console and add command to install the tables into the database by migrating from the models:
```csharp
Add-migration <string>
```
5. Then, Add command  to update the database:
```csharp
Update-database
```
6. Next, build the solution by running:
```csharp
dotnet build
```
7. Next, within the project directory, launch the application by running:
```csharp
dotnet run
```
8. Launch http://localhost:5000/ in your browser to view the Web UI.

<!-- LICENSE -->
## License

* Distributed under the MIT License. [MIT License](https://en.wikipedia.org/wiki/MIT_LICENSE)
* [Swagger API](https://swagger.io/)


<!-- CONTACT -->
## Contact

Your Name - [Kajal Singh](https://www.linkedin.com/in/kajal1106) - singh.kajal940@gmail.com

Project Link: [https://github.com/kajal1106/TMS_API](https://github.com/kajal1106/TMS_API)


