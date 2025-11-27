import { useMutation } from "@tanstack/react-query";
import { useAuthStore } from "../../states/auth/auth.store";
import { useUserStore } from "../../states/auth/user.store";
import { authService } from "../../services/authService";

export const useLogout = () => {

  return useMutation({
    mutationFn: async ({
      refreshToken
    }: {
      refreshToken: string;
    }) => {
      return await authService.logout(refreshToken);
    },
    onSuccess: () => {
      useUserStore.getState().clearUser();
      useAuthStore.getState().logout();
    },
  });
};
