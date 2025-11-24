import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import ListGroup from "react-bootstrap/ListGroup";
import type { SubscriptionPlan } from "../types/SubscriptionPlan.types";


interface PlanCardProps {
  plan: SubscriptionPlan;
  onSelect: (planId:number) => void;
}

export function PlanCard({ plan, onSelect }: PlanCardProps) {



  return (
    <Card className="mb-3 shadow-sm min-h-100">
      <Card.Body>
        <div className="d-flex justify-content-between align-items-center mb-2">
          <Card.Title className="fw-bold mb-0">{plan.name}</Card.Title>

          <h3 className="fw-semibold mb-0">
            ${plan.price}
            <small className="text-muted" style={{ fontSize: "0.8rem" }}>
              /{plan.interval}
            </small>
          </h3>
        </div>

        {plan.description && (
          <Card.Subtitle className="text-muted mb-2 mt-1">
            {plan.description}
          </Card.Subtitle>
        )}

        <ListGroup variant="flush" className="mt-3">
          {plan.features?.map((f: string, i: number) => (
            <ListGroup.Item key={i}>{f}</ListGroup.Item>
          ))}
        </ListGroup>

        <Button
          variant="primary"
          className="mt-3 w-100 "
          onClick={() => onSelect(plan.id)}
        >
          Elegir plan
        </Button>
      </Card.Body>
    </Card>
  );
}
