# To-Do List Application (ASP.NET Core Razor Pages & MySQL)

This repository contains a full-stack monolithic web application built using ASP.NET Core Razor Pages, C#, and MySQL. It features complete task management functionality, including creating, viewing, toggling status, and deleting tasks.

---

## Prerequisites

Before running this application, ensure you have the following installed on your system:

1. .NET SDK (Version 6.0 or later) -> https://dotnet.microsoft.com/download
2. MySQL Server and MySQL Workbench (or phpMyAdmin) -> https://dev.mysql.com/downloads/
3. Visual Studio Code (with the C# Dev Kit extension) or Visual Studio

---

## How to Open and Run the Project

Follow these steps to set up and launch the application locally:

### 1. Clone the Repository
Open your terminal (or Command Prompt) and run the following command to download the project:
git clone https://github.com/Zayan9005/wapp-todolist-mysql.git

### 2. Set Up the Database
Open your database client (MySQL Workbench or phpMyAdmin), connect to your local MySQL server, and run the following script to create the required schema and table structure:

CREATE DATABASE IF NOT EXISTS todolistdb;
USE todolistdb;

CREATE TABLE IF NOT EXISTS todolist (
    id INT AUTO_INCREMENT PRIMARY KEY,
    task VARCHAR(255) NOT NULL,
    completed BOOLEAN NOT NULL DEFAULT FALSE,
    due_date DATE NULL
);

### 3. Update the Connection String
1. Inside the cloned project, navigate to the wapp folder.
2. Open the appsettings.json file.
3. Update the ConnectionStrings:TodoDb configuration with your actual local MySQL server username and password:

"ConnectionStrings": {
  "TodoDb": "Server=localhost;Port=3306;Database=todolistdb;User ID=YOUR_MYSQL_USERNAME;Password=YOUR_MYSQL_PASSWORD;"
}

### 4. Build and Launch the Application
1. Open the project folder in Visual Studio Code.
2. Open a New Terminal inside VS Code (Ctrl + `) and ensure your path is inside the main wapp folder directory.
3. Run the following command to restore dependencies, compile, and start the application development engine:
dotnet watch

### 5. Access the Web Page
Once the build is complete, your terminal will provide a local hosting URL. Open your web browser and navigate to:


(Note: Your system port number might vary slightly depending on your local network configuration environment. Check your terminal output window for the exact port address.)
