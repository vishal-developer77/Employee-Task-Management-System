const API_URL = "https://localhost:7265/api/Employee";

// ======================================================
// GET EMPLOYEES
// ======================================================

export const getEmployees = async ({
  search = "",
  department = "",
  isActive = "",
  sortBy = "",
  sortOrder = "",
  pageNumber = 1,
  pageSize = 10,
} = {}) => {
  const token = localStorage.getItem("token");

  console.log("Employee GET Token Exists:", !!token);

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  const params = new URLSearchParams();

  if (search) {
    params.append("search", search);
  }

  if (department) {
    params.append("department", department);
  }

  if (isActive !== "") {
    params.append("isActive", isActive);
  }

  if (sortBy) {
    params.append("sortBy", sortBy);
  }

  if (sortOrder) {
    params.append("sortOrder", sortOrder);
  }

  params.append("pageNumber", pageNumber);
  params.append("pageSize", pageSize);

  try {
    const response = await fetch(
      `${API_URL}?${params.toString()}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    console.log("Employee GET Status:", response.status);

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        throw new Error(
          `Server returned invalid response. Status: ${response.status}`
        );
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
        data.message ||
          `Failed to fetch employees. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Employee GET API Error:", error);
    throw error;
  }
};

// ======================================================
// ADD EMPLOYEE
// ======================================================

export const addEmployee = async (employee) => {
  const token = localStorage.getItem("token");

  console.log("Employee POST Token Exists:", !!token);

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  console.log("Employee Data:", employee);

  try {
    const response = await fetch(API_URL, {
      method: "POST",

      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
        
      },
      

      body: JSON.stringify({
  ...employee,
  employeeId: Number(employeeId),
}),
    });
    

    console.log("Employee POST Status:", response.status);

    const text = await response.text();

    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        throw new Error(
          `Server returned invalid response. Status: ${response.status}`
        );
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
        data.message ||
          `Failed to create employee. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Employee POST API Error:", error);
    throw error;
  }
};
/* ======================================================
   UPDATE EMPLOYEE
====================================================== */

export const updateEmployee = async (employeeId, employee) => {
  const token = localStorage.getItem("token");

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  try {
    const response = await fetch(`${API_URL}/${employeeId}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(employee),
    });

    const text = await response.text();
    let data = {};

    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = { message: text };
      }
    }

    if (response.status === 401) {
      localStorage.removeItem("token");
      throw new Error("401 Unauthorized. Please login again.");
    }

    if (!response.ok) {
      throw new Error(
        data.message || `Failed to update employee. Status: ${response.status}`
      );
    }

    // Some APIs return an empty body on successful update.
    return data.success === undefined
      ? { success: true, data }
      : data;
  } catch (error) {
    console.error("Employee PUT API Error:", error);
    throw error;
  }
};
/* ======================================================
   DELETE EMPLOYEE
====================================================== */

export const deleteEmployee = async (employeeId) => {
  const token = localStorage.getItem("token");

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  const response = await fetch(`${API_URL}/${employeeId}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });

  const text = await response.text();
  let data = {};

  if (text) {
    try {
      data = JSON.parse(text);
    } catch {
      throw new Error("Server returned an invalid response.");
    }
  }

  if (response.status === 401) {
    localStorage.removeItem("token");
    throw new Error("Session expired. Please login again.");
  }

  if (!response.ok || data.success === false) {
    throw new Error(
      data.message || `Failed to delete employee. Status: ${response.status}`
    );
  }

  return data;
};