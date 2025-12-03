import { Nav, Navbar, Button } from "react-bootstrap";
import { useLocation } from "react-router-dom";
import { useAuthStore } from "../states/auth/auth.store";
import { useLogout } from "../hooks/auth/useLogout";
import { useRedirect } from "../hooks/useRedirect";

export const NavBar = () => {
  const { mutateAsync: logout } = useLogout();
  const location = useLocation();
  const { goToLogin,goToHome } = useRedirect();
  var refreshToken = useAuthStore((state) => state.refreshToken);

  const isLoginOrRegister =
    location.pathname == "/login" || location.pathname == "/register";

  const isHome = location.pathname == "/";

  async function handleLogout() {
    await logout({ refreshToken: refreshToken! });
  }
  return (
    <>
      <Navbar
        fixed="top"
        expand="lg"
        className="border-bottom shadow-sm bg-dark p-3"
      >
        <Navbar.Brand
          href="https://sebastianmolina.netlify.app/"
          target="_blank"
          className="fw-bold text-secondary"
        >
          Portfolio
        </Navbar.Brand>

        <Nav className="ms-auto">
          {isHome && <Button onClick={() => goToLogin()}> Login</Button>}
          {!isLoginOrRegister && !isHome && (
            <Button className="" onClick={() => handleLogout()}>
              Salir
            </Button>
          )}
          {isLoginOrRegister && (
            <Button className="" onClick={() => goToHome()}>
              Home
            </Button>
          )}
        </Nav>
      </Navbar>
    </>
  );
};
