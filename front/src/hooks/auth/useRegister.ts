import apiClient from "../../api/client";
import { useMutation } from "@tanstack/react-query";
import useToastit from "../useToastit";
import { successMessages } from "../../constants/successMessages";
import { errorMessages } from "../../constants/errorMessages";
import { useRedirect } from "../useRedirect";

export const useRegister = () => {
  const { goToLogin } = useRedirect();
  const { success, error } = useToastit();
  
  return useMutation({
    mutationFn: async ({
      email,
      password,
      fullName,
    }: {
      email: string;
      password: string;
      fullName: string;
    }) => {
      return await apiClient.post("/users", {
        email,
        password,
        fullName,
      });
    },
    onSuccess: () => {
      success(successMessages.USER_CREATE_SUCCESS);
      goToLogin();
    },
    onError: (err: unknown) => {
      console.log(err);
      error(errorMessages.USER_CREATE_ERROR);
    },
  });
};
