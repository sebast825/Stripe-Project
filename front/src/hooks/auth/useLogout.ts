import { useMutation } from "@tanstack/react-query";
import { useAuthStore } from "../../states/auth/auth.store";
import { useUserStore } from "../../states/auth/user.store";
import { queryClient } from "../../states/queryClient";
import apiClient from "../../api/client";

export const useLogout = () => {
  return useMutation({
    mutationFn: async ({ refreshToken }: { refreshToken: string }) => {
      return await apiClient.post("/auth/logout", refreshToken, {
        timeout: 3000,
      });
    },
    onSettled: () => {
      useUserStore.getState().clearUser();
      useAuthStore.getState().logout();
      queryClient.clear();
    },
  });
};
