export interface SubscriptionPlan {
   id: number,
  planType: string;
  stripePriceId: string;
  price: number;
  name: string;
  description?: string;
  interval: string;
  features: string[];
}
