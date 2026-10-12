Employee & Task Management System

A full-stack web application designed to manage employees, departments, projects, and tasks through a centralized admin dashboard.

🚀 Overview

The Employee & Task Management System is a practical full-stack project built to strengthen my software development skills and understand how frontend applications communicate with backend APIs and databases.

✨ Features

- Dashboard: View employee and task summaries.
- Employee Management: Manage employee records.
- Department Management: Organize departments.
- Project Management: Create and manage projects.
- Task Management: Track tasks and their statuses.
- Authentication: Login and API authentication support.
- Notifications: Manage application notifications.
- Audit Logs: Support activity tracking.

🛠️ Technologies Used

Frontend

- React.js
- JavaScript
- HTML5
- CSS3
- Vite

Backend

- C#
- ASP.NET Core Web API
- .NET

Database

- Microsoft SQL Server

Tools

- Visual Studio
- Visual Studio Code
- Git
- GitHub
- SQL Server Management Studio (SSMS)

📁 Project Structure

Employee-Task-Management-System/
├── EmployeeTaskManagement.API/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Migrations/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   └── Program.cs
│
├── employee-task-management-ui/
│   ├── public/
│   ├── src/
│   │   ├── assets/
│   │   ├── services/
│   │   ├── App.jsx
│   │   ├── App.css
│   │   └── index.css
│   └── package.json
│
└── README.md

⚙️ Getting Started

Prerequisites

Install the following tools:

- .NET SDK compatible with the API project
- Node.js and npm
- Microsoft SQL Server
- Visual Studio or Visual Studio Code

1. Clone the Repository

git clone https://github.com/vishal-developer77/Employee-Task-Management-System.git
cd Employee-Task-Management-System

2. Configure the Database and Backend

1. Open "EmployeeTaskManagement.API" in Visual Studio.
2. Configure your local SQL Server connection string.
3. Configure the JWT settings using your own local secret key.
4. Apply the required Entity Framework migrations if applicable.
5. Run the ASP.NET Core API.
6. Open Swagger to explore the available API endpoints.

Important: Do not commit database passwords, JWT secret keys, or other credentials to GitHub.

3. Run the Frontend

Open a terminal in the frontend folder:

cd employee-task-management-ui
npm install
npm run dev

Open the local URL shown in the terminal.

4. Configure API Connectivity

Ensure the frontend API service URLs point to the backend API address and port used by your local environment. Configure CORS on the backend as needed.

🎯 Learning Outcomes

- Building RESTful APIs with ASP.NET Core.
- Connecting a React frontend to backend APIs.
- Working with SQL Server and database migrations.
- Organizing code using controllers, services, and repositories.
- Implementing authentication and API integration.
- Using Git and GitHub for version control.

👨‍💻 Developer

Vishal Miratkar

Aspiring .NET Full Stack Developer

GitHub: "vishal-developer77" (https://github.com/vishal-developer77)

📌 Future Improvements

- Improve validation and error handling.
- Add automated testing.
- Enhance the user interface and responsiveness.
- Deploy the application with a hosted database and backend API.

---

⭐ If you find this project interesting, feel free to explore the repository and share feedback.
