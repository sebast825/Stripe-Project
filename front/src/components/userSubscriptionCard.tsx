import { Card, Button } from "react-bootstrap";
import type { userSubscriptionPlan } from "../types/userSubscriptionPlan.types";
import { useEffect, useState } from "react";

interface UserSubscriptionCard {
  plan: userSubscriptionPlan;
  onManage?: () => void;
}
export function UserSubscriptionCard({ plan, onManage }: UserSubscriptionCard) {
  const [startDate, setStartDate] = useState<string | null>(null);
  const [endDate, setEndDate] = useState<string | null>(null);

  useEffect(() => {
    console.log("currentStartDate");

    const currentEndDate = plan.currentPeriodEnd
      ? new Date(plan.currentPeriodEnd).toISOString().split("T")[0]
      : null;
    const currentStartDate = plan.currentPeriodEnd
      ? new Date(plan.currentPeriodEnd).toISOString().split("T")[0]
      : null;
    setStartDate(currentStartDate);
    setEndDate(currentEndDate);
  }, []);

  return (

      <div>
        <h2 className="mb-3 border-bottom">Mi Plan</h2>
        <Card
          className=" d-flex flex-column mx-auto mb-4"
          style={{ maxWidth: "500px" }}
        >
          <Card.Body>
            <div className="d-flex justify-content-between align-items-center mb-2">
              <Card.Title className="fw-bold mb-0">{plan.plan}</Card.Title>
              <span className="fw-semibold mb-0">{plan.status}</span>
            </div>

            <Card.Body className="text-muted d-flex flex-column gap-1">
              <div>
                <span className="fw-bold">Inicio:</span> {startDate}
              </div>
              <div>
                <span className="fw-bold">Fin actual:</span> {endDate}
              </div>
            </Card.Body>

            {onManage && (
              <Button
                variant="primary"
                className="mt-auto w-100"
                onClick={onManage}
              >
                Gestionar plan
              </Button>
            )}
          </Card.Body>
        </Card>
      </div>

  );
}
