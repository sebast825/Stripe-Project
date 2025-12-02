import { Button } from "react-bootstrap";
import { useUserStore } from "../states/auth/user.store";
import { useEffect, useState } from "react";
import { useGetCurrentSubscription } from "../hooks/subscription/useGetCurrentSubscription";
import type { userSubscriptionPlan } from "../types/userSubscriptionPlan.types";
import { UserSubscriptionCard } from "../components/userSubscriptionCard";
import { useRedirect } from "../hooks/useRedirect";
import { CardMessage } from "../components/cardMessage";
import { errorMessages } from "../constants/errorMessages";
import plansData from "../constants/testDataArt.json";
import ImageTextCard from "../components/ImageTextCard";
import type { SubscriptionTestData } from "../types/subscriptionTestData.types";
import { CallToAction } from "../components/buttons/callToAction";

// planMapping.ts
export const planMapping: Record<string, string> = {
  boceto: "1",
  lienzo: "2",
  galería: "3",
};

function Dashboard() {
  const user = useUserStore((state) => state.user);

  const { data: userPlan, error } = useGetCurrentSubscription();
  const [unPlan, setUnPlan] = useState<userSubscriptionPlan | null>(null);
  const { goToPlans } = useRedirect();
  const [currentPlanInfo, setCurrentPlanInfo] =
    useState<SubscriptionTestData | null>(null);

  function handlerCurrentPlanInfo() {
    const planId = planMapping[userPlan.plan.toLowerCase()];
    if (!planId) {
      setCurrentPlanInfo(null);
      return;
    }
    const match = plansData.find((p) => p.planId === planId);
    setCurrentPlanInfo(match || null);
  }

  useEffect(() => {
    if (userPlan == null) return;
    setUnPlan(userPlan);
    handlerCurrentPlanInfo();
  }, [userPlan]);

  {
    !error && (
      <CardMessage
        message={errorMessages.genericMessage("al cargar la subscripción")}
      />
    );
  }
  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100">
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
        </div>

        {currentPlanInfo && (
          <>
            <div className="w-100 px-lg-5">
              <h2 className="m-3 border-bottom">
                Informacion del plan {currentPlanInfo.name}
              </h2>
              <ImageTextCard
                title={""}
                content={currentPlanInfo.description}
                imageUrl={currentPlanInfo.photoUrl}
                altImg={currentPlanInfo.altText}
                imageLeft={false}
              ></ImageTextCard>{" "}
            </div>
            <small className="mt-2 " style={{ opacity: 0.8 }}>
              Vista simplificada para demostración. En una suscripción real,
              cada plan incluye beneficios y características adicionales.
            </small>
          </>
        )}

        {unPlan && <UserSubscriptionCard plan={unPlan} />}
        <CallToAction
          action={goToPlans}
          text="¿Querés conocer nuestras opciones de suscripción?"
        />
      </div>
    </>
  );
}

export default Dashboard;
