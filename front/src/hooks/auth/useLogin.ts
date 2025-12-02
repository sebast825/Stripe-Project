import { useMutation } from "@tanstack/react-query";
import { useAuthStore } from "../../states/auth/auth.store";
import { useUserStore } from "../../states/auth/user.store";
import apiClient from "../../api/client";
import type { LoginResponse } from "../../types/loginResponse.types";

export const useLogin = () => {
  return useMutation({
    mutationFn: async ({
      email,
      password,
    }: {
      email: string;
      password: string;
    }): Promise<LoginResponse> => {
      var rsta = await apiClient.post("/auth/login", {
        email,
        password,
      });
      return rsta.data;
    },
    onSuccess: (data) => {
      useUserStore.getState().setUser(data.user);
      useAuthStore.getState().setTokens(data.accessToken, data.refreshToken);
    },
    onError: (err: unknown) => {
      console.error("Error al iniciar sesión", err);
    },
  });
};
