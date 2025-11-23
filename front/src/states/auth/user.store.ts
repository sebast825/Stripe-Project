import type { User } from '../../types/user.types';
import { create } from 'zustand';


interface UserState  {
  user: User | null;
  setUser: (u:User ) => void;
  clearUser: () => void;
};

export const useUserStore = create<UserState>((set) => ({
  user: null,
  setUser: (user) => set({ user }),
  clearUser: () => set({ user: null }),
}));