const API_URL = "https://localhost:7265/api/Project";

const getToken = () => {
  const token = localStorage.getItem("token");

  if (!token) {
    throw new Error(
      "Authentication token not found. Please login again."
    );
  }

  return token;
};

// ================= GET ALL PROJECTS =================

export const getProjects = async () => {
  const token = getToken();

  try {
    const response = await fetch(API_URL, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });

    console.log("Project GET Status:", response.status);

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
            `Failed to fetch projects. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Project GET API Error:", error);
    throw error;
  }
};

// ================= ADD PROJECT =================

export const addProject = async (project) => {
  const token = getToken();

  console.log("Project Data:", project);

  try {
    const response = await fetch(API_URL, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(project),
    });

    console.log("Project POST Status:", response.status);

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
            `Failed to create project. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Project POST API Error:", error);
    throw error;
  }
};

// ================= UPDATE PROJECT =================

export const updateProject = async (id, project) => {
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
        body: JSON.stringify(project),
      }
    );

    console.log("Project PUT Status:", response.status);

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
            `Failed to update project. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Project PUT API Error:", error);
    throw error;
  }
};

// ================= DELETE PROJECT =================

export const deleteProject = async (id) => {
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

    console.log("Project DELETE Status:", response.status);

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
            `Failed to delete project. Status: ${response.status}`
      );
    }

    return data;
  } catch (error) {
    console.error("Project DELETE API Error:", error);
    throw error;
  }
};