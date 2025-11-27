import { Card, Button } from "react-bootstrap";
import type { userSubscriptionPlan } from "../types/userSubscriptionPlan.types";
import { useEffect, useState } from "react";
import { useGetBillingPortalUrl } from "../hooks/useGetBillingPortalUrl";
import { ErrorMessage } from "./errorMessage";
import { errorMessages } from "../constants/errorMessages";

interface UserSubscriptionCard {
  plan: userSubscriptionPlan;
}
export function UserSubscriptionCard({ plan }: UserSubscriptionCard) {
  const [startDate, setStartDate] = useState<string | null>(null);
  const [endDate, setEndDate] = useState<string | null>(null);
  const { data:billingPortalUrl, isFetching, error ,refetch } = useGetBillingPortalUrl();
  useEffect(() => {
    const currentEndDate = plan.currentPeriodEnd
      ? new Date(plan.currentPeriodEnd).toISOString().split("T")[0]
      : null;
    const currentStartDate = plan.currentPeriodEnd
      ? new Date(plan.currentPeriodEnd).toISOString().split("T")[0]
      : null;
    setStartDate(currentStartDate);
    setEndDate(currentEndDate);
  }, []);

  async function showBillingPortal() {
      const { data } = await refetch ();
    await window.open(billingPortalUrl, "_blank");
  }
  {
    !error && (
      <ErrorMessage
        message={errorMessages.genericMessage("al cargar el Portal de Usuario")}
      />
    );
  }

  return (
    <div className="w-100 px-lg-5">
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

          <Button
            variant="primary"
            className="mt-auto w-100"
            onClick={() => {
              showBillingPortal();
            }}
            disabled={isFetching}
          >
            Gestionar plan
          </Button>
        </Card.Body>
      </Card>
    </div>
  );
}
