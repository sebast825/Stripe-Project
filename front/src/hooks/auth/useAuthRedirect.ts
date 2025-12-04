
import { useEffect } from "react";
import { useAuthStore } from "../../states/auth/auth.store";
import { useRedirect } from "../useRedirect";
import { useLocation } from "react-router-dom";

// Redirects to login when the user becomes unauthenticated.
// Prevents infinite retry loops after a 401 response.
export function useAuthRedirect() {
   const {goToLogin} = useRedirect();
   const {pathname} = useLocation();
  const isAuthenticated = useAuthStore((s) => s.isAuthenticated);
 
    const publicRoutes = ["/", "/login", "/register", "/success"];

  useEffect(() => {
    if (!isAuthenticated && !publicRoutes.includes(pathname)) {
      goToLogin();
    }
  }, [isAuthenticated]);
}
