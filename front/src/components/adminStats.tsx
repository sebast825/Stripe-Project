import { Card, Row, Col } from "react-bootstrap";
import { useGetAdminStats } from "../hooks/useGetAdminStats";

const AdminStats = () => {
  const { data } = useGetAdminStats();
  const stats = {
    totalUsers: data?.totalUsers,
    totalRevenue: data?.totalRevenue,
    activeSubscriptions: data?.activeSubscriptions,
    totalSubscriptions: data?.totalSubscriptions,
    estimatedMonthlyRevenue: data?.estimatedMonthlyRevenue,
    canceledSubscriptions: data?.canceledSubscriptions,
  };

  return (
    <div className="p-3">
      <Row className="g-3 row-cols-1 row-cols-md-3 d-flex justify-content-center">
        <Col style={{ maxWidth: "400px" }}>
          <Card className="shadow-sm">
            <Card.Body>
              <Card.Title>Ingresos Totales</Card.Title>
              <h3>US$ {stats.totalRevenue?.toLocaleString()}</h3>
            </Card.Body>
          </Card>
        </Col>

        <Col style={{ maxWidth: "400px" }}>
          <Card className="shadow-sm">
            <Card.Body>
              <Card.Title>Ingresos Mensuales Estimados</Card.Title>
              <h3>US$ {stats.estimatedMonthlyRevenue?.toLocaleString()}</h3>
            </Card.Body>
          </Card>
        </Col>
        <Col style={{ maxWidth: "400px" }}>
          <Card className="shadow-sm">
            <Card.Body>
              <Card.Title>Suscripciones Activas</Card.Title>
              <h3>{stats.activeSubscriptions}</h3>
            </Card.Body>
          </Card>
        </Col>
        <Col style={{ maxWidth: "400px" }}>
          <Card className="shadow-sm">
            <Card.Body>
              <Card.Title>Usuarios Totales</Card.Title>
              <h3>{stats.totalUsers}</h3>
            </Card.Body>
          </Card>
        </Col>
        <Col style={{ maxWidth: "400px" }}>
          <Card className="shadow-sm">
            <Card.Body>
              <Card.Title>Total de Suscripciones</Card.Title>
              <h3>{stats.totalSubscriptions}</h3>
            </Card.Body>
          </Card>
        </Col>

        <Col style={{ maxWidth: "400px" }}>
          <Card className="shadow-sm">
            <Card.Body>
              <Card.Title>Suscripciones Canceladas</Card.Title>
              <h3>{stats.canceledSubscriptions}</h3>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </div>
  );
};

export default AdminStats;
