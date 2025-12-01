import { useEffect } from "react";
import { PlansList } from "../components/PlansList";
import { usePlans } from "../hooks/subscription/usePlans";
import { useCreateSubscriptionCheckout } from "../hooks/subscription/useCreateSubscriptionCheckout";
import BackLink from "../components/buttons/backLink";

export function PlansPage() {
  const { data: plans, isLoading, error } = usePlans();
  const {
    mutate: createCheckout,
    isSuccess,
    data,
    isPending: isCreatingCheckout,
  } = useCreateSubscriptionCheckout();
  useEffect(() => {
    if (isSuccess && data) {
      window.open(data, "_blank");
    }
  }, [isSuccess, data]);

  if (isLoading) return <p>Cargando...</p>;
  if (error) return;
  return (
    <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
      <div className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100">
        <h1 className="fw-bold">Nuestros Planes</h1>
      </div>
      <PlansList
        plans={plans}
        onSelect={createCheckout}
        isPending={isCreatingCheckout}
      />
      <BackLink />
    </div>
  );
}
