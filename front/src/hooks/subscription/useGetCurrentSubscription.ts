import { useQuery } from "@tanstack/react-query";
import apiClient from "../../api/client";

export const useGetCurrentSubscription =()=>{
   return useQuery({
    queryKey: ["userPlan"],
    queryFn: async () => {
      const res = await apiClient.get("/subscription/current");
            console.log(res.data)
      return res.data;
    }
  });
}