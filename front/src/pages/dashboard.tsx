import { Button } from "react-bootstrap";
import { useUserStore } from "../states/auth/user.store";
import { useEffect, useState } from "react";
import { useGetCurrentSubscription } from "../hooks/subscription/useGetCurrentSubscription";
import type { userSubscriptionPlan } from "../types/userSubscriptionPlan.types";
import { UserSubscriptionCard } from "../components/userSubscriptionCard";
import { useNavigate } from "react-router-dom";

function Dashboard() {
  const user = useUserStore((state) => state.user);

  const { data: userPlan} = useGetCurrentSubscription();
  const [unPlan, setUnPlan] = useState<userSubscriptionPlan|null>(null);
  const boolean = false
    const navigate = useNavigate();
 
  useEffect(() => {setUnPlan(userPlan)}, [userPlan]);
 <p>Error al cargar los planes</p>;
  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div
          
          className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100"
        >
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
        </div>
      {unPlan && <UserSubscriptionCard plan={unPlan}/>
    
      }
             <Button onClick={()=>navigate("/plans")}>Nuestros Planes</Button>

     
      </div>
    </>

  );
}

export default Dashboard;
