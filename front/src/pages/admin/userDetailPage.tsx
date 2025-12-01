import { useParams } from "react-router-dom";
import { useGetSubscriptionPayments } from "../../hooks/subscription/useGetSubscriptionPayments";
import { Table } from "react-bootstrap";
import type { SubscriptionPaymentDto } from "../../types/SubscriptionPaymentDto.types";
import { useLocation } from "react-router-dom";
import { usePagination } from "../../hooks/usePagination";
import PaginationBtns from "../../components/paginationBtns";

export const UserDetailPage = () => {
  const location = useLocation();
  const { id } = useParams<{ id: string }>();
  const { page, pageSize, goToPage } = usePagination();
  const userName = location.state?.userName || "NameEmpty";

  const { data: payments } = useGetSubscriptionPayments(
    Number(id),
    page,
    pageSize
  );
  return (
    <>
      <div className=" margin-top  d-flex flex-column justify-content-center align-items-center w-100 p-3 p-sm-5 ">
        <div className="bg-primary text-white text-center py-5 rounded-3 mb-4 w-100">
          <h1 className="fw-bold">Detalles de {userName}!</h1>
          <p>Panel Administrador</p>
        </div>
        <Table striped bordered hover style={{ overflowX: "auto" }}>
          <thead>
            <tr className="text-center">
              <th>Monto</th>
              <th>Moneda</th>
              <th>Estado</th>
              <th>Fecha de Pago</th>
              <th>Periodo</th>
              <th>Factura</th>
            </tr>
          </thead>
          <tbody>
            {payments?.data
              ?.filter(
                (payment): payment is SubscriptionPaymentDto => payment !== null
              )
              .map((payment, index) => (
                <tr key={index} className="text-center">
                  <td>${(payment.amountPaid / 100).toFixed(2)}</td>
                  <td>{payment.currency}</td>
                  <td>
                    <span
                      className={`badge ${
                        payment.status === "paid"
                          ? "bg-success"
                          : payment.status === "pending"
                          ? "bg-warning"
                          : payment.status === "failed"
                          ? "bg-danger"
                          : "bg-secondary"
                      }
                      p-2
                      `}
                    >
                      {payment.status}
                    </span>
                  </td>
                  <td>
                    {payment.paidAt
                      ? new Date(payment.paidAt).toLocaleDateString()
                      : "No pagado"}
                  </td>
                  <td>
                    {new Date(payment.periodStart).toLocaleDateString()} -{" "}
                    {new Date(payment.periodEnd).toLocaleDateString()}
                  </td>
                  <td>
                    {payment.invoiceUrl ? (
                      <a
                        href={payment.invoiceUrl}
                        target="_blank"
                        rel="noopener noreferrer"
                        className="btn btn-sm btn-outline-primary"
                      >
                        Ver Factura
                      </a>
                    ) : (
                      "N/A"
                    )}
                  </td>
                </tr>
              ))}
          </tbody>
        </Table>
        <PaginationBtns
          page={page}
          totalPages={payments?.totalPages!}
          goToPage={goToPage}
        />
      </div>
    </>
  );
};
