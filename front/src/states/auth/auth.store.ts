import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";

type AuthState = {
  accessToken: string | null;
  refreshToken: string | null;
  isAuthenticated: boolean;
  setTokens: (access: string, refresh: string) => void;
  logout: () => void;
};

export const useAuthStore = create<AuthState>((set) => ({
  
  accessToken: null,
  refreshToken: null,
  isAuthenticated: false,
  setTokens: (access, refresh) =>
    set({ accessToken: access, refreshToken: refresh, isAuthenticated: true }),
  logout: () =>
    set({ accessToken: null, refreshToken: null, isAuthenticated: false }),
}));
