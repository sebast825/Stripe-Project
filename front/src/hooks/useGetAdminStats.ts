import { useQuery } from "@tanstack/react-query";
import apiClient from "../api/client";
import type { AdminDashboardStats } from "../types/AdminDashboardStats";

export const useGetAdminStats = () => {
  return useQuery({
    queryKey: ["admin-stats"],
    queryFn: async (): Promise<AdminDashboardStats> => {
      const res = await apiClient.get("/admin/metrics");
      return res.data;
    },
  });
};
