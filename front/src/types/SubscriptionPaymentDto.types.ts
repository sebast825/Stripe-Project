export interface SubscriptionPaymentDto {
  amountPaid: number;
  currency: string;
  status: string;
  paidAt: string | null; 
  invoiceUrl: string;
  periodEnd: string; 
  periodStart: string;
}