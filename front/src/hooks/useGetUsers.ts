import { useQuery } from "@tanstack/react-query";
import apiClient from "../api/client";
import type { UserWithSubscription } from "../types/UserWithSubscription.type";
import type { PagedResponseDto } from "../types/PagedResponse.types";

export const useGetUsers =  (page:number,pageSize:number,searchTerm?:string) => {
return  useQuery({
       queryKey: ["users",page,pageSize,searchTerm],
       queryFn: async () : Promise<PagedResponseDto<UserWithSubscription>>=> {
         const res = await apiClient.get(`/users?page=${page}&pageSize=${pageSize}`+ (searchTerm ? `&searchTerm=${searchTerm}` : ''));
         return res.data;
       }
     });

}