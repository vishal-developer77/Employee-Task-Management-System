const API_URL = "https://localhost:7265/api/Notification";

function getHeaders() {
  const token = localStorage.getItem("token");

  return {
    "Content-Type": "application/json",
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
  };
}

async function handleResponse(response) {
  const text = await response.text();

  if (!response.ok) {
    throw new Error(text || `HTTP Error: ${response.status}`);
  }

  if (!text) return null;

  try {
    return JSON.parse(text);
  } catch {
    return text;
  }
}

export async function getNotifications() {
  const response = await fetch(API_URL, {
    method: "GET",
    headers: getHeaders(),
  });

  return handleResponse(response);
}

export async function markNotificationAsRead(id) {
  const response = await fetch(`${API_URL}/${id}/read`, {
    method: "PUT",
    headers: getHeaders(),
  });

  return handleResponse(response);
}

export async function deleteNotification(id) {
  const response = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
    headers: getHeaders(),
  });

  return handleResponse(response);
}