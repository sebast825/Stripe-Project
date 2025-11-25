import { useNavigate } from "react-router-dom";

export const useRedirect = () => {
    const navigate = useNavigate();

 const goToPlans = () => navigate("/plans");
  const goToDashboard = () => navigate("/dashboard");
  const goToLogin = () => navigate("/");
     
  return { goToPlans, goToDashboard, goToLogin };
   }
