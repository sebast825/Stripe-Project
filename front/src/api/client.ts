// src/api/client.ts
import axios from 'axios';
import { useAuthStore } from '../states/auth/auth.store';
const apiClient = axios.create({
  baseURL: 'https://localhost:7098/api',
  withCredentials: true,
    headers: {
    'Content-Type': 'application/json'
  }
});

apiClient.interceptors.request.use((config) => {
  const accessToken= useAuthStore.getState().accessToken

  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }

  return config;
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
