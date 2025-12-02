import { Nav, Navbar, Button } from "react-bootstrap";
import { useLocation } from "react-router-dom";
import { useAuthStore } from "../states/auth/auth.store";
import { useLogout } from "../hooks/auth/useLogout";

export const NavBar = () => {
  const { mutateAsync: logout } = useLogout();
  const location = useLocation();
  var refreshToken = useAuthStore((state) => state.refreshToken);

  const showBtn = location.pathname != "/" && location.pathname != "/register";
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
          {showBtn && (
            <Button className="" onClick={() => handleLogout()}>
              Salir
            </Button>
          )}
        </Nav>
      </Navbar>
    </>
  );
};
