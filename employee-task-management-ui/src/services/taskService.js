const API_URL = "https://localhost:7265/api/Task";

const getToken = () => {
  const token = localStorage.getItem("token");

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  return token;
};

// ================= GET ALL TASKS =================

export const getTasks = async () => {
  const token = getToken();

  try {
    const response = await fetch(API_URL, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });

    console.log("Task GET Status:", response.status);

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = text;
      }
    }

    if (response.status === 401) {
      localStorage.removeItem("token");
      throw new Error(
        "401 Unauthorized. Please login again."
      );
    }

    if (!response.ok) {
      throw new Error(
        typeof data === "string"
          ? data
          : data.message ||
            `Failed to fetch tasks. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Task GET API Error:", error);
    throw error;
  }
};

// ================= GET TASK BY ID =================

export const getTaskById = async (id) => {
  const token = getToken();

  try {
    const response = await fetch(
      `${API_URL}/${id}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    console.log(
      "Task GET BY ID Status:",
      response.status
    );

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = text;
      }
    }

    if (response.status === 401) {
      localStorage.removeItem("token");
      throw new Error(
        "401 Unauthorized. Please login again."
      );
    }

    if (!response.ok) {
      throw new Error(
        typeof data === "string"
          ? data
          : data.message ||
            `Failed to fetch task. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error(
      "Task GET BY ID API Error:",
      error
    );
    throw error;
  }
};

// ================= ADD TASK =================

export const addTask = async (task) => {
  const token = getToken();

  console.log("Task Data:", task);

  try {
    const response = await fetch(API_URL, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    });

    console.log("Task POST Status:", response.status);

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = text;
      }
    }

    if (response.status === 401) {
      localStorage.removeItem("token");
      throw new Error(
        "401 Unauthorized. Please login again."
      );
    }

    if (!response.ok) {
      throw new Error(
        typeof data === "string"
          ? data
          : data.message ||
            `Failed to create task. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Task POST API Error:", error);
    throw error;
  }
};

// ================= UPDATE TASK =================

export const updateTask = async (id, task) => {
  const token = getToken();

  try {
    const response = await fetch(
      `${API_URL}/${id}`,
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(task),
      }
    );

    console.log("Task PUT Status:", response.status);

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = text;
      }
    }

    if (response.status === 401) {
      localStorage.removeItem("token");
      throw new Error(
        "401 Unauthorized. Please login again."
      );
    }

    if (!response.ok) {
      throw new Error(
        typeof data === "string"
          ? data
          : data.message ||
            `Failed to update task. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Task PUT API Error:", error);
    throw error;
  }
};

// ================= DELETE TASK =================

export const deleteTask = async (id) => {
  const token = getToken();

  try {
    const response = await fetch(
      `${API_URL}/${id}`,
      {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    console.log(
      "Task DELETE Status:",
      response.status
    );

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = text;
      }
    }

    if (response.status === 401) {
      localStorage.removeItem("token");
      throw new Error(
        "401 Unauthorized. Please login again."
      );
    }

    if (!response.ok) {
      throw new Error(
        typeof data === "string"
          ? data
          : data.message ||
            `Failed to delete task. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error(
      "Task DELETE API Error:",
      error
    );
    throw error;
  }
};