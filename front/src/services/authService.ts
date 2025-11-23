import apiClient from "../api/client";
import type { LoginResponse } from "../types/loginResponse.types";

 class AuthService {
  public login = async (
    email: string,
    password: string
  ): Promise<LoginResponse> => {
    const response = await apiClient.post("/auth/login", {
      email,
      password,
    });
    return response.data as LoginResponse;
  };
}
export const authService = new AuthService();
