import {
  Bar,
  BarChart,
  CartesianGrid,
  LabelList,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import type { PlanStats } from "../types/AdminDashboardStats";

interface Props {
  planDistribution: PlanStats[];
}

const PlanDistributionGraphic = ( {planDistribution}: Props ) => {
   //map the data for the graphic
  const chartData = planDistribution.map((p) => ({
    name: p.planName,
    value: p.count,
    percentage: p.percentage,
  }));
  //get color bor the bars from css variable --bs-primary
  const primaryColor = getComputedStyle(
    document.documentElement
  ).getPropertyValue("--bs-primary");

  return (
    <div className="w-100 d-flex justify-content-center flex-column align-items-center">
      <h4 >Usuarios por Plan</h4>

      <BarChart width={300} height={300} data={chartData} margin={{ top: 20 , left: -40}}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="name" />
        <YAxis />
        <Tooltip />
        <Bar dataKey="value" fill={primaryColor}>
          <LabelList
            dataKey="percentage"
            position="top"
            formatter={(val) => `${val}%`}
          />
        </Bar>{" "}
      </BarChart>
    </div>
  );
};

export default PlanDistributionGraphic;
