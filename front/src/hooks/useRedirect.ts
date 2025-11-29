import { useNavigate } from "react-router-dom";

export const useRedirect = () => {
  const navigate = useNavigate();

  const goToPlans = () => navigate("/plans");
  const goToDashboard = () => navigate("/dashboard");
  const goToLogin = () => navigate("/");
  const goToRegister = () => navigate("/register");

  return { goToPlans, goToDashboard, goToLogin,goToRegister };
};
