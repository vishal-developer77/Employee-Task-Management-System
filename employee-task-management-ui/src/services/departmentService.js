const API_URL = "https://localhost:7265/api/Department";

// ======================================================
// GET ALL DEPARTMENTS
// ======================================================

export const getDepartments = async () => {
  const token = localStorage.getItem("token");

  console.log("Department GET Token Exists:", !!token);

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  try {
    const response = await fetch(API_URL, {
      method: "GET",

      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });

    console.log(
      "Department GET Status:",
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
            `Failed to fetch departments. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error(
      "Department GET API Error:",
      error
    );

    throw error;
  }
};


// ======================================================
// ADD DEPARTMENT
// ======================================================

export const addDepartment = async (department) => {
  const token = localStorage.getItem("token");

  console.log(
    "Department POST Token Exists:",
    !!token
  );

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  try {
    const response = await fetch(API_URL, {
      method: "POST",

      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },

      body: JSON.stringify(department),
    });

    console.log(
      "Department POST Status:",
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
            `Failed to create department. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error(
      "Department POST API Error:",
      error
    );

    throw error;
  }
};


// ======================================================
// UPDATE DEPARTMENT
// ======================================================

export const updateDepartment = async (
  id,
  department
) => {
  const token = localStorage.getItem("token");

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  try {
    const response = await fetch(
      `${API_URL}/${id}`,
      {
        method: "PUT",

        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },

        body: JSON.stringify(department),
      }
    );

    console.log(
      "Department PUT Status:",
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
            `Failed to update department. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error(
      "Department PUT API Error:",
      error
    );

    throw error;
  }
};


// ======================================================
// DELETE DEPARTMENT
// ======================================================

export const deleteDepartment = async (id) => {
  const token = localStorage.getItem("token");

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

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
      "Department DELETE Status:",
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
            `Failed to delete department. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error(
      "Department DELETE API Error:",
      error
    );

    throw error;
  }
};