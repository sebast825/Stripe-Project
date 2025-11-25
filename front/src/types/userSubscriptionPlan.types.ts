export interface userSubscriptionPlan {
  plan: string;
  id: number;
  startDate: Date;
  currentPeriodEnd: Date;
  status: string;
}
