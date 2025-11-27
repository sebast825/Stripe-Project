import { useMutation } from "@tanstack/react-query";
import { useAuthStore } from "../../states/auth/auth.store";
import { useUserStore } from "../../states/auth/user.store";
import { authService } from "../../services/authService";
import { useRedirect } from "../useRedirect";

export const useLogin = () => {

   const {goToDashboard}= useRedirect();
  return useMutation({
    mutationFn: async ({
      email,
      password,
    }: {
      email: string;
      password: string;
    }) => {
      return await authService.login(email, password);
    },
    onSuccess: (data) => {
      useUserStore.getState().setUser(data.user);
      useAuthStore.getState().setTokens(data.accessToken, data.refreshToken);
      goToDashboard()
    },
    onError: (err: unknown) => {
      console.error("Error al iniciar sesión", err);
    },
  });
};
