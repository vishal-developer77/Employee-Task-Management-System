import "./App.css";
import { useEffect, useState } from "react";
import {
  getEmployees,
  addEmployee,
  updateEmployee,
  deleteEmployee,
} from "./services/employeeService";
import {
  getNotifications,
  markNotificationAsRead,
  deleteNotification,
} from "./services/notificationService";
import {
  getDepartments,
  addDepartment,
  updateDepartment,
  deleteDepartment,
} from "./services/departmentService";
import {
  getProjects,
  addProject,
  updateProject,
  deleteProject,
} from "./services/projectService";
import {
  getTasks,
  addTask,
  updateTask,
  deleteTask,
} from "./services/taskService";

function App() {
  // ================= LOGIN =================
  
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  
  
  const [isLoggedIn, setIsLoggedIn] = useState(
    !!localStorage.getItem("token")
  );
  
  // ================= MENU =================
  
  const [activeMenu, setActiveMenu] = useState("Dashboard");
  
  
  
  
  // ================= DASHBOARD =================
  
  const [dashboard, setDashboard] = useState(null);
  const [loading, setLoading] = useState(false);
  
  // ================= EMPLOYEES =================
  
  const [employees, setEmployees] = useState([]);
  const handleDeleteEmployee = async (employee) => {
   
    const confirmed = window.confirm(
      `Are you sure you want to delete ${employee.employeeName}?`
    );
    
    if (!confirmed) return;

  try {
    setEmployeeLoading(true);
    setEmployeeError("");

    const response = await deleteEmployee(employee.employeeId);

    if (response.success) {
      setEmployees((currentEmployees) =>
        currentEmployees.filter(
          (item) => item.employeeId !== employee.employeeId
        )
      );

      setShowEmployeeActions(false);
      setSelectedEmployee(null);

      alert(response.message || "Employee deleted successfully.");
    } else {
      setEmployeeError(
        response.message || "Failed to delete employee."
      );
    }
  } catch (error) {
    console.error("Delete employee error:", error);
    setEmployeeError(error.message || "Failed to delete employee.");
  } finally {
    setEmployeeLoading(false);
  }
};
  const [selectedEmployee, setSelectedEmployee] = useState(null);
const [showEmployeeActions, setShowEmployeeActions] = useState(false);
const [employeeSearch, setEmployeeSearch] = useState("");
  const [employeeLoading, setEmployeeLoading] = useState(false);
  
  const [employeeError, setEmployeeError] = useState("");
  const [employeeDepartment, setEmployeeDepartment] = useState("");

  const [showAddEmployee, setShowAddEmployee] = useState(false);
  const [editingEmployee, setEditingEmployee] = useState(null);

  const [employeeForm, setEmployeeForm] = useState({
    employeeName: "",
    email: "",
    department: "",
    designation: "",
    salary: "",
    isActive: true,
  });

  const [employeeMessage, setEmployeeMessage] = useState("");
  const [savingEmployee, setSavingEmployee] = useState(false);
  // ================= PROJECTS =================

const [projects, setProjects] = useState([]);
const [projectLoading, setProjectLoading] = useState(false);
const [projectError, setProjectError] = useState("");

const [showAddProject, setShowAddProject] = useState(false);
const [editingProjectId, setEditingProjectId] = useState(null);

const [projectForm, setProjectForm] = useState({
  projectName: "",
  description: "",
  startDate: "",
  endDate: "",
  createdBy: "",
  status: "Active",
});

const [projectMessage, setProjectMessage] = useState("");
const [savingProject, setSavingProject] = useState(false);
// ================= TASKS =================

const [tasks, setTasks] = useState([]);

const [notifications, setNotifications] = useState([]);
const [taskLoading, setTaskLoading] = useState(false);
const [taskError, setTaskError] = useState("");

const [showAddTask, setShowAddTask] = useState(false);
const [editingTaskId, setEditingTaskId] = useState(null);
const [taskProjects, setTaskProjects] = useState([]);
const [taskEmployees, setTaskEmployees] = useState([]);
const [dashboardStats, setDashboardStats] = useState({
  employees: 0,
  projects: 0,
  tasks: 0,
  completedTasks: 0,
});
const [taskStatusStats, setTaskStatusStats] = useState({
  pending: 0,
  inProgress: 0,
  completed: 0,
  cancelled: 0,
});
const [taskSearch, setTaskSearch] = useState("");
const [taskStatusFilter, setTaskStatusFilter] = useState("All");
const [taskPriorityFilter, setTaskPriorityFilter] = useState("All");
const filteredTasks = tasks.filter((task) => {
  const search = taskSearch.toLowerCase();

  const taskTitle =
    task.taskTitle?.toLowerCase() || "";

  const description =
    task.description?.toLowerCase() || "";

  const matchesSearch =
    taskTitle.includes(search) ||
    description.includes(search);

  const matchesStatus =
    taskStatusFilter === "All" ||
    task.status === taskStatusFilter;

  const matchesPriority =
    taskPriorityFilter === "All" ||
    task.priority === taskPriorityFilter;

  return (
    matchesSearch &&
    matchesStatus &&
    matchesPriority
  );
});

const [taskForm, setTaskForm] = useState({
  projectId: "",
  assignedTo: "",
  taskTitle: "",
  description: "",
  priority: "Medium",
  status: "Pending",
  dueDate: "",
  createdBy: "",
});

const [taskMessage, setTaskMessage] = useState("");
const [savingTask, setSavingTask] = useState(false);
  // ================= DEPARTMENTS =================

const [departments, setDepartments] = useState([]);

const [departmentLoading, setDepartmentLoading] =
  useState(false);

const [departmentError, setDepartmentError] =
  useState("");

const [showAddDepartment, setShowAddDepartment] =
  useState(false);

const [editingDepartmentId, setEditingDepartmentId] =
  useState(null);

const [departmentForm, setDepartmentForm] =
  useState({
    departmentName: "",
    description: "",
    isActive: true,
  });

const [departmentMessage, setDepartmentMessage] =
  useState("");

const [savingDepartment, setSavingDepartment] =
  useState(false);

  // ================= LOGIN =================

 const handleLogin = async (e) => {
  e.preventDefault();

  setMessage("Logging in...");

  try {
    const response = await fetch(
      "https://localhost:7265/api/Auth/login",
      {
        method: "POST",

        headers: {
          "Content-Type": "application/json",
        },

        body: JSON.stringify({
          email: email,
          password: password,
        }),
      }
    );

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = {};
      }
    }

    console.log("Login Status:", response.status);
    console.log("Login Response:", data);

    if (response.ok) {

      // Save JWT token
      localStorage.setItem("token", data.token);

      console.log(
        "Login Token Saved:",
        !!localStorage.getItem("token")
      );

      setIsLoggedIn(true);
      setMessage("");

    } else {

      setMessage(
        data.message || "Invalid email or password."
      );
    }

  } catch (error) {

    console.error("Login Error:", error);

    setMessage(
      "Unable to connect to server."
    );
  }
};

  // ================= DASHBOARD =================

  useEffect(() => {
    if (!isLoggedIn) {
      return;
    }

    const getDashboard = async () => {
      setLoading(true);

      try {
        const token = localStorage.getItem("token");

        const response = await fetch(
          "https://localhost:7265/api/Dashboard/admin",
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        const text = await response.text();

        let data = {};

        if (text) {
          try {
            data = JSON.parse(text);
          } catch {
            data = {};
          }
        }

        if (response.ok) {
          setDashboard(data);
        } else {
          console.error(
            "Dashboard API error:",
            response.status,
            data
          );
        }
      } catch (error) {
        console.error(
          "Dashboard error:",
          error
        );
      } finally {
        setLoading(false);
      }
    };

    getDashboard();
  }, [isLoggedIn]);

  // ================= LOAD EMPLOYEES =================

  useEffect(() => {
  if (!isLoggedIn || activeMenu !== "Employees") {
    return;
  }

  const loadEmployees = async () => {
    setEmployeeLoading(true);
    setEmployeeError("");

    try {
      const response = await getEmployees({
        pageNumber: 1,
        pageSize: 10,
      });

      console.log("Employee API Response:", response);

      if (response.success) {
        const employeeList = response.data?.data;
        setEmployees(
          Array.isArray(employeeList) ? employeeList : []
        );
        console.log("Employees Loaded:", employeeList);
      } else {
        setEmployees([]);
        setEmployeeError(
          response.message || "Failed to load employees."
        );
      }
    } catch (error) {
      console.error("Employee API error:", error);
      setEmployees([]);
      setEmployeeError(
        error.message || "Failed to load employees."
      );
    } finally {
      setEmployeeLoading(false);
    }
  };

  loadEmployees();
}, [isLoggedIn, activeMenu]);
  // ================= LOAD PROJECTS =================
  useEffect(() => {
  if (!isLoggedIn || activeMenu !== "Projects") {
    return;
  }

  const loadProjects = async () => {
    setProjectLoading(true);
    setProjectError("");

    try {
      const response = await getProjects();

      console.log("Project API Response:", response);

      const projectList = Array.isArray(response)
        ? response
        : Array.isArray(response.data)
        ? response.data
        : [];

      setProjects(projectList);
    } catch (error) {
      console.error("Project loading error:", error);
      setProjectError(
        error.message || "Failed to load projects."
      );
    } finally {
      setProjectLoading(false);
    }
  };

  loadProjects();
}, [isLoggedIn, activeMenu]);
// ================= LOAD TASKS =================

useEffect(() => {
  if (
    !isLoggedIn ||
    activeMenu !== "Tasks"
  ) {
    return;
  }

  const loadTaskData = async () => {
    setTaskLoading(true);
    setTaskError("");

    try {
      // Load Tasks
      const taskResponse = await getTasks();

      setTasks(
        Array.isArray(taskResponse)
          ? taskResponse
          : taskResponse.data || []
      );

      // Load Projects
      const projectResponse = await getProjects();

      setTaskProjects(
        Array.isArray(projectResponse)
          ? projectResponse
          : projectResponse.data || []
      );

     const employeeResponse = await getEmployees();

console.log(
  "Employee API Response:",
  employeeResponse
);

const employeeList =
  Array.isArray(employeeResponse)
    ? employeeResponse
    : Array.isArray(employeeResponse.data?.data)
    ? employeeResponse.data.data
    : Array.isArray(employeeResponse.data)
    ? employeeResponse.data
    : [];
    console.log("Task Employee: ",employeeList);

setTaskEmployees(employeeList);

    } catch (error) {
      console.error(
        "Task page loading error:",
        error
      );

      setTaskError(
        error.message ||
        "Failed to load task data."
      );

    } finally {
      setTaskLoading(false);
    }
  };

  loadTaskData();

}, [isLoggedIn, activeMenu]);
useEffect(() => {
  // existing Task useEffect
}, [isLoggedIn, activeMenu]);


// 👇 YAHAN SE NEW CODE

useEffect(() => {
  if (!isLoggedIn || activeMenu !== "Dashboard") {
    return;
  }

  const loadDashboardData = async () => {
    try {
      const employeeResponse = await getEmployees();
      const projectResponse = await getProjects();
      const taskResponse = await getTasks();

      const employeeList =
        Array.isArray(employeeResponse)
          ? employeeResponse
          : Array.isArray(employeeResponse.data?.data)
          ? employeeResponse.data.data
          : [];

      const projectList =
        Array.isArray(projectResponse)
          ? projectResponse
          : Array.isArray(projectResponse.data)
          ? projectResponse.data
          : [];

      const taskList =
        Array.isArray(taskResponse)
          ? taskResponse
          : Array.isArray(taskResponse.data)
          ? taskResponse.data
          : [];

      const completedTasks =
        taskList.filter(
          (task) => task.status === "Completed"
        ).length;
        
        const pendingTasks =
  taskList.filter(
    (task) => task.status === "Pending"
  ).length;

const inProgressTasks =
  taskList.filter(
    (task) => task.status === "In Progress"
  ).length;

const cancelledTasks =
  taskList.filter(
    (task) => task.status === "Cancelled"
  ).length;

setTaskStatusStats({
  pending: pendingTasks,
  inProgress: inProgressTasks,
  completed: completedTasks,
  cancelled: cancelledTasks,
});

      setDashboardStats({
        employees: employeeList.length,
        projects: projectList.length,
        tasks: taskList.length,
        completedTasks: completedTasks,
      });

    } catch (error) {
      console.error(
        "Dashboard loading error:",
        error
      );
    }
  };

  loadDashboardData();

}, [isLoggedIn, activeMenu]);
  // ================= LOAD DEPARTMENTS =================

useEffect(() => {
  if (
    !isLoggedIn ||
    activeMenu !== "Departments"
  ) {
    return;
  }

  const loadDepartments = async () => {
    setDepartmentLoading(true);
    setDepartmentError("");

    try {
      const response = await getDepartments();

      console.log(
        "Department API Response:",
        response
      );

      setDepartments(
        Array.isArray(response)
          ? response
          : response.data || []
      );

    } catch (error) {
      console.error(
        "Department API error:",
        error
      );

      setDepartmentError(
        error.message ||
          "Failed to load departments."
      );
    } finally {
      setDepartmentLoading(false);
    }
  };

  loadDepartments();

}, [isLoggedIn, activeMenu]);

  // ================= EMPLOYEE FORM =================

  const handleEmployeeChange = (e) => {
    const { name, value } = e.target;

    setEmployeeForm({
      ...employeeForm,
      [name]: value,
    });
  };

  // ================= ADD EMPLOYEE =================

  const handleAddEmployee = async (e) => {
    e.preventDefault();

    setSavingEmployee(true);
    setEmployeeMessage("");

    try {
   const employeeData = {
  employeeId: Number(editingEmployee.employeeId),
  employeeName: employeeForm.employeeName,
  email: employeeForm.email,
  department: employeeForm.department,
  designation: employeeForm.designation,
  salary: Number(employeeForm.salary),
  isActive: employeeForm.isActive,
};
console.log("EDITING EMPLOYEE ID:", editingEmployee?.employeeId);
console.log("UPDATE DATA:", employeeData);

      console.log(
        "Employee Data:",
        employeeData
      );

      const response =
        await addEmployee(employeeData);

      if (response.success) {
        setEmployeeMessage(
          "Employee created successfully."
        );

        const updatedEmployees =
          await getEmployees({
            pageNumber: 1,
            pageSize: 10,
          });

        if (updatedEmployees.success) {
          setEmployees(
            updatedEmployees.data?.data || []
          );
        }

        setEmployeeForm({
          employeeName: "",
          email: "",
          department: "",
          designation: "",
          salary: "",
          isActive: true,
        });

        setTimeout(() => {
          setShowAddEmployee(false);
          setEmployeeMessage("");
        }, 1000);
      } else {
        setEmployeeMessage(
          response.message ||
            "Failed to create employee."
        );
      }
    } catch (error) {
      console.error(
        "Add employee error:",
        error
      );

      setEmployeeMessage(
        error.message ||
          "Failed to create employee."
      );
    } finally {
      setSavingEmployee(false);
    }
  };
  const handleUpdateEmployee = async (e) => {
  e.preventDefault();

  setSavingEmployee(true);
  setEmployeeMessage("");

  try {
    const employeeData = {
      employeeName: employeeForm.employeeName,
      email: employeeForm.email,
      department: employeeForm.department,
      designation: employeeForm.designation,
      salary: Number(employeeForm.salary),
      isActive: employeeForm.isActive,
    };

    console.log("Updating Employee:", employeeData);

    const response = await updateEmployee(
      editingEmployee.employeeId,
      employeeData
    );

    if (response.success) {
      setEmployeeMessage("Employee updated successfully.");

      const updatedEmployees = await getEmployees({
        pageNumber: 1,
        pageSize: 10,
      });

      if (updatedEmployees.success) {
        setEmployees(updatedEmployees.data?.data || []);
      }

      setEditingEmployee(null);

      setTimeout(() => {
        setShowAddEmployee(false);
        setEmployeeMessage("");
      }, 1000);
    } else {
      setEmployeeMessage(
        response.message || "Failed to update employee."
      );
    }
  } catch (error) {
    console.error("Update employee error:", error);

    setEmployeeMessage(
      error.message || "Failed to update employee."
    );
  } finally {
    setSavingEmployee(false);
  }
};

  // ================= CANCEL EMPLOYEE =================

  const handleCancelEmployee = () => {
    setShowAddEmployee(false);

    setEmployeeForm({
      employeeName: "",
      email: "",
      department: "",
      designation: "",
      salary: "",
      isActive: true,
    });

    setEmployeeMessage("");
  };

  // ================= LOGOUT =================

  const handleLogout = () => {
    localStorage.removeItem("token");

    setIsLoggedIn(false);
    setDashboard(null);
    setEmployees([]);

    setEmail("");
    setPassword("");

    setActiveMenu("Dashboard");
  };

  // ================= LOGIN PAGE =================

  if (!isLoggedIn) {
    return (
      <div className="login-container">

        <div className="login-box">

          <h1>
            Employee Task Management
          </h1>

          <p className="login-title">
            Login
          </p>

          <form onSubmit={handleLogin}>

            <input
              type="email"
              placeholder="Enter Email"
              value={email}
              onChange={(e) =>
                setEmail(e.target.value)
              }
              required
            />

            <input
              type="password"
              placeholder="Enter Password"
              value={password}
              onChange={(e) =>
                setPassword(e.target.value)
              }
              required
            />

            <button type="submit">
              Login
            </button>

          </form>

          {message && (
            <p
              style={{
                marginTop: "15px",
                textAlign: "center",
              }}
            >
              {message}
            </p>
          )}

        </div>

      </div>
    );
  }

  // ================= MAIN APPLICATION =================

  return (
    <div className="app-layout">

      {/* ================= SIDEBAR ================= */}

      <aside className="sidebar">

        <h2>
          Employee Task
        </h2>

        <p className="sidebar-subtitle">
          Management System
        </p>

        <nav>

          <button
            className={
              activeMenu === "Dashboard"
                ? "active"
                : ""
            }
            onClick={() =>
              setActiveMenu("Dashboard")
            }
          >
            📊 Dashboard
          </button>

          <button
            className={
              activeMenu === "Employees"
                ? "active"
                : ""
            }
            onClick={() =>
              setActiveMenu("Employees")
            }
          >
            👥 Employees
          </button>

          <button
            className={
              activeMenu === "Departments"
                ? "active"
                : ""
            }
            onClick={() =>
              setActiveMenu("Departments")
            }
          >
            🏢 Departments
          </button>

          <button
            className={
              activeMenu === "Projects"
                ? "active"
                : ""
            }
            onClick={() =>
              setActiveMenu("Projects")
            }
          >
            📁 Projects
          </button>
          <button
  className={
    activeMenu === "Tasks"
      ? "active"
      : ""
  }
  onClick={() =>
    setActiveMenu("Tasks")
  }
>
  📋 Tasks
</button>

         
          <button
            className={
              activeMenu === "Notifications"
                ? "active"
                : ""
            }
            onClick={() =>
              setActiveMenu("Notifications")
            }
          >
            🔔 Notifications
          </button>

          <button
            className={
              activeMenu === "Audit Logs"
                ? "active"
                : ""
            }
            onClick={() =>
              setActiveMenu("Audit Logs")
            }
          >
            📝 Audit Logs
          </button>

        </nav>

        <button
          className="logout-button"
          onClick={handleLogout}
        >
          🚪 Logout
        </button>

      </aside>

      {/* ================= MAIN CONTENT ================= */}

      <main className="main-content">

        {/* TOP BAR */}

        <div className="top-bar">

          <h1>
            {activeMenu}
          </h1>

          <span>
            👤 Admin
          </span>

        </div>
        {/* Task Status Summary */}
<div className="task-status-section">

  <div className="task-status-header">
    <div>
      <h2>Task Status</h2>
      <p>Current task progress summary</p>
    </div>
  </div>

  <div className="task-status-cards">

    {/* Pending */}
    <div className="task-status-card">
      <div className="task-status-icon">
        ⏳
      </div>

      <div>
        <h3>Pending</h3>
        <h2>{taskStatusStats.pending}</h2>
      </div>
    </div>

    {/* In Progress */}
    <div className="task-status-card">
      <div className="task-status-icon">
        🔄
      </div>

      <div>
        <h3>In Progress</h3>
        <h2>{taskStatusStats.inProgress}</h2>
      </div>
    </div>

    {/* Completed */}
    <div className="task-status-card">
      <div className="task-status-icon">
        ✅
      </div>

      <div>
        <h3>Completed</h3>
        <h2>{taskStatusStats.completed}</h2>
      </div>
    </div>

    {/* Cancelled */}
    <div className="task-status-card">
      <div className="task-status-icon">
        ❌
      </div>

      <div>
        <h3>Cancelled</h3>
        <h2>{taskStatusStats.cancelled}</h2>
      </div>
    </div>

  </div>

</div>

        {/* ================= DASHBOARD ================= */}

        {activeMenu === "Dashboard" && (
  <div className="dashboard">

    {/* Dashboard Header */}
    <div className="dashboard-header">
      <div>
        <h1>Dashboard</h1>
        <p>Welcome to Employee Task Management System</p>
      </div>
    </div>

    {/* Statistics Cards */}
    <div className="dashboard-stats">

      {/* Employees */}
      <div className="dashboard-card">
        <div className="dashboard-card-icon">
          👨‍💼
        </div>

        <div>
          <h3>Total Employees</h3>
          <h2>{dashboardStats.employees}</h2>
          <p>Active employees</p>
        </div>
      </div>

      {/* Projects */}
      <div className="dashboard-card">
        <div className="dashboard-card-icon">
          📁
        </div>

        <div>
          <h3>Total Projects</h3>
          <h2>{dashboardStats.projects}</h2>
          <p>Available projects</p>
        </div>
      </div>

      {/* Tasks */}
      <div className="dashboard-card">
        <div className="dashboard-card-icon">
          📋
        </div>

        <div>
          <h3>Total Tasks</h3>
          <h2>{dashboardStats.tasks}</h2>
          <p>All assigned tasks</p>
        </div>
      </div>

      {/* Completed Tasks */}
      <div className="dashboard-card">
        <div className="dashboard-card-icon">
          
        </div>

        <div>
          <h3>Completed Tasks</h3>
          <h2>{dashboardStats.completedTasks}</h2>
          <p>Successfully completed</p>
        </div>
      </div>

    </div>

    {/* Quick Overview */}
    <div className="dashboard-overview">

      <div className="overview-card">
        <h2>Quick Overview</h2>

        <div className="overview-item">
          <span>Total Employees</span>
          <strong>{dashboardStats.employees}</strong>
        </div>

        <div className="overview-item">
          <span>Total Projects</span>
          <strong>{dashboardStats.projects}</strong>
        </div>

        <div className="overview-item">
          <span>Total Tasks</span>
          <strong>{dashboardStats.tasks}</strong>
        </div>

        <div className="overview-item">
          <span>Completed Tasks</span>
          <strong>{dashboardStats.completedTasks}</strong>
        </div>
      </div>

      {/* Task Progress */}
      <div className="overview-card">
        <h2>Task Progress</h2>

        <div className="progress-container">

          <div className="progress-info">
            <span>Completed</span>

            <strong>
              {dashboardStats.tasks > 0
                ? Math.round(
                    (dashboardStats.completedTasks /
                      dashboardStats.tasks) *
                      100
                  )
                : 0}
              %
            </strong>
          </div>

          <div className="progress-bar">
            <div
              className="progress-fill"
              style={{
                width:
                  dashboardStats.tasks > 0
                    ? `${
                        (dashboardStats.completedTasks /
                          dashboardStats.tasks) *
                        100
                      }%`
                    : "0%",
              }}
            ></div>
          </div>

          <p>
            {dashboardStats.completedTasks} of{" "}
            {dashboardStats.tasks} tasks completed
          </p>

        </div>
      </div>

    </div>

    {/* Welcome Section */}
    <div className="dashboard-welcome">

      <h2>Employee Task Management</h2>

      <p>
        Manage employees, projects and tasks from one
        place. You can create projects, assign tasks to
        employees and track task progress easily.
      </p>
{/* Recent Tasks */}
<div className="recent-tasks">

  <div className="recent-tasks-header">
    <div>
      <h2>Recent Tasks</h2>
      <p>Latest assigned tasks</p>
    </div>

    <button
      onClick={() => setActiveMenu("Tasks")}
      className="view-all-btn"
    >
      View All
    </button>
  </div>

  {tasks.length === 0 ? (
    <div className="no-recent-tasks">
      <p>No tasks available.</p>
    </div>
  ) : (
    <div className="recent-tasks-table-wrapper">
      <table className="recent-tasks-table">

        <thead>
          <tr>
            <th>Task</th>
            <th>Project</th>
            <th>Assigned To</th>
            <th>Priority</th>
            <th>Status</th>
            <th>Due Date</th>
          </tr>
        </thead>

        <tbody>
          {tasks.slice(0, 5).map((task) => {

            const project = taskProjects.find(
              (project) =>
                Number(project.projectId || project.id) ===
                Number(task.projectId)
            );

            const employee = taskEmployees.find(
              (employee) =>
                Number(employee.employeeId || employee.id) ===
                Number(task.assignedTo)
            );

            return (
              <tr key={task.taskId}>

                <td>
                  <strong>
                    {task.taskTitle}
                  </strong>
                </td>

                <td>
                  {project?.projectName ||
                    `Project #${task.projectId}`}
                </td>

                <td>
                  {employee?.firstName ||
                    employee?.name ||
                    employee?.employeeName ||
                    `Employee #${task.assignedTo}`}
                </td>

                <td>
                  <span
                    className={`priority-badge ${(
                      task.priority || ""
                    ).toLowerCase()}`}
                  >
                    {task.priority}
                  </span>
                </td>

                <td>
                  <span
                    className={`status-badge ${(
                      task.status || ""
                    )
                      .toLowerCase()
                      .replace(" ", "-")}`}
                  >
                    {task.status}
                  </span>
                </td>

                <td>
                  {task.dueDate
                    ? new Date(
                        task.dueDate
                      ).toLocaleDateString()
                    : "-"}
                </td>

              </tr>
            );
          })}
        </tbody>

      </table>
    </div>
  )}

</div>
    </div>

  </div>
)}
{/* Notifications Page */}
{activeMenu === "Notifications" && (

  <div className="notifications-page">
    <div className="notifications-header">
      <div>
        <h1>Notifications</h1>
        <p>Stay updated with your employee and project activities.</p>
      </div>
      <span className="notifications-count">Updates</span>
    </div><div className="notifications-stats">
  <div className="notification-stat-card">
    <span className="notification-stat-icon">🔔</span>
    <div>
      <p>Total Notifications</p>
      <h2>{notifications.length}</h2>
    </div>
  </div>

  <div className="notification-stat-card">
    <span className="notification-stat-icon unread-icon">●</span>
    <div>
      <p>Unread Notifications</p>
      <h2>{notifications.filter((n) => !n.isRead).length}</h2>
    </div>
  </div>
</div>

<div className="notifications-panel">
  <div className="notifications-panel-header">
    <div>
      <h2>Recent Activity</h2>
      <p>Important updates at a glance</p>
    </div>

    <button
      className="mark-all-read-btn"
      onClick={() =>
        setNotifications((previous) =>
          previous.map((n) => ({ ...n, isRead: true }))
        )
      }
      disabled={!notifications.some((n) => !n.isRead)}
    >
      Mark all as read
    </button>
  </div>

  {notifications.length === 0 ? (
    <div className="notifications-empty">
      <span>🔔</span>
      <h3>You're all caught up!</h3>
      <p>New notifications will appear here when available.</p>
    </div>
  ) : (
    <div className="notifications-list">
      {notifications.map((notification, index) => (
        <div
          key={notification.notificationId || notification.id || index}
          className={`notification-item ${notification.isRead ? "read" : "unread"}`}
        >
          <div className="notification-item-icon">🔔</div>

          <div className="notification-item-content">
            <div className="notification-item-title-row">
              <h3>
                {notification.title ||
                  notification.notificationTitle ||
                  "Notification"}
              </h3>
              {!notification.isRead && (
                <span className="unread-label">NEW</span>
              )}
            </div>

            <p>
              {notification.message ||
                notification.description ||
                notification.notificationMessage ||
                "You have a new update."}
            </p>

            <small>
              {notification.createdAt
                ? new Date(notification.createdAt).toLocaleString()
                : "Recently"}
            </small>
          </div>

          {!notification.isRead && (
            <button
  className="mark-read-btn"
  onClick={async () => {
    try {
      const id =
        notification.notificationId || notification.id;

      if (!id) {
        throw new Error("Notification ID not found.");
      }

      await markNotificationAsRead(id);

      setNotifications((previous) =>
        previous.map((n) =>
          (n.notificationId || n.id) === id
            ? { ...n, isRead: true }
            : n
        )
      );
    } catch (error) {
      console.error("Mark notification as read error:", error);
      alert(error.message || "Failed to mark notification as read.");
    }
  }}
>
  Mark read
</button>
          )}
        </div>
      ))}
    </div>
  )}
</div>

  </div>
)}
       {/* ================= EMPLOYEES ================= */}

{activeMenu === "Employees" && (

  <div className="employee-page">

    {/* HEADER */}

    <div className="employee-page-header">

      <div>
        <h2>
          Employees
        </h2>

        <p>
          Manage and monitor all employees
        </p>
      </div>

      {!showAddEmployee && (
        <button
          className="add-employee-btn"
          onClick={() => setShowAddEmployee(true)}
        >
          + Add Employee
        </button>
      )}

    </div>

            {/* EMPLOYEE LIST */}

            {!showAddEmployee && (
              <>

                <div className="employee-toolbar">

                  <div className="search-box">

                    <span>
                      🔍
                    </span>

                    <input
  type="text"
  placeholder="Search employee..."
  value={employeeSearch}
  onChange={(e) => setEmployeeSearch(e.target.value)}
/>

                  </div>

                 <select
  className="filter-select"
  value={employeeDepartment}
  onChange={(e) => setEmployeeDepartment(e.target.value)}
>
  <option value="">
    All Departments
  </option>

  <option value="IT">
    IT
  </option>

  <option value="HR">
    HR
  </option>

  <option value="Finance">
    Finance
  </option>

  <option value="Sales">
    Sales
  </option>
</select>

                </div>

                {employeeLoading && (
                  <div className="employee-loading">
                    Loading employees...
                  </div>
                )}

                {employeeError && (
                  <div className="employee-error">
                    {employeeError}
                  </div>
                )}

                {!employeeLoading &&
                  !employeeError && (
                    

                    <div className="employee-table-card">

                     <table className="employee-table">

  <thead>
    <tr>
      <th>ID</th>
      <th>Employee</th>
      <th>Email</th>
      <th>Department</th>
      <th>Designation</th>
      <th>Salary</th>
      <th>Status</th>
      <th>Action</th>
    </tr>
  </thead>

  <tbody>

    {employees.length === 0 ? (

      <tr>
        <td colSpan="8" className="no-employees">
          No employees found.
        </td>
      </tr>

    ) : (

     employees
  .filter((employee) =>
    employee.employeeName
      ?.toLowerCase()
      .includes(employeeSearch.toLowerCase())
  )
  .filter((employee) =>
    employeeDepartment === "" ||
    employee.department === employeeDepartment
  )
  .map(
    (employee) => (

        <tr key={employee.employeeId}>

          <td>
            #{employee.employeeId}
          </td>

          <td>
            <div className="employee-name">

              <div className="employee-avatar">
                {employee.employeeName
                  ?.charAt(0)
                  ?.toUpperCase()}
              </div>

              <strong>
                {employee.employeeName}
              </strong>

            </div>
          </td>

          <td>
            {employee.email}
          </td>

          <td>
            <span className="department-badge">
              {employee.department}
            </span>
          </td>

          <td>
            {employee.designation}
          </td>

          <td>
            ₹{employee.salary}
          </td>

          <td>

            <span
              className={
                employee.isActive
                  ? "status-active"
                  : "status-inactive"
              }
            >
              {employee.isActive
                ? "Active"
                : "Inactive"}
            </span>

          </td>

          <td>

            <button
              className="action-btn"
              onClick={() => {
                setSelectedEmployee(employee);
                setShowEmployeeActions(true);
              }}
            >
              ⋮
            </button>

            {showEmployeeActions &&
              selectedEmployee?.employeeId === employee.employeeId && (
<div className="employee-action-menu">
  <button
  onClick={() => {
    console.log("EDIT BUTTON CLICKED");
    alert("Edit button working");

    setEditingEmployee(employee);

    setEmployeeForm({
      employeeName: employee.employeeName || "",
      email: employee.email || "",
      department: employee.department || "",
      designation: employee.designation || "",
      salary: employee.salary ?? "",
      isActive: employee.isActive ?? true,
    });

    setShowAddEmployee(true);
    setShowEmployeeActions(false);
  }}
>
  Edit
</button>

  <button
  onClick={() => handleDeleteEmployee(employee)}
  disabled={employeeLoading}
>
  {employeeLoading ? "Deleting..." : "Delete"}
</button>
</div>

              )}

          </td>

        </tr>

      ))

    )}

  </tbody>

</table>

                    </div>

                  )}

              </>
            )}

            {/* ================= ADD EMPLOYEE FORM ================= */}

            {showAddEmployee && (

              <div className="add-employee-card">

                <div className="add-employee-header">

                  <div>

                    <h2>
  {editingEmployee ? "Edit Employee" : "Add New Employee"}
</h2>
                    <p>
  {editingEmployee
    ? "Update the employee information below"
    : "Enter the employee information below"}
</p>

                  </div>

                  <button
                    className="close-form-btn"
                    onClick={handleCancelEmployee}
                  >
                    ✕
                  </button>

                </div>

                <form
  className="employee-form"
  onSubmit={
    editingEmployee
      ? handleUpdateEmployee
      : handleAddEmployee
  }
>

                  <div className="form-group">

                    <label>
                      Employee Name
                    </label>

                    <input
                      type="text"
                      name="employeeName"
                      placeholder="Enter employee name"
                      value={
                        employeeForm.employeeName
                      }
                      onChange={
                        handleEmployeeChange
                      }
                      required
                    />

                  </div>

                  <div className="form-group">

                    <label>
                      Email Address
                    </label>

                    <input
                      type="email"
                      name="email"
                      placeholder="Enter email address"
                      value={
                        employeeForm.email
                      }
                      onChange={
                        handleEmployeeChange
                      }
                      required
                    />

                  </div>

                  <div className="form-group">

                    <label>
                      Department
                    </label>

                    <select
                      name="department"
                      value={
                        employeeForm.department
                      }
                      onChange={
                        handleEmployeeChange
                      }
                      required
                    >

                      <option value="">
                        Select department
                      </option>

                      <option value="IT">
                        IT
                      </option>

                      <option value="HR">
                        HR
                      </option>

                      <option value="Finance">
                        Finance
                      </option>

                      <option value="Sales">
                        Sales
                      </option>

                    </select>

                  </div>

                  <div className="form-group">

                    <label>
                      Designation
                    </label>

                    <input
                      type="text"
                      name="designation"
                      placeholder="Enter designation"
                      value={
                        employeeForm.designation
                      }
                      onChange={
                        handleEmployeeChange
                      }
                      required
                    />

                  </div>

                  <div className="form-group">

                    <label>
                      Salary
                    </label>

                    <input
                      type="number"
                      name="salary"
                      placeholder="Enter salary"
                      value={
                        employeeForm.salary
                      }
                      onChange={
                        handleEmployeeChange
                      }
                      min="1"
                      required
                    />

                  </div>

                  <div className="form-group">

                    <label>
                      Status
                    </label>

                    <select
                      name="isActive"
                      value={
                        employeeForm.isActive
                          ? "true"
                          : "false"
                      }
                      onChange={(e) =>
                        setEmployeeForm({
                          ...employeeForm,
                          isActive:
                            e.target.value ===
                            "true",
                        })
                      }
                    >

                      <option value="true">
                        Active
                      </option>

                      <option value="false">
                        Inactive
                      </option>

                    </select>

                  </div>

                  {employeeMessage && (

                    <div className="employee-message">
                      {employeeMessage}
                    </div>

                  )}

                  <div className="employee-form-actions">

                    <button
                      type="button"
                      className="form-cancel-btn"
                      onClick={
                        handleCancelEmployee
                      }
                    >
                      Cancel
                    </button>

                    <button
                      type="submit"
                      className="form-save-btn"
                      disabled={savingEmployee}
                    >
                      {savingEmployee
                        ? "Saving..."
                        : "Save Employee"}
                    </button>

                  </div>

                </form>

              </div>

            )}

          </div>

        )}
{/* ================= DEPARTMENTS ================= */}

{activeMenu === "Departments" && (
  <div className="employees-container">

    <div className="page-header">
      <div>
        <h2>Departments</h2>
        <p>Manage all departments</p>
      </div>

      <button
        className="add-btn"
        onClick={() => {
          setShowAddDepartment(true);
          setEditingDepartmentId(null);
          setDepartmentForm({
            departmentName: "",
            description: "",
            isActive: true,
          });
          setDepartmentMessage("");
        }}
      >
        + Add Department
      </button>
    </div>

    {/* Add / Edit Department Form */}
    {showAddDepartment && (
      <div className="employee-form">

        <h3>
          {editingDepartmentId
            ? "Edit Department"
            : "Add Department"}
        </h3>

        <input
          type="text"
          placeholder="Department Name"
          value={departmentForm.departmentName}
          onChange={(e) =>
            setDepartmentForm({
              ...departmentForm,
              departmentName: e.target.value,
            })
          }
        />

        <textarea
          placeholder="Description"
          value={departmentForm.description}
          onChange={(e) =>
            setDepartmentForm({
              ...departmentForm,
              description: e.target.value,
            })
          }
        />

        {editingDepartmentId && (
          <label>
            <input
              type="checkbox"
              checked={departmentForm.isActive}
              onChange={(e) =>
                setDepartmentForm({
                  ...departmentForm,
                  isActive: e.target.checked,
                })
              }
            />
            Active
          </label>
        )}

        <div>
          <button
            className="save-btn"
            disabled={savingDepartment}
            onClick={async () => {
              if (!departmentForm.departmentName.trim()) {
                setDepartmentMessage(
                  "Department name is required."
                );
                return;
              }

              try {
                setSavingDepartment(true);
                setDepartmentMessage("");

                if (editingDepartmentId) {
                  await updateDepartment(
                    editingDepartmentId,
                    departmentForm
                  );
                } else {
                  await addDepartment(departmentForm);
                }

                const response = await getDepartments();

                setDepartments(
                  Array.isArray(response)
                    ? response
                    : response.data || []
                );

                setShowAddDepartment(false);
                setEditingDepartmentId(null);

                setDepartmentForm({
                  departmentName: "",
                  description: "",
                  isActive: true,
                });

                setDepartmentMessage(
                  editingDepartmentId
                    ? "Department updated successfully."
                    : "Department added successfully."
                );
              } catch (error) {
                console.error(
                  "Department save error:",
                  error
                );

                setDepartmentMessage(
                  error.message ||
                    "Failed to save department."
                );
              } finally {
                setSavingDepartment(false);
              }
            }}
          >
            {savingDepartment
              ? "Saving..."
              : editingDepartmentId
              ? "Update"
              : "Save"}
          </button>

          <button
            className="cancel-btn"
            onClick={() => {
              setShowAddDepartment(false);
              setEditingDepartmentId(null);
              setDepartmentMessage("");
            }}
          >
            Cancel
          </button>
        </div>

        {departmentMessage && (
          <p>{departmentMessage}</p>
        )}
      </div>
    )}

    {/* Loading */}
    {departmentLoading && (
      <p>Loading departments...</p>
    )}

    {/* Error */}
    {departmentError && (
      <p className="error-message">
        {departmentError}
      </p>
    )}

    {/* Department Table */}
    {!departmentLoading && !departmentError && (
      <table className="employee-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Department</th>
            <th>Description</th>
            <th>Status</th>
            <th>Action</th>
          </tr>
        </thead>

        <tbody>
          {departments.length === 0 ? (
            <tr>
              <td colSpan="5">
                No departments found.
              </td>
            </tr>
          ) : (
            departments.map((department) => (
              <tr
                key={
                  department.departmentId ||
                  department.id
                }
              >
                <td>
                  {department.departmentId ||
                    department.id}
                </td>

                <td>
                  {department.departmentName}
                </td>

                <td>
                  {department.description || "-"}
                </td>

                <td>
                  {department.isActive === false
                    ? "Inactive"
                    : "Active"}
                </td>

                <td>
                  <button
                    className="edit-btn"
                    onClick={() => {
                      setEditingDepartmentId(
                        department.departmentId ||
                          department.id
                      );

                      setDepartmentForm({
                        departmentName:
                          department.departmentName || "",
                        description:
                          department.description || "",
                        isActive:
                          department.isActive !== false,
                      });

                      setShowAddDepartment(true);
                      setDepartmentMessage("");
                    }}
                  >
                    Edit
                  </button>

                  <button
                    className="delete-btn"
                    onClick={async () => {
                      const id =
                        department.departmentId ||
                        department.id;

                      if (
                        !window.confirm(
                          "Are you sure you want to delete this department?"
                        )
                      ) {
                        return;
                      }

                      try {
                        await deleteDepartment(id);

                        const response =
                          await getDepartments();

                        setDepartments(
                          Array.isArray(response)
                            ? response
                            : response.data || []
                        );
                      } catch (error) {
                        console.error(
                          "Department delete error:",
                          error
                        );

                        setDepartmentError(
                          error.message ||
                            "Failed to delete department."
                        );
                      }
                    }}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    )}

  </div>
)}
{/* ================= PROJECTS ================= */}

{activeMenu === "Projects" && (
  <div className="employee-page">

    {/* HEADER */}
    <div className="employee-page-header">

      <div>
        <h2>Projects</h2>
        <p>Manage all projects</p>
      </div>

      {!showAddProject && (
        <button
          className="add-employee-btn"
          onClick={() => {
            setShowAddProject(true);
            setEditingProjectId(null);

            setProjectForm({
              projectName: "",
              description: "",
              startDate: "",
              endDate: "",
              createdBy: "",
              status: "Active",
            });

            setProjectMessage("");
          }}
        >
          + Add Project
        </button>
      )}

    </div>

    {/* ================= PROJECT LIST ================= */}

    {!showAddProject && (
      <>

        {projectLoading && (
          <div className="employee-loading">
            Loading projects...
          </div>
        )}

        {projectError && (
          <div className="employee-error">
            {projectError}
          </div>
        )}

        {!projectLoading && !projectError && (
          <div className="employee-table-card">

            <table className="employee-table">

              <thead>
                <tr>
                  <th>ID</th>
                  <th>Project Name</th>
                  <th>Description</th>
                  <th>Start Date</th>
                  <th>End Date</th>
                  <th>Created By</th>
                  <th>Status</th>
                  <th>Action</th>
                </tr>
              </thead>

              <tbody>

                {projects.length === 0 ? (

                  <tr>
                    <td
                      colSpan="8"
                      className="no-employees"
                    >
                      No projects found.
                    </td>
                  </tr>

                ) : (

                  projects.map((project) => (

                    <tr
                      key={
                        project.projectId ||
                        project.id
                      }
                    >

                      <td>
                        #
                        {project.projectId ||
                          project.id}
                      </td>

                      <td>
                        <strong>
                          {project.projectName}
                        </strong>
                      </td>

                      <td>
                        {project.description || "-"}
                      </td>

                      <td>
                        {project.startDate
                          ? new Date(
                              project.startDate
                            ).toLocaleDateString()
                          : "-"}
                      </td>

                      <td>
                        {project.endDate
                          ? new Date(
                              project.endDate
                            ).toLocaleDateString()
                          : "-"}
                      </td>

                      <td>
                        {project.createdBy}
                      </td>

                      <td>

                        <span
                          className={
                            project.status === "Completed"
                              ? "status-active"
                              : "status-inactive"
                          }
                        >
                          {project.status}
                        </span>

                      </td>

                      <td>

                        <button
                          className="edit-btn"
                          onClick={() => {

                            const id =
                              project.projectId ||
                              project.id;

                            setEditingProjectId(id);

                            setProjectForm({
                              projectName:
                                project.projectName ||
                                "",

                              description:
                                project.description ||
                                "",

                              startDate:
                                project.startDate
                                  ? project.startDate
                                      .split("T")[0]
                                  : "",

                              endDate:
                                project.endDate
                                  ? project.endDate
                                      .split("T")[0]
                                  : "",

                              createdBy:
                                project.createdBy ||
                                "",

                              status:
                                project.status ||
                                "Active",
                            });

                            setShowAddProject(true);
                            setProjectMessage("");
                          }}
                        >
                          Edit
                        </button>

                        <button
                          className="delete-btn"
                          onClick={async () => {

                            const id =
                              project.projectId ||
                              project.id;

                            if (
                              !window.confirm(
                                "Are you sure you want to delete this project?"
                              )
                            ) {
                              return;
                            }

                            try {

                              await deleteProject(id);

                              const response =
                                await getProjects();

                              setProjects(
                                Array.isArray(response)
                                  ? response
                                  : response.data || []
                              );

                            } catch (error) {

                              console.error(
                                "Project delete error:",
                                error
                              );

                              setProjectError(
                                error.message ||
                                  "Failed to delete project."
                              );
                            }
                          }}
                        >
                          Delete
                        </button>

                      </td>

                    </tr>

                  ))

                )}

              </tbody>

            </table>

          </div>
        )}

      </>
    )}

    {/* ================= ADD / EDIT PROJECT ================= */}

    {showAddProject && (

      <div className="add-employee-card">

        <div className="add-employee-header">

          <div>

            <h2>
              {editingProjectId
                ? "Edit Project"
                : "Add New Project"}
            </h2>

            <p>
              Enter project information below
            </p>

          </div>

          <button
            className="close-form-btn"
            onClick={() => {

              setShowAddProject(false);
              setEditingProjectId(null);
              setProjectMessage("");

            }}
          >
            ✕
          </button>

        </div>

        <form
          className="employee-form"
          onSubmit={async (e) => {

            e.preventDefault();

            if (
              !projectForm.projectName.trim()
            ) {
              setProjectMessage(
                "Project name is required."
              );
              return;
            }

            if (!projectForm.createdBy) {
              setProjectMessage(
                "Created By is required."
              );
              return;
            }

            try {

              setSavingProject(true);
              setProjectMessage("");

              const projectData = {

                projectName:
                  projectForm.projectName,

                description:
                  projectForm.description || null,

                startDate:
                  projectForm.startDate,

                endDate:
                  projectForm.endDate || null,

                createdBy:
                  Number(projectForm.createdBy),

                status:
                  projectForm.status,
              };

              console.log(
                "Project Data:",
                projectData
              );

              if (editingProjectId) {

                await updateProject(
                  editingProjectId,
                  {
                    projectId:
                      editingProjectId,
                    ...projectData,
                  }
                );

                setProjectMessage(
                  "Project updated successfully."
                );

              } else {

                await addProject(projectData);

                setProjectMessage(
                  "Project created successfully."
                );
              }

              const response =
                await getProjects();

              setProjects(
                Array.isArray(response)
                  ? response
                  : response.data || []
              );

              setProjectForm({
                projectName: "",
                description: "",
                startDate: "",
                endDate: "",
                createdBy: "",
                status: "Active",
              });

              setTimeout(() => {

                setShowAddProject(false);
                setEditingProjectId(null);
                setProjectMessage("");

              }, 1000);

            } catch (error) {

              console.error(
                "Project save error:",
                error
              );

              setProjectMessage(
                error.message ||
                  "Failed to save project."
              );

            } finally {

              setSavingProject(false);

            }

          }}
        >

          {/* PROJECT NAME */}

          <div className="form-group">

            <label>
              Project Name
            </label>

            <input
              type="text"
              placeholder="Enter project name"
              value={
                projectForm.projectName
              }
              onChange={(e) =>
                setProjectForm({
                  ...projectForm,
                  projectName:
                    e.target.value,
                })
              }
              required
            />

          </div>

          {/* DESCRIPTION */}

          <div className="form-group">

            <label>
              Description
            </label>

            <textarea
              placeholder="Enter project description"
              value={
                projectForm.description
              }
              onChange={(e) =>
                setProjectForm({
                  ...projectForm,
                  description:
                    e.target.value,
                })
              }
            />

          </div>

          {/* START DATE */}

          <div className="form-group">

            <label>
              Start Date
            </label>

            <input
              type="date"
              value={
                projectForm.startDate
              }
              onChange={(e) =>
                setProjectForm({
                  ...projectForm,
                  startDate:
                    e.target.value,
                })
              }
              required
            />

          </div>

          {/* END DATE */}

          <div className="form-group">

            <label>
              End Date
            </label>

            <input
              type="date"
              value={
                projectForm.endDate
              }
              onChange={(e) =>
                setProjectForm({
                  ...projectForm,
                  endDate:
                    e.target.value,
                })
              }
            />

          </div>

          {/* CREATED BY */}

          <div className="form-group">

            <label>
              Created By
            </label>

            <input
              type="number"
              placeholder="Enter Admin/User ID"
              value={
                projectForm.createdBy
              }
              onChange={(e) =>
                setProjectForm({
                  ...projectForm,
                  createdBy:
                    e.target.value,
                })
              }
              min="1"
              required
            />

          </div>

          {/* STATUS */}

          <div className="form-group">

            <label>
              Status
            </label>

            <select
              value={
                projectForm.status
              }
              onChange={(e) =>
                setProjectForm({
                  ...projectForm,
                  status:
                    e.target.value,
                })
              }
            >

              <option value="Active">
                Active
              </option>

              <option value="Pending">
                Pending
              </option>

              <option value="In Progress">
                In Progress
              </option>

              <option value="Completed">
                Completed
              </option>

              <option value="Cancelled">
                Cancelled
              </option>

            </select>

          </div>

          {/* MESSAGE */}

          {projectMessage && (

            <div className="employee-message">
              {projectMessage}
            </div>

          )}

          {/* BUTTONS */}

          <div className="employee-form-actions">

            <button
              type="button"
              className="form-cancel-btn"
              onClick={() => {

                setShowAddProject(false);
                setEditingProjectId(null);
                setProjectMessage("");

              }}
            >
              Cancel
            </button>

            <button
              type="submit"
              className="form-save-btn"
              disabled={savingProject}
            >
              {savingProject
                ? "Saving..."
                : editingProjectId
                ? "Update Project"
                : "Save Project"}
            </button>

          </div>

        </form>

      </div>

    )}

  </div>
)}
{/* ================= TASKS ================= */}

{activeMenu === "Tasks" && (
  <div className="employee-page">

    {/* HEADER */}
    <div className="employee-page-header">

      <div>
        <h2>Tasks</h2>
        <p>Manage all employee tasks</p>
      </div>

      {!showAddTask && (
        <button
          className="add-employee-btn"
          onClick={() => {
            setShowAddTask(true);
            setEditingTaskId(null);

            setTaskForm({
              projectId: "",
              assignedTo: "",
              taskTitle: "",
              description: "",
              priority: "Medium",
              status: "Pending",
              dueDate: "",
              createdBy: "",
            });

            setTaskMessage("");
          }}
        >
          + Add Task
        </button>
      )}

    </div>

    {/* ================= TASK LIST ================= */}

    {!showAddTask && (
      <><div className="task-search" style={{ marginBottom: "16px" }}>
  <input
    type="text"
    placeholder="Search tasks by title or description..."
    value={taskSearch}
    onChange={(e) => setTaskSearch(e.target.value)}
  />
</div>
<select
  value={taskStatusFilter}
  onChange={(e) =>
    setTaskStatusFilter(e.target.value)
  }
  style={{
    marginBottom: "16px",
    marginLeft: "10px",
    padding: "8px"
  }}
>
  <option value="All">All Status</option>
  <option value="Pending">Pending</option>
  <option value="In Progress">In Progress</option>
  <option value="Completed">Completed</option>
  <option value="Cancelled">Cancelled</option>
</select><select
  value={taskPriorityFilter}
  onChange={(e) =>
    setTaskPriorityFilter(e.target.value)
  }
  style={{
    marginBottom: "16px",
    marginLeft: "10px",
    padding: "8px"
  }}
>
  <option value="All">All Priority</option>
  <option value="Low">Low</option>
  <option value="Medium">Medium</option>
  <option value="High">High</option>
</select>

        {taskLoading && (
          <div className="employee-loading">
            Loading tasks...
          </div>
        )}

        {taskError && (
          <div className="employee-error">
            {taskError}
          </div>
        )}

        {!taskLoading && !taskError && (
          <div className="employee-table-card">

            <table className="employee-table">

              <thead>
                <tr>
                  <th>ID</th>
                  <th>Task</th>
                  <th>Project</th>
                  <th>Assigned To</th>
                  <th>Priority</th>
                  <th>Status</th>
                  <th>Due Date</th>
                  <th>Action</th>
                </tr>
              </thead>

              <tbody>

                {tasks.length === 0 ? (

                  <tr>
                    <td
                      colSpan="8"
                      className="no-employees"
                    >
                      No tasks found.
                    </td>
                  </tr>

                ) : (

                 filteredTasks.map((task) => {

                    const id =
                      task.taskId ||
                      task.id;

                    return (
                      <tr key={id}>

                        <td>
                          #{id}
                        </td>

                        <td>
                          <strong>
                            {task.taskTitle}
                          </strong>

                          {task.description && (
                            <div>
                              {task.description}
                            </div>
                          )}
                        </td>

                        <td>
  {taskProjects.find(
    (project) =>
      Number(project.projectId || project.id) ===
      Number(task.projectId)
  )?.projectName || `Project #${task.projectId}`}
</td>
                       <td>
  {(() => {
    const employee = taskEmployees.find(
      (emp) =>
        Number(emp.employeeId || emp.id) ===
        Number(task.assignedTo)
    );

    return (
      employee?.firstName ||
      employee?.name ||
      employee?.employeeName ||
      `Employee #${task.assignedTo}`
    );
  })()}
</td>

                        <td>
                          {task.priority}
                        </td>

                        <td>
                          <span
                            className={
                              task.status ===
                              "Completed"
                                ? "status-active"
                                : "status-inactive"
                            }
                          >
                            {task.status}
                          </span>
                        </td>

                        <td>
                          {task.dueDate
                            ? new Date(
                                task.dueDate
                              ).toLocaleDateString()
                            : "-"}
                        </td>

                        <td>

                          {/* EDIT */}

                          <button
                            className="edit-btn"
                            onClick={() => {

                              setEditingTaskId(id);

                              setTaskForm({
                                projectId:
                                  task.projectId ||
                                  "",

                                assignedTo:
                                  task.assignedTo ||
                                  "",

                                taskTitle:
                                  task.taskTitle ||
                                  "",

                                description:
                                  task.description ||
                                  "",

                                priority:
                                  task.priority ||
                                  "Medium",

                                status:
                                  task.status ||
                                  "Pending",

                                dueDate:
                                  task.dueDate
                                    ? task.dueDate
                                        .split("T")[0]
                                    : "",

                                createdBy:
                                  task.createdBy ||
                                  "",
                              });

                              setShowAddTask(true);
                              setTaskMessage("");

                            }}
                          >
                            Edit
                          </button>

                          {/* DELETE */}

                          <button
                            className="delete-btn"
                            onClick={async () => {

                              if (
                                !window.confirm(
                                  "Are you sure you want to delete this task?"
                                )
                              ) {
                                return;
                              }

                              try {

                                await deleteTask(id);

                                const response =
                                  await getTasks();

                                setTasks(
                                  Array.isArray(response)
                                    ? response
                                    : response.data || []
                                );

                              } catch (error) {

                                console.error(
                                  "Task delete error:",
                                  error
                                );

                                setTaskError(
                                  error.message ||
                                    "Failed to delete task."
                                );

                              }

                            }}
                          >
                            Delete
                          </button>

                        </td>

                      </tr>
                    );

                  })

                )}

              </tbody>

            </table>

          </div>
        )}

      </>
    )}

    {/* ================= ADD / EDIT TASK ================= */}

    {showAddTask && (

      <div className="add-employee-card">

        <div className="add-employee-header">

          <div>

            <h2>
              {editingTaskId
                ? "Edit Task"
                : "Add New Task"}
            </h2>

            <p>
              Enter task information below
            </p>

          </div>

          <button
            className="close-form-btn"
            onClick={() => {

              setShowAddTask(false);
              setEditingTaskId(null);
              setTaskMessage("");

            }}
          >
            ✕
          </button>

        </div>

        <form
          className="employee-form"
          onSubmit={async (e) => {

            e.preventDefault();

            if (!taskForm.projectId) {
              setTaskMessage(
                "Project ID is required."
              );
              return;
            }

            if (!taskForm.assignedTo) {
              setTaskMessage(
                "Assigned To is required."
              );
              return;
            }

            if (!taskForm.taskTitle.trim()) {
              setTaskMessage(
                "Task title is required."
              );
              return;
            }

            if (!taskForm.createdBy) {
              setTaskMessage(
                "Created By is required."
              );
              return;
            }

            try {

              setSavingTask(true);
              setTaskMessage("");

              const taskData = {

                projectId:
                  Number(taskForm.projectId),

                assignedTo:
                  Number(taskForm.assignedTo),

                taskTitle:
                  taskForm.taskTitle,

                description:
                  taskForm.description || null,

                priority:
                  taskForm.priority,

                status:
                  taskForm.status,

                dueDate:
                  taskForm.dueDate || null,

                createdBy:
                  Number(taskForm.createdBy),
              };

              console.log(
                "Task Data:",
                taskData
              );

              if (editingTaskId) {

                await updateTask(
                  editingTaskId,
                  {
                    taskId:
                      editingTaskId,
                    ...taskData,
                  }
                );

                setTaskMessage(
                  "Task updated successfully."
                );

              } else {

                await addTask(taskData);

                setTaskMessage(
                  "Task created successfully."
                );

              }

              const response =
                await getTasks();

              setTasks(
                Array.isArray(response)
                  ? response
                  : response.data || []
              );

              setTaskForm({
                projectId: "",
                assignedTo: "",
                taskTitle: "",
                description: "",
                priority: "Medium",
                status: "Pending",
                dueDate: "",
                createdBy: "",
              });

              setTimeout(() => {

                setShowAddTask(false);
                setEditingTaskId(null);
                setTaskMessage("");

              }, 1000);

            } catch (error) {

              console.error(
                "Task save error:",
                error
              );

              setTaskMessage(
                error.message ||
                  "Failed to save task."
              );

            } finally {

              setSavingTask(false);

            }

          }}
        >

          {/* PROJECT */}

<div className="form-group">

  <label>
    Project
  </label>

  <select
    value={taskForm.projectId}
    onChange={(e) =>
      setTaskForm({
        ...taskForm,
        projectId: e.target.value,
      })
    }
    required
  >

    <option value="">
      Select Project
    </option>

    {taskProjects.map((project) => {

      const projectId =
        project.projectId || project.id;

      return (
        <option
          key={projectId}
          value={projectId}
        >
          {project.projectName}
        </option>
      );

    })}

  </select>

</div>
          {/* ASSIGNED TO */}

<div className="form-group">
  <label>Assigned To</label>

  <select
    value={taskForm.assignedTo}
    onChange={(e) =>
      setTaskForm({
        ...taskForm,
        assignedTo: e.target.value,
      })
    }
    required
  >
    <option value="">Select Employee</option>

    {Array.isArray(taskEmployees) &&
      taskEmployees.map((employee) => {
        const userId = employee.userId;

        return (
          <option
            key={userId}
            value={userId ?? ""}
            disabled={!userId}
          >
            {employee.fullName ||
              employee.employeeName ||
              employee.name}
          </option>
        );
      })}
  </select>
</div>
          {/* TASK TITLE */}

          <div className="form-group">

            <label>
              Task Title
            </label>

            <input
              type="text"
              placeholder="Enter task title"
              value={
                taskForm.taskTitle
              }
              onChange={(e) =>
                setTaskForm({
                  ...taskForm,
                  taskTitle:
                    e.target.value,
                })
              }
              required
            />

          </div>

          {/* DESCRIPTION */}

          <div className="form-group">

            <label>
              Description
            </label>

            <textarea
              placeholder="Enter task description"
              value={
                taskForm.description
              }
              onChange={(e) =>
                setTaskForm({
                  ...taskForm,
                  description:
                    e.target.value,
                })
              }
            />

          </div>

          {/* PRIORITY */}

          <div className="form-group">

            <label>
              Priority
            </label>

            <select
              value={
                taskForm.priority
              }
              onChange={(e) =>
                setTaskForm({
                  ...taskForm,
                  priority:
                    e.target.value,
                })
              }
            >

              <option value="Low">
                Low
              </option>

              <option value="Medium">
                Medium
              </option>

              <option value="High">
                High
              </option>

            </select>

          </div>

          {/* STATUS */}

          <div className="form-group">

            <label>
              Status
            </label>

            <select
              value={
                taskForm.status
              }
              onChange={(e) =>
                setTaskForm({
                  ...taskForm,
                  status:
                    e.target.value,
                })
              }
            >

              <option value="Pending">
                Pending
              </option>

              <option value="In Progress">
                In Progress
              </option>

              <option value="Completed">
                Completed
              </option>

              <option value="Cancelled">
                Cancelled
              </option>

            </select>

          </div>

          {/* DUE DATE */}

          <div className="form-group">

            <label>
              Due Date
            </label>

            <input
              type="date"
              value={
                taskForm.dueDate
              }
              onChange={(e) =>
                setTaskForm({
                  ...taskForm,
                  dueDate:
                    e.target.value,
                })
              }
            />

          </div>

          {/* CREATED BY */}

          <div className="form-group">

            <label>
              Created By
            </label>

            <input
              type="number"
              placeholder="Enter User ID"
              value={
                taskForm.createdBy
              }
              onChange={(e) =>
                setTaskForm({
                  ...taskForm,
                  createdBy:
                    e.target.value,
                })
              }
              min="1"
              required
            />

          </div>

          {/* MESSAGE */}

          {taskMessage && (
            <div className="employee-message">
              {taskMessage}
            </div>
          )}

          {/* BUTTONS */}

          <div className="employee-form-actions">

            <button
              type="button"
              className="form-cancel-btn"
              onClick={() => {

                setShowAddTask(false);
                setEditingTaskId(null);
                setTaskMessage("");

              }}
            >
              Cancel
            </button>

            <button
              type="submit"
              className="form-save-btn"
              disabled={savingTask}
            >
              {savingTask
                ? "Saving..."
                : editingTaskId
                ? "Update Task"
                : "Save Task"}
            </button>

          </div>

        </form>

      </div>

    )}

  </div>
)}
        {/* ================= OTHER MODULES ================= */}

        {activeMenu !== "Dashboard" &&
          activeMenu !== "Employees" &&
          activeMenu !== "Departments" &&
          activeMenu !== "Projects" && 
          activeMenu !== "Tasks" &&
          (

            <div className="coming-soon">

              <h2>
                {activeMenu}
              </h2>

              <p>
                This module will be connected
                to the backend API next.
              </p>

            </div>

          )}

      </main>

    </div>
  );
}

export default App;