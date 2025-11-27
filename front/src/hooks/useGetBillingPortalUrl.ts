import { useQuery } from "@tanstack/react-query";
import apiClient from "../api/client";

export const useGetBillingPortalUrl =()=> {

     return useQuery({
       queryKey: ["plans"],
       queryFn: async () => {
         const res = await apiClient.get("/subscription/billing-portal");
         return res.data;
       }
     });
}