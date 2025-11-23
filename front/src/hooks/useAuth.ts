import { useState } from "react";
import apiClient from "../api/client";
import { useNavigate } from "react-router-dom";
import type { LoginResponse } from "../types/loginResponse.types";
import { useUserStore } from "../states/auth/user.store";
import { useAuthStore } from "../states/auth/auth.store";
import { authService } from "../services/authService";

export const useAuth = () => {
  const [error, setError] = useState(false);
  const navigate = useNavigate();

  const login = async (email: string, password: string) => {
    try {
      const data: LoginResponse = await authService.login(email, password);
      useUserStore.getState().setUser(data.user);
      useAuthStore.getState().setTokens(data.accessToken, data.refreshToken);
      navigate("/dashboard");
    } catch (error) {
      setError(true);
      console.error("Login failed:", error);
    }
  };
  const logout = async () => {
    try {
      await apiClient.post("/auth/logout");
      navigate("/");
    } catch (error) {
      setError(true);
      console.error("Login failed:", error);
    }
  };

  return { login, error, setError, logout };
};
