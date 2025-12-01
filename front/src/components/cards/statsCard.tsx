import { Card, Col } from "react-bootstrap";

export const StatsCard = ({
  title,
  estadistic,
}: {
  title: string;
  estadistic: any;
}) => {
  if (!estadistic) return null;
  return (
    <Col md={4} style={{ maxWidth: "300px" }}>
      <Card className="shadow-sm text-center">
        <Card.Body>
          <Card.Title> {title}</Card.Title>

          <h3 className="fw-bold text-primary"> {estadistic}</h3>
        </Card.Body>
      </Card>
    </Col>
  );
};
