import { useParams ,useLocation} from "react-router-dom";
import { UserPaymentsTable } from "../../components/tables/userPaymentsTable";

export const UserDetailPage = () => {
  const location = useLocation();
  const { id } = useParams<{ id: string }>();
  const userName = location.state?.userName || "NameEmpty";


  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100">
          <h1 className="fw-bold">Detalles de {userName}!</h1>
          <p>Panel Administrador</p>
        </div>
        <UserPaymentsTable id={Number(id)}/>
      </div>
    </>
  );
};
