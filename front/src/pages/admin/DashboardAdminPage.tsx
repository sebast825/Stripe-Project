import { useUserStore } from "../../states/auth/user.store";
import { UsersubscriptionsTable } from "../../components/tables/userSubscriptionsTable";
import AdminDashboardStats from "../../components/adminStats";
import { useState } from "react";
import ToggleButtons from "../../components/buttons/toggleButtons";

function DashboardAdminPage() {
  const user = useUserStore((state) => state.user);
  type Section = "stats" | "users";
  const [activeSection, setActiveSection] = useState<Section>("stats");

  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div className="bg-primary text-white text-center py-4  rounded-3 w-100">
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
          <p>Panel Administrador</p>
        </div>
        <ToggleButtons
          textButton1={"Estadisticas"}
          textButton2={"Usuarios"}
          onClickButton1={() => setActiveSection("stats")}
          onClickButton2={() => setActiveSection("users")}
        ></ToggleButtons>

        {activeSection === "stats" && (
          <>
            <div className=" border-bottom w-100"></div>
            <AdminDashboardStats />
          </>
        )}

        {activeSection === "users" && (
          <>
            <UsersubscriptionsTable />
          </>
        )}
      </div>
    </>
  );
}

export default DashboardAdminPage;
