import { useState } from "react";
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
    }
  };
  const logout = async (refreshToken: string) => {
    try {
      authService.logout(refreshToken);
      navigate("/");
    } catch (error) {
      setError(true);
    }
  };

  return { login, error, setError, logout };
};
