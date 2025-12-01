export interface PlanStats {
  planName: string;
  count: number;
  percentage: number;
}

export interface AdminDashboardStats {
  totalUsers: number;
  activeSubscriptions: number;
  totalRevenue: number;

  totalSubscriptions: number;
  canceledSubscriptions: number;
  estimatedMonthlyRevenue: number;

  planDistribution: ReadonlyArray<PlanStats>;
}
