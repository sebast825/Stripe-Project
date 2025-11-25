import { useEffect, useState } from "react";
import { Button, Col, Form, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

function Login() {
  const { login, error, setError } = useAuth();
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  async function handleSubmit(e: React.FormEvent<HTMLButtonElement>) {
    e.preventDefault();
    console.log(email, password);
    await login("test@gmail.com", "stringstringstring");
    //await login(email, password);
  }
  useEffect(() => {
    if (error) {
      alert("Usuario o contraseña incorrectos");
    }
    setError(false);
  }, [error]);

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
              >
                Iniciar Sesión
              </Button>
            </div>
          </Form>
          <div className="mt-2 d-flex flex-column border-top pt-3">
            <Button
              onClick={() => {
                navigate("/register");
              }}
              variant="secondary"
              type="submit"
            >
              Crear Usuario
            </Button>
          </div>
        </Col>
      </Row>
    </>
  );
}

export default Login;
