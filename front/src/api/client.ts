// src/api/client.ts
import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7098/api',
  withCredentials: true,
    headers: {
    'Content-Type': 'application/json'
  }
});

// Interceptor de errores
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    // Normalizamos el error
    const formattedError = {
      status: error.response?.status,
      message: error.response?.data?.message || "Error inesperado",
      data: error.response?.data,
    };

    return Promise.reject(formattedError);
  }
);


export default apiClient;
