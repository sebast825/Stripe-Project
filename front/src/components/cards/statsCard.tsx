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
    <Col  >
      <Card className="shadow-sm text-center h-100 ">
        <Card.Body className="d-flex flex-column justify-content-center">
          <Card.Title> {title}</Card.Title>

          <h3 className="fw-bold text-primary"> {estadistic}</h3>
        </Card.Body>
      </Card>
    </Col>
  );
};
