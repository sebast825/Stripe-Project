import { Nav, Navbar, Button } from "react-bootstrap";
import { useAuth } from "../hooks/useAuth";
import { useLocation } from "react-router-dom";
import { useAuthStore } from "../states/auth/auth.store";

export const NavBar = () => {
  const { logout } = useAuth();
  const location = useLocation();
  var refreshToken = useAuthStore((state) => state.refreshToken);

  const showBtn = location.pathname != "/";
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
            <Button className="" onClick={() => logout(refreshToken!)}>
              Salir
            </Button>
          )}
        </Nav>
      </Navbar>
    </>
  );
};
