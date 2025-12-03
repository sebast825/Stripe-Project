import { useMutation } from "@tanstack/react-query";
import apiClient from "../../api/client";
import useToastit from "../useToastit";

export const useCreateSubscriptionCheckout = () => {
  const { error } = useToastit();
  return useMutation({
    mutationFn: async (planId: number) => {
      console.log(planId);
      const res = await apiClient.post("/subscription/checkout", planId);
      return res.data;
    },
    onError: (err: any) => {
      error(err.message);
    },
  });
};
