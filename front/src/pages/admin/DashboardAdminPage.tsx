import { useEffect } from "react";
import { useGetUsers } from "../../hooks/useGetUsers";
import { useUserStore } from "../../states/auth/user.store";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";

function DashboardAdminPage() {
  const user = useUserStore((state) => state.user);
  const { data: users } = useGetUsers(1,3,"s");

  useEffect(() => {
    console.log(users);
  }, [users]);

  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100">
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
          <p>Panel Administrador</p>
        </div>
        <Table striped bordered hover >
          <thead>
            <tr className="text-center">
              <th>Nombre</th>
              <th>Plan</th>
              <th>Estado</th>
              <th>Historial</th>
            </tr>
          </thead>
          <tbody>
            {users?.data.map((u: any) => (
              <tr key={u.id} className="text-center">
                <td className="text-left">{u.fullName}</td>
                <td>{u.plan}</td>
                <td>{u.status}</td>
                <td className="text-center">
                  <Link to={`/users/${u.id}`}>Ver</Link>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>
    </>
  );
}

export default DashboardAdminPage;
