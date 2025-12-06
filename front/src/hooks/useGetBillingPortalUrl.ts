import { useQuery } from "@tanstack/react-query";
import apiClient from "../api/client";

export const useGetBillingPortalUrl =()=> {

     return useQuery({
       queryKey: ["billing-portal-url"],
       queryFn: async () => {
         const res = await apiClient.get("/subscriptions/billing-portal");
         return res.data;
       },
       enabled: false
     });
}