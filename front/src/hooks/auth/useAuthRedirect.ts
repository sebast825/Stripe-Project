
import { useEffect } from "react";
import { useAuthStore } from "../../states/auth/auth.store";
import { useRedirect } from "../useRedirect";

// Redirects to login when the user becomes unauthenticated.
// Prevents infinite retry loops after a 401 response.
export function useAuthRedirect() {
   const {goToLogin} = useRedirect();
  const isAuthenticated = useAuthStore((s) => s.isAuthenticated);

  useEffect(() => {
    if (!isAuthenticated) {
      goToLogin();
    }
  }, [isAuthenticated]);
}
