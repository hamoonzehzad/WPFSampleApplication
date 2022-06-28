# WPF Sample Application

This is a Windows Presentation Foundation (WPF) sample application written in [.NET 6 ](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) using the MVVM architecture.

## Senario

The main senario of this application is to fetch 100 posts from the [{JSON} PlaceHolder](https://jsonplaceholder.typicode.com)  api and then show their ids in a 10x10 square buttons. By clicking on on of the buttons, their content will all change to post userid.

## Setup

Clone the repository to your local disk using this command:

```plaintext
git clone https://github.com/hamoonzehzad/WPFSampleApplication.git
```

Open WPFSampleApplication.sln in Visual Studio 2022 then simply build and run the apploication.

## Using the Application

The application has 3 section:

- **Home:** This is the startup view and shows a welcome message and describes the senario of application.   
- **About:** This view will show you a brief technial description about the project.
- **Posts:** This is the main business of application. It will automatically fetch the data from api and show them in 10x10 square buttons. you can change the content of buttons from id to user id by clicking on any button in the square.

You can quit the application simply by using the quit button and confirm the message box.

## Technical Description
- **Solution Structure:** There are 2 projects in the solution. Service for communicating with external resources like apis and UserInterface fro having the viewmodels, views and resource dictionaries. 
- **Configuration:** For having a json configuration file, I added a appsetting.json and configured it by using an IConfiguration (Microsoft.Extensions.Configuration) object in the app.xaml.cs. There is 2 type of configuration in appsettings.json.One for HttpClient settings and another for resiliency.
- **Dependency Injection:** .NET service provider (Microsoft.Extensions.DependencyInjection) is the service provider used in this project.
- **Navigation:** For navigating between sections I used navigation store pattern in order to keep the state of the application and bind the current viewmodel on a xaml ContentControl object.
- **Resiliency:** For having resiliency in http calls in service project I used Polly (Microsoft.Extensions.Http.Polly) and provided three diffrent strategies for resiliency immediate, linear and exponential.
- **Converters:** In the posts section we have a big border which should be responsive and also we have buttons which should calculate their width ,heigh and margin for filling the stack row with 10 button. To achieving these goals I used 2 MultiValueConverter for each senario.
- **Http Client:** I didn't use Http Client object directly because of port disposing problems instead I used HttpClientFactory and inject it into the service classes.
- **Exception Handling:** Each project have it's own custom exception classes and error messages provided by resx files. There is a global exception handling event in the app.xaml.cs for DispatcherUnhandledException and depending on which type of exception is raised the application will handle it.
- **Resource Dictionaries:** I put the styles, animations, triggers and etc in the resource dictionaries as much as I can in order to have a clean and readble xaml in the views. Also I used inheritance in styles for element such as buttons and text blocks.
- **Commands:** For handling command in MVVM architecture I used DelegateCommands from Prism because I found it simple and easy to use instead of implementing ICommand classes manually.
- **Main Window:** The main window of this project has a layout in 4 section. Title, subtitle and navigation menu are fixed as layout and the forth part is content control as a place holder of current section view. 

