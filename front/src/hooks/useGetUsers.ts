import { useQuery } from "@tanstack/react-query";
import apiClient from "../api/client";

export const useGetUsers =  (page:number,pageSize:number,searchTerm?:string) => {
return  useQuery({
       queryKey: ["users",page,pageSize,searchTerm],
       queryFn: async () => {
         const res = await apiClient.get(`/users?page=${page}&pageSize=${pageSize}`+ (searchTerm ? `&searchTerm=${searchTerm}` : ''));
         return res.data;
       }
     });

}