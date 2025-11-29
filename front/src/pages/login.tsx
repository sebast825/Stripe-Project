import { useState } from "react";
import { Button, Col, Form, Row } from "react-bootstrap";
import { useRedirect } from "../hooks/useRedirect";
import { useLogin } from "../hooks/auth/useLogin";

function Login() {
  const { mutateAsync: login,isPending} = useLogin();
  const { goToDashboard } = useRedirect();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  async function handleSubmit(e: React.FormEvent<HTMLButtonElement>) {
    e.preventDefault();
    try {
      await login({ email, password });
      goToDashboard();
    } catch (err: any) {
      if (err.status === 401) {
        alert("Usuario o contraseña incorrectos");
        return;
      } else {
        alert("No pudimos establecer conexion con el servidor");
      }
    }
  }

  return (
    <>
      <Row className="w-100 vh-100 justify-content-center align-items-center m-0">
        <Col
          xs={11}
          sm={8}
          md={6}
          lg={4}
          className="p-4 border rounded-3 shadow-lg"
        >
          <h2 className="text-center mb-2 text-secondary ">Iniciar Sesión</h2>
          <Form onSubmit={() => {}} className="d-flex flex-column ">
            <Form.Group controlId="formBasicNombre">
              <Form.Label className="fw-bold"></Form.Label>
              <Form.Control
                type="text"
                placeholder="Usuario"
                onChange={(e) => setEmail(e.target.value)}
                value={email}
                className="p-2"
              />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label className=""></Form.Label>
              <Form.Control
                type="password"
                placeholder="Contraseña"
                onChange={(e) => setPassword(e.target.value)}
                value={password}
                className="p-2"
              />
            </Form.Group>
            <div className="mt-4 d-flex flex-column">
              <Button
                className=""
                onClick={(e) => handleSubmit(e)}
                variant="primary"
                type="submit"
                disabled={isPending}
              >
                Iniciar Sesión
              </Button>
            </div>
          </Form>
          <div className="mt-2 d-flex flex-column border-top pt-3">
            <Button variant="secondary" type="submit">
              Crear Usuario
            </Button>
          </div>
        </Col>
      </Row>
    </>
  );
}

export default Login;
