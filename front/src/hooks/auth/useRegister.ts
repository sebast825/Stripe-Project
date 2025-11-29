import { useNavigate } from "react-router-dom";
import apiClient from "../../api/client";
import { useMutation } from "@tanstack/react-query";

export const useRegister = () => {
  const navigate = useNavigate();
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
      alert("Usuario creado con éxito");
      navigate("/");
    },
    onError: (error: unknown) => {
      console.error("Error al crear el usuario", error);
      alert("Error al crear el usuario");
    },
  });
};
