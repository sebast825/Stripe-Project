// src/api/client.ts
import axios from "axios";
import { useAuthStore } from "../states/auth/auth.store";
const apiClient = axios.create({
  baseURL: "https://bhx92f9r-7098.brs.devtunnels.ms/api",
  //baseURL: 'https://localhost:7098/api',
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  },
});

apiClient.interceptors.request.use((config) => {
  const accessToken = useAuthStore.getState().accessToken;

  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }

  return config;
});

apiClient.interceptors.response.use(
  (res) => res,
  async (error) => {
    const original = error.config;

    if (error.response?.status === 401 && !original._retry) {
      original._retry = true;

      try {
        const refreshToken = useAuthStore.getState().refreshToken;

        const { data } = await apiClient.post(
          "/auth/access-token",
          JSON.stringify(refreshToken)
        );
        useAuthStore.getState().setTokens(data, refreshToken!);

        return apiClient(original);
      } catch (err) {
        //logout if refresh token also fails
        //in app is a hook that will redirect to login
          useAuthStore.getState().logout();

        return Promise.reject(err);
      }
    }

    return Promise.reject(error);
  }
);

// Error Interceptor
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    // Normalize error format
    const formattedError = {
      status: error.response?.status,
      message: error.response?.data?.message || "Error inesperado",
      data: error.response?.data,
    };

    return Promise.reject(formattedError);
  }
);

export default apiClient;
