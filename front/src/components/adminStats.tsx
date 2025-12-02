import { Row } from "react-bootstrap";
import { useGetAdminStats } from "../hooks/useGetAdminStats";
import { StatsCard } from "./cards/statsCard";
import PlanDistributionGraphic from "./planDistributionGraphic";

const AdminStats = () => {
  const { data } = useGetAdminStats();
  const stats = {
    totalUsers: data?.totalUsers,
    totalRevenue: data?.totalRevenue.toLocaleString(),
    activeSubscriptions: data?.activeSubscriptions.toLocaleString(),
    totalSubscriptions: data?.totalSubscriptions,
    estimatedMonthlyRevenue: data?.estimatedMonthlyRevenue,
    canceledSubscriptions: data?.canceledSubscriptions,
    planDistribution: data?.planDistribution || [],
  };

  return (
    <div className="p-3">
      <Row className="g-3 d-flex justify-content-center">
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

      <PlanDistributionGraphic planDistribution={stats.planDistribution} />
    </div>
  );
};

export default AdminStats;
