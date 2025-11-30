import { useGetUsers } from "../../hooks/useGetUsers";
import { useUserStore } from "../../states/auth/user.store";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import PaginationBtns from "../../components/paginationBtns";
import { usePagination } from "../../hooks/usePagination";
import InputRegex from "../../components/inputRegex";
import { useState } from "react";

function DashboardAdminPage() {
  const user = useUserStore((state) => state.user);
  const [fraseRegex, setFraseRegex] = useState<string>("");
  const { page, pageSize, goToPage } = usePagination();
  const { data: users } = useGetUsers(page, pageSize, fraseRegex);

  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100">
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
          <p>Panel Administrador</p>
        </div>
        <div className="w-100 ">
          <h2 className="mb-3 border-bottom">Usuarios</h2>
        </div>
        <InputRegex
          onFraseRegexChage={setFraseRegex}
          placeholder="Buscar usuario por nombre"
        />

        <Table striped bordered hover style={{ overflowX: "hidden" }}>
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
                <td
                  className="text-left"
                  style={{
                    maxWidth: "120px",
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                  }}
                >
                  {u.fullName}
                </td>
                <td>{u.plan}</td>
                <td>{u.status}</td>
                <td className="text-center">
                  <Link to={`/users/${u.id}`} state={{ userName: u.fullName }}>
                    Ver
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <PaginationBtns
          page={page}
          totalPages={users?.totalPages}
          goToPage={goToPage}
        />
      </div>
    </>
  );
}

export default DashboardAdminPage;
