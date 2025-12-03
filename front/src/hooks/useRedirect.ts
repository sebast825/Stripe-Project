import { useNavigate } from "react-router-dom";

export const useRedirect = () => {
  const navigate = useNavigate();

  const goToPlans = () => navigate("/plans");
  const goToDashboard = (role?: string) => {
    if (role === "Admin") {
      navigate("/admin/dashboard");
    } else {
      navigate("/dashboard");
    }
  };
  const goToLogin = () => navigate("/login");
  const goToRegister = () => navigate("/register");
  const goToPreviousPage = () => navigate(-1);
  const goToHome = () => navigate("/");

  return { goToPlans, goToDashboard, goToLogin, goToRegister,goToPreviousPage,goToHome };
};
