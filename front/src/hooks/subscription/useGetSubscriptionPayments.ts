import { useQuery } from "@tanstack/react-query";
import apiClient from "../../api/client";
import type { SubscriptionPaymentDto } from "../../types/SubscriptionPaymentDto.types";
import type { PagedResponseDto } from "../../types/PagedResponse.types";

export const useGetSubscriptionPayments =(userId : number,page:number,pageSize:number)=>{
   return useQuery({
    queryKey: ["suscrpitionPaymentRecord",userId,page,pageSize],
    queryFn: async () :Promise<PagedResponseDto<SubscriptionPaymentDto>>=> {
      const res = await apiClient.get(`/admin/users/${userId}/subscriptions-payments?page=${page}&pageSize=${pageSize}`);
      return res.data;
    }
  });
}