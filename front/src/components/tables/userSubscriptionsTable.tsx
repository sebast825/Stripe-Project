import { Table } from "react-bootstrap";
import { Link } from "react-router-dom";
import InputRegex from "../inputRegex";
import PaginationBtns from "../paginationBtns";

import { ErrorMessage } from "../errorMessage";
import { useState } from "react";
import { useGetUsers } from "../../hooks/useGetUsers";
import { usePagination } from "../../hooks/usePagination";

export function UsersubscriptionsTable() {
  const [fraseRegex, setFraseRegex] = useState<string>("");
  const { page, pageSize, goToPage } = usePagination();
  const { data: users } = useGetUsers(page, pageSize, fraseRegex);
  return (
    <>
      <InputRegex
        onFraseRegexChage={setFraseRegex}
        placeholder="Buscar usuario por nombre"
      />

      {users?.totalItems === 0 ? (
        <ErrorMessage message="No se encontraron usuarios" />
      ) : (
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
                <td>{u.plan ?? "-"}</td>
                <td>{u.status ?? "-"}</td>
                <td className="text-center">
                  <Link to={`/users/${u.id}`} state={{ userName: u.fullName }}>
                    Ver
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        
      )}
        <PaginationBtns
        page={page}
        totalPages={users?.totalPages!}
        goToPage={goToPage}
      />
    
    </>
  );
}
