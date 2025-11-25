import { Container } from "react-bootstrap";

import { useUserStore } from "../states/auth/user.store";
import { usePlans } from "../hooks/subscription/usePlans";
import { PlansList } from "../components/PlansList";
import { useEffect, useState } from "react";
import { useCreateSubscriptionCheckout } from "../hooks/subscription/useCreateSubscriptionCheckout";
import { useGetCurrentSubscription } from "../hooks/subscription/useGetCurrentSubscription";
import type { userSubscriptionPlan } from "../types/userSubscriptionPlan.types";
import { UserSubscriptionCard } from "../components/userSubscriptionCard";

function Dashboard() {
  const user = useUserStore((state) => state.user);
  const { data: plans, isLoading, error } = usePlans();
  const { data: userPlan} = useGetCurrentSubscription();
  const [unPlan, setUnPlan] = useState<userSubscriptionPlan|null>(null);
  const {
    mutate: createCheckout,
    isSuccess,
    data,
  } = useCreateSubscriptionCheckout();
  useEffect(() => {
    console.log(userPlan)

    if (isSuccess && data) {
      window.open(data, "_blank");
    }
  }, [isSuccess, data]);

  useEffect(() => {setUnPlan(userPlan)}, [userPlan]);
  if (isLoading) return <p>Cargando...</p>;
  if (error) return <p>Error al cargar los planes</p>;
  return (
    <>
      <div className="flex-grow-1  d-flex flex-column vw-100 mt-5 pt-5 p-3 p-sm-5 ">
        <Container
          fluid
          className="bg-primary text-white text-center py-5 rounded-3 mb-4"
        >
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
        </Container>
      {unPlan && <UserSubscriptionCard plan={unPlan}/>}
       
        <PlansList plans={plans} onSelect={createCheckout} />
      </div>
    </>
  );
}

export default Dashboard;
