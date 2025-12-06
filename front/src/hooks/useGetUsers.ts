import { useQuery } from "@tanstack/react-query";
import apiClient from "../api/client";
import type { PagedResponseDto } from "../types/PagedResponse.types";
import type { UserWithSubscription } from "../types/userWithSubscription.type";

export const useGetUsers =  (page:number,pageSize:number,searchTerm?:string) => {
return  useQuery({
       queryKey: ["users",page,pageSize,searchTerm],
       queryFn: async () : Promise<PagedResponseDto<UserWithSubscription>>=> {
         const res = await apiClient.get(`/users?page=${page}&pageSize=${pageSize}`+ (searchTerm ? `&searchTerm=${searchTerm}` : ''));
         return res.data;
       }
     });

}