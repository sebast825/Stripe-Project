# Stripe Integration

This service provides a complete Stripe integration for subscription management. It creates Checkout Sessions, processes Stripe webhooks with idempotent and timestamp-safe logic, and updates the internal subscription state in the database. The application uses an existing authentication module to identify the user initiating the payment flow, but all payment confirmation and status updates rely exclusively on Stripe’s webhook events.

# Requirements
### 1 Requirements
- **SQL Server** (local or remote instance)  
- **.NET 8 SDK**  
- **Node.js + npm**  

### 2 Create Products and Prices in Stripe

Go to your Stripe Dashboard → **Products**  
Create the subscription products and their corresponding **Price IDs** (e.g., *Galería*, *Lienzo*, *Boceto*).

Copy the generated **Price IDs**, as the backend requires valid Stripe prices to create Checkout Sessions.

> 💡 **Important**  
> This demo does **not** store plans in a database.  
> Price IDs are hardcoded under the `DemoPlans` folder.  
> Update the `StripePriceId` value for each plan with the Price ID you created in Stripe.

If you create more than three plans (or fewer), make sure the number of demo plan files matches the number of products you want to support.

## 3. Configure `appsettings.json`

Use the `appsettings.example.json` file as a template to create your own `appsettings.json`.

# Run
```bash

# Run the Backend

#From the backend project directory (`back/Payments/Api`):
dotnet restore
dotnet run

# Run the Frontend
# From the frontend project directory (front):

npm install
npm run dev
```


# Payment Flow Architecture

General flow:

- The backend creates a **Checkout Session** based on the selected subscription.
- The user is redirected to **Stripe Checkout**.
- Stripe processes the payment.
- Stripe sends **webhooks** to this service.
- Webhooks are validated, processed idempotently, and update the internal state in the database.
- The frontend queries the backend to retrieve the real subscription status.

> The final confirmation of a payment never depends on the frontend; it always comes from Stripe via webhooks.

---

# Webhooks – Robust Processing

## Idempotency

Each webhook processed is recorded to prevent:

- double record creation  
- duplicate updates  
- inconsistencies caused by Stripe retries  

If the webhook was already processed → it is safely ignored.

## Ordering & Eventual Consistency

Stripe does **not** guarantee event order.  
The processor applies:

- Timestamp comparison between incoming event and current internal state  
- “Only update if this event is more recent than the existing state”  

This prevents outdated webhooks from overwriting correct data.

## Handled Events

- `customer.subscription.created`  
- `invoice.payment_succeeded`  
- `invoice.payment_failed`  
- `customer.subscription.updated`  
- `customer.subscription.deleted`  

---

# Authentication

This project reuses an existing authentication layer (**JWT**, **Refresh Tokens**, **Rate Limiting**).  
It is **not** part of the Stripe integration itself. Its only purpose is to identify the authenticated user who starts a checkout or accesses the billing portal.

For more details about the authentication module, refer to:
> https://github.com/sebast825/AuthService/releases/tag/v2.0.0
