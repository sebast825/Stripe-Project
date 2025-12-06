import { Row,  } from "react-bootstrap";
import { useGetAdminStats } from "../hooks/useGetAdminStats";
import { StatsCard } from "./cards/statsCard";
import PlanDistributionGraphic from "./planDistributionGraphic";
import LoadingSpinner from "./loadingSpinner";

const AdminStats = () => {
  const { data ,isLoading} = useGetAdminStats();
  const stats = {
    totalUsers: data?.totalUsers,
    totalRevenue: data?.totalRevenue.toLocaleString(),
    activeSubscriptions: data?.activeSubscriptions.toLocaleString(),
    totalSubscriptions: data?.totalSubscriptions,
    estimatedMonthlyRevenue: data?.estimatedMonthlyRevenue,
    canceledSubscriptions: data?.canceledSubscriptions,
    planDistribution: data?.planDistribution || [],
  };
  if(isLoading)return <LoadingSpinner/>

  return (
    <div
      className="p-3 d-flex flex-column flex-md-row gap-2 align-items-start"
      style={{ maxWidth: "1200px" }}
    >
      <div className="w-100 d-flex">
        <PlanDistributionGraphic planDistribution={stats.planDistribution} />
      </div>
      <div className="w-100 d-flex flex-column">
        <h4 className="text-center ">Estadisticas</h4>
        <Row className=" g-3 ">
          <StatsCard
            title="Ingresos Totales"
            estadistic={"US$" + stats.totalRevenue}
          />
          <StatsCard
            title="Ingresos Mensuales"
            estadistic={"US$" + stats.estimatedMonthlyRevenue}
          />
          <StatsCard
            title="Suscripciones Activas"
            estadistic={stats.activeSubscriptions}
          />

          <StatsCard title="Usuarios Totales" estadistic={stats.totalUsers} />
          <StatsCard
            title="Suscripciones Totales"
            estadistic={stats.totalSubscriptions}
          />
          <StatsCard
            title="Suscripciones Canceladas"
            estadistic={stats.canceledSubscriptions}
          />
        </Row>
      </div>
    </div>
  );
};

export default AdminStats;
