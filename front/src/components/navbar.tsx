import { Nav, Navbar, Button } from "react-bootstrap";
import { useLocation } from "react-router-dom";
import { useAuthStore } from "../states/auth/auth.store";
import  { useLogout } from "../hooks/auth/useLogout";
import { useRedirect } from "../hooks/useRedirect";

export const NavBar = () => {
  const {mutateAsync:logout}= useLogout()
  const {goToLogin} = useRedirect();
  const location = useLocation();
  var refreshToken = useAuthStore((state) => state.refreshToken);

  const showBtn = location.pathname != "/" && location.pathname != "/register";
  async function handleLogout() {
    try {
      await logout({refreshToken: refreshToken! });
      goToLogin();
    } catch (error) {
      alert("Error al cerrar sesión");
    }   
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
            <Button className="" onClick={() =>handleLogout()}>
              Salir
            </Button>
          )}
        </Nav>
      </Navbar>
    </>
  );
};
