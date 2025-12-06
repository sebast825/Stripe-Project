import { useQuery } from "@tanstack/react-query";
import apiClient from "../../api/client";

export const useGetCurrentSubscription =()=>{
   return useQuery({
    queryKey: ["userPlan"],
    queryFn: async () => {
      const res = await apiClient.get("/subscriptions/current");
      return res.data;
    }
  });
}