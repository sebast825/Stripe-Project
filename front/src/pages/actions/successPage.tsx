import { Container, Card, Button } from "react-bootstrap";

export default function SuccessActionPage() {
  return (
    <Container className="d-flex justify-content-center align-items-center vh-100 ">
      <Card className="p-5 text-center shadow-sm border-success ">
        <h1 className="mb-3 text-success">Acción completada</h1>
        <h6 className="text-muted mb-4">La operación se realizó correctamente.</h6>
        <Button variant="secondary" onClick={() => window.close()}>
          Puedes cerrar esta pestaña
        </Button>
      </Card>
    </Container>
  );
}
