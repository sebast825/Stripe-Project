import type { SubscriptionPlan } from "../types/SubscriptionPlan.types";
import { Col, Row } from "react-bootstrap";
import { PlanCard } from "./PlanCard";

interface PlansListProps {
  plans: SubscriptionPlan[];
  onSelect: (planId:number ) => void;
}

export function PlansList({ plans, onSelect }: PlansListProps) {
  return (
    <Row>
      {plans?.map((p) => (
        <Col key={p.id} xs={12} lg={4} md={4} className="d-flex  flex-column">
          <PlanCard plan={p} onSelect={onSelect} />
        </Col>
      ))}
    </Row>
  );
}
