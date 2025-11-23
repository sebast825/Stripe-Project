import type { User } from "./user.types";

export interface LoginResponse {
  message: string;
  user: User;
  accessToken: string;
  refreshToken: string;
}