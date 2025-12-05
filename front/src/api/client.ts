import axios, {
  type AxiosResponse,
  type InternalAxiosRequestConfig,
} from "axios";
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
  (response) => {
    return response;
  },
  async (error) => {
    return handleInterceptorError(error);
  }
);

type QueuedRequest = {
  resolve: (value: AxiosResponse) => void;
  reject: (error: any) => void;
  config: InternalAxiosRequestConfig;
};

let isRefreshing = false;
let failedQueue: QueuedRequest[] = [];
const REFRESH_ENDPOINT = "/auth/refresh";


async function handleInterceptorError(error: any) {
  const originalRequest = error.config;

  // if que  no sea 404 y que no sea retry
  if (error.response?.status === 401 && !originalRequest._retry) {
    if (isRefreshing) {
      return addtoFailedQueue(originalRequest);
    }
    originalRequest._retry = true;
    isRefreshing = true;
    try {
      await getRefreshToken();
      const response = await apiClient(originalRequest);
      await executeAndClearFiledQueue();
      return response;
    } catch (error: any) {
      //in case response throw exception will not logout
      if (error.status === 401) {      
        useAuthStore.getState().logout();
        rejectAndClearExecutedQueue(error);
      } else {
        //in case response throw error, need to run queries here
        await executeAndClearFiledQueue();
      }
      throw createFormattedError(error);
    } finally {
      isRefreshing = false;
    }
  } else {
    return Promise.reject(error);
  }
}

async function getRefreshToken() {
  const refreshToken = useAuthStore.getState().refreshToken;
  const { data } = await apiClient.post(
    REFRESH_ENDPOINT,
    JSON.stringify(refreshToken)
  );
  useAuthStore.getState().setTokens(data, refreshToken!);
  //this fix the next queries in queue
  apiClient.defaults.headers.common[
    "Authorization"
  ] = `Bearer ${data.accessToken}`;
}
function addtoFailedQueue(originalRequest: any) {
  return new Promise((resolve, reject) => {
    failedQueue.push({ resolve, reject, config: originalRequest });
  });
}

async function executeAndClearFiledQueue() {
  const promises = failedQueue.map((item) =>
    apiClient(item.config)
      .then((response) => item.resolve(response))
      .catch((error) => item.reject(error))
  );
  await Promise.all(promises);
  failedQueue = [];
}

function rejectAndClearExecutedQueue(error: Error) {
  failedQueue.forEach((item) => item.reject(error));
  failedQueue = [];
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
