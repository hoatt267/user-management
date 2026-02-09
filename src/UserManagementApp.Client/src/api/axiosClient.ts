import axios from "axios";

const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || "https://localhost:7117/api",
  timeout: parseInt(import.meta.env.VITE_API_TIMEOUT || "30000", 10),
  headers: {
    "Content-Type": "application/json",
  },
});

// Response Interceptor
axiosClient.interceptors.response.use(
  (response) => response,
  (error) => {
    // Handle Global Errors here
    if (error.response) {
      console.error(
        `API Error: ${error.response.status} - ${error.response.data.title}`,
      );
    } else {
      console.error("Network Error:", error.message);
    }
    return Promise.reject(error);
  },
);

export default axiosClient;
