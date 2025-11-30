import { useUserStore } from "../../states/auth/user.store";


function DashboardAdminPage() {
  const user = useUserStore((state) => state.user);





  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100">
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
          <p>Panel Administrador</p>
        </div>

      </div>
    </>
  );
}

export default DashboardAdminPage;
