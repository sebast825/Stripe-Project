import { useMutation } from "@tanstack/react-query"
import apiClient from "../../api/client"

export const useCreateSubscriptionCheckout = ()=>{
   return useMutation({
      mutationFn: async(planId : number) =>{
         console.log(planId)
         const res = await apiClient.post("/subscription/checkout",planId);
         console.log(res)
         return res.data;
      }
   }

   )
}