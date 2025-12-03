import { useMutation } from "@tanstack/react-query";
import apiClient from "../../api/client";
import useToastit from "../useToastit";

export const useCreateSubscriptionCheckout = () => {
  const { info } = useToastit();
  return useMutation({
    mutationFn: async (planId: number) => {
      console.log(planId);
      const res = await apiClient.post("/subscription/checkout", planId);
      return res.data;
    },
    onError: (err: any) => {
      if (err.data.Error == "ValidationException") {
        info(
          "Ya tenés una subscripción activa. Podés cambiar de plan desde 'Gestionar Plan' en el dashboard"          
        );
      }
    },
  });
};
