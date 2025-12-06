import { useQuery } from '@tanstack/react-query';
import apiClient from '../../api/client';

export function usePlans() {
  return useQuery({
    queryKey: ["plans"],
    queryFn: async () => {
      const res = await apiClient.get("/subscriptions/plans");
      return res.data;
    }
  });
}
