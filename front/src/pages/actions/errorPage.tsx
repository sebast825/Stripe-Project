import { Container, Card, Button } from "react-bootstrap";

export default function ErrorActionPage() {
  return (
    <Container className="d-flex justify-content-center align-items-center vh-100">
      <Card className="p-5 text-center shadow-sm border-danger">
        <h1 className="mb-3 text-danger">Ocurrió un error</h1>
        <h6 className="text-muted mb-4">
          La acción no pudo completarse. Intenta nuevamente.
        </h6>
        <Button variant="secondary" onClick={() => window.close()}>
          Cerrar esta pestaña
        </Button>
      </Card>
    </Container>
  );
}
