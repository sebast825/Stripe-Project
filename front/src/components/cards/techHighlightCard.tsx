import { Col } from "react-bootstrap";

export const TechHighlightCard = ({
  icon,
  title,
  text,
}: {
  icon?: string;
  title: string;
  text: string;
}) => {
  return (
    <Col md={4}>
      <div className="p-4 border rounded-4 border-primary  h-100 shadow-sm">
        {icon && <div className="fs-2 mb-2">{icon}</div>}
        <h5 className="fw-bold">{title}</h5>
        <p className="text-muted small mb-0">{text}</p>
      </div>
    </Col>
  );
};
