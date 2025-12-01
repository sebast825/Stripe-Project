import { Table } from "react-bootstrap";
import { useGetSubscriptionPayments } from "../../hooks/subscription/useGetSubscriptionPayments";
import { usePagination } from "../../hooks/usePagination";
import type { SubscriptionPaymentDto } from "../../types/SubscriptionPaymentDto.types";
import PaginationBtns from "../paginationBtns";
import { CardMessage } from "../cardMessage";

export function UserPaymentsTable({ id }: { id: number }) {
  const { page, pageSize, goToPage } = usePagination();

  const { data: payments } = useGetSubscriptionPayments(id, page, pageSize);
  return (
    <>
      <div className="w-100 ">
        <h2 className="mb-3 border-bottom">Historial de Pagos</h2>
      </div>
      {payments?.totalItems === 0 ? (
        <CardMessage
          type="warning"
          message="El usuario no tiene facturas disponibles"
        />
      ) : (
        <div style={{ overflowX: "auto", width: "100%" }}>
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
                  (payment): payment is SubscriptionPaymentDto =>
                    payment !== null
                )
                .map((payment, index) => (
                  <tr
                    key={index}
                    className="text-center"
                    style={{ verticalAlign: "middle" }}
                  >
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
        </div>
      )}
      <PaginationBtns
        page={page}
        totalPages={payments?.totalPages!}
        goToPage={goToPage}
      />
    </>
  );
}
