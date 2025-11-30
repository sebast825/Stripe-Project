import { useNavigate } from "react-router-dom";
import apiClient from "../../api/client";
import { useMutation } from "@tanstack/react-query";
import useToastit from "../useToastit";
import { successMessages } from "../../constants/successMessages";
import { errorMessages } from "../../constants/errorMessages";

export const useRegister = () => {
  const navigate = useNavigate();
  const {success,error} = useToastit();
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
      return await apiClient.post("/user", {
        email,
        password,
        fullName,
      });
    },
    onSuccess: () => {
      success(successMessages.USER_CREATE_SUCCESS)
      navigate("/");
    },
    onError: (err: unknown) => {
      console.log(err);
      error(errorMessages.USER_CREATE_ERROR);
    },
  });
};
