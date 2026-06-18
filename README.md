# WAPP Student Portal

WAPP Student Portal is an ASP.NET Core Razor Pages web application developed for a Web Applications module. The project includes user registration, login authentication, student profile pages, course registration, grade calculation, feedback handling, and a MySQL-connected to-do list system.

## Features

* Home page with student portal navigation
* User registration system
* Login and logout functionality
* Session-based user access
* MySQL-connected to-do list
* Add, edit, complete, and delete tasks
* Course dashboard
* Course registration form
* Course fee and discount calculation
* Grade calculator with result classification
* Student information page
* Profile page
* CV/resume page
* Feedback form
* Contact page
* Responsive layout with custom CSS styling

## Technologies Used

* ASP.NET Core Razor Pages
* C#
* MySQL
* MySqlConnector
* BCrypt.Net
* HTML
* CSS
* JavaScript
* Bootstrap

## Project Structure

```text
wapp/
├── Pages/
│   ├── Index.cshtml
│   ├── Login.cshtml
│   ├── Register.cshtml
│   ├── Logout.cshtml
│   ├── TodoList.cshtml
│   ├── Courses.cshtml
│   ├── CourseRegistration.cshtml
│   ├── GradeCalculator.cshtml
│   ├── StudentInfo.cshtml
│   ├── Profile.cshtml
│   ├── MyCV.cshtml
│   ├── FeedbackForm.cshtml
│   └── Contact.cshtml
├── Pages/Shared/
│   └── _Layout.cshtml
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
├── appsettings.json
├── Program.cs
└── wapp.csproj
```

## Database Setup

Create a MySQL database named `Todolistdb`.

```sql
CREATE DATABASE IF NOT EXISTS Todolistdb;
USE Todolistdb;

CREATE TABLE IF NOT EXISTS todolist_users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS todolist (
    id INT AUTO_INCREMENT PRIMARY KEY,
    task VARCHAR(255) NOT NULL,
    completed BOOLEAN NOT NULL DEFAULT FALSE,
    due_date DATE NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES todolist_users(id)
);
```

## Connection String

Update the MySQL connection string inside `appsettings.json`.

```json
{
  "ConnectionStrings": {
    "TodoDb": "Server=localhost;Port=3306;Database=Todolistdb;User ID=root;Password=YOUR_PASSWORD;"
  }
}
```

Replace `YOUR_PASSWORD` with your own MySQL password.

## How to Run the Project

1. Open the project folder in Visual Studio Code.

2. Make sure the .NET SDK and MySQL Server are installed.

3. Open the terminal inside the project folder.

4. Restore the project dependencies:

```bash
dotnet restore
```

5. Run the project:

```bash
dotnet run
```

6. Open the local URL shown in the terminal, usually similar to:

```text
https://localhost:5001
```

or

```text
http://localhost:5000
```

## Main Pages

| Page                  | Description                             |
| --------------------- | --------------------------------------- |
| `/`                   | Main student portal home page           |
| `/Register`           | User registration page                  |
| `/Login`              | User login page                         |
| `/Logout`             | Logout page                             |
| `/TodoList`           | MySQL task management page              |
| `/Courses`            | Course dashboard                        |
| `/CourseRegistration` | Course registration and fee calculation |
| `/GradeCalculator`    | Grade calculator                        |
| `/StudentInfo`        | Student information page                |
| `/Profile`            | Student profile page                    |
| `/MyCV`               | CV/resume page                          |
| `/FeedbackForm`       | Feedback submission page                |
| `/Contact`            | Contact page                            |

## Purpose

The purpose of this project is to demonstrate key web application development concepts using ASP.NET Core Razor Pages. It covers server-side programming, form handling, session management, authentication, database connectivity, and responsive user interface design.

## Author

Developed by Zayan Aijaz Reshi.
