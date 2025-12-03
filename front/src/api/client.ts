// src/api/client.ts
import axios from "axios";
import { useAuthStore } from "../states/auth/auth.store";
const apiClient = axios.create({
  baseURL: "https://bhx92f9r-7098.brs.devtunnels.ms/api",
  // baseURL: "https://localhost:7098/api",
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  },
    timeout: 10000, 
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
    return handleOriginalRequest(error);
  }
);
function shouldRefreshToken(error: any, request: any): boolean {
  return (
    error.response?.status === 401 &&
    !request._retry &&
    !request.url?.includes("/login")
  );
}
async function tryRefreshToken(error: any) {
  const original = error.config;

  original._retry = true;

  try {
    const refreshToken = useAuthStore.getState().refreshToken;

    const { data } = await apiClient.post(
      "/auth/refresh",
      JSON.stringify(refreshToken)
    );
    useAuthStore.getState().setTokens(data, refreshToken!);
        console.log("prueba")

    return apiClient(original);
  } catch (err) {
    //logout if refresh token also fails
    //in app is a hook that will redirect to login
    useAuthStore.getState().logout();
    console.log("entra aca")
    return Promise.reject(err);
  }
}

async function handleOriginalRequest(error: any) {
  const originalRequest = error.config;

  if (shouldRefreshToken(error, originalRequest)) {
    await tryRefreshToken(error);
  }

  var formattedError = createFormattedError(error);
  console.log("API Error:", formattedError);

  return Promise.reject(formattedError);
}



function createFormattedError(error: any) {
  // Normalize error format
  var formattedError;
  //si if there is no conexion
  if (!error.response) {
    formattedError = {
      status: error.code || "NETWORK_ERROR",
      message: "No se pudo establecer conexión con el servidor",
      data: error.response?.data,
    };
  } else {
    //error from server
    formattedError = {
      status: error.response?.status,
      message: error.response?.data?.Message || "Error inesperado",
      data: error.response?.data,
    };
  }
  return formattedError;
}

export default apiClient;
