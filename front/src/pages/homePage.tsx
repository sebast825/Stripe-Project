import { Row, Container, Badge, Col } from "react-bootstrap";
import { CallToAction } from "../components/buttons/callToAction";
import { FeatureCard } from "../components/cards/featureCard";
import { TechHighlightCard } from "../components/cards/techHighlightCard";
export const HomePage = () => {
  return (
    <Container fluid className="p-0 margin-top">
      {/* Hero Section */}
      
      <div className="text-white text-center py-1 py-md-5 m-3 m-md-5 bg-primary border rounded-3 ">
        <Container className="py-5 ">
          <Badge bg="light" text="dark" className="mb-3 fs-6 p-2 fw-semibold ">
            🎨 DEMO TÉCNICO – Stripe + Arte Digital
          </Badge>

          <h1 className="display-4 fw-bold mb-3">
            Plataforma Creativa con Pagos y Suscripciones
          </h1>

          <p className="lead mb-4 opacity-75">
            Una demo completa que combina una experiencia visual artística con
            la integración profesional de Stripe Checkout, Subscriptions y
            Webhooks.
          </p>

          <div className="d-flex justify-content-center gap-2 flex-wrap">
            <Badge bg="light" text="dark" className="fs-6 p-2">
              Checkout
            </Badge>
            <Badge bg="light" text="dark" className="fs-6 p-2">
              Suscripciones
            </Badge>
            <Badge bg="light" text="dark" className="fs-6 p-2">
              Webhooks
            </Badge>
          </div>
        </Container>
      </div>
  {/*  CONTEXT SECTION   */}
      <Container className="my-5">
        <Row className="align-items-center">
          <Col md={6}>
            <h2 className="fw-bold mb-3">
              Una Plataforma de <span className="text-primary">Arte Digital</span>
            </h2>

            <p className="lead text-muted mb-4">
              Para esta demo, simulamos una plataforma donde los artistas pueden
              acceder a recursos creativos exclusivos mediante planes de suscripción.
            </p>

          </Col>

          <Col md={6} className="text-center">
            <div className="border rounded-4 p-4 bg-white shadow-sm">
              <h5 className="text-primary mb-3 fw-semibold">Planes Disponibles</h5>
              <div className="d-flex justify-content-around">
                
                <div className="text-center">
                  <div className="fs-1 mb-1">📝</div>
                  <div className="fw-bold">Boceto</div>
                </div>

                <div className="text-center">
                  <div className="fs-1 mb-1">🖼️</div>
                  <div className="fw-bold">Lienzo</div>
                </div>

                <div className="text-center">
                  <div className="fs-1 mb-1">🏆</div>
                  <div className="fw-bold">Galería</div>
                </div>

              </div>
            </div>
          </Col>
        </Row>
      </Container>
      {/* Features Section */}
      <Container className="">
        <Row className="g-4">
          <FeatureCard
            title="Checkout Integrado"
            text="Flujo completo de pagos únicos con Stripe Checkout. Incluye 3D Secure, manejo de errores y confirmación automática."
            tag="POST /api/checkout"
          ></FeatureCard>
          <FeatureCard
            title="Suscripciones"
            text="Sistema de suscripciones recurrentes. Cambio de plan, gestión de pagos fallidos y ciclo de vida completo."
            bgColor="success"
            tag="POST /api/subscriptions"
          ></FeatureCard>

          <FeatureCard
            title="Webhooks"
            text="Endpoint seguro para eventos Stripe. Sincronización en tiempo real con verificación de firma y manejo de eventos."
            bgColor="danger"
            tag="   POST /api/webhooks"
          ></FeatureCard>
        </Row>
      </Container>
      {/* ⭐ TECH HIGHLIGHTS  */}
      <Container className="py-5 border-top mt-5" >
          <h3 className="text-center fw-bold mb-4">
            Tech Highlights del Proyecto
          </h3>

          <Row className="g-4 text-center">
            <TechHighlightCard
              title="Seguridad Avanzada"
              icon="🔐"
              text="JWT con refresh tokens, roles/claims y comunicación frontend–backend segura."
            />
            <TechHighlightCard
              title="Integración con Stripe"
              icon="💳"
              text="Prorrateo automático, gestión de estados y sincronización confiable con Stripe."
            />
            <TechHighlightCard
              title="Arquitectura Escalable"
              icon="⚙️"
              text="Clean Architecture, separación de capas, middlewares y resiliencia en procesos."
            />

            <TechHighlightCard
              title="Frontend Moderno"
              icon="🌐"
              text="React + Zustand + TanStack Query, caching avanzado y rutas protegidas."
            />

            <TechHighlightCard
              title="Monitoreo & Logs"
              icon="📊"
              text="Registro de eventos críticos y trazabilidad completa de procesos."
            />

            <TechHighlightCard
              title="Performance & UX"
              icon="🚀"
              text="Respuestas rápidas, estados fluidos y arquitectura optimizada."
            />

    
          </Row>
    
      </Container>

      {/* CTA Section */}
      <Container className="text-center pb-5">
        <CallToAction
          action={function (): void {
            throw new Error("Function not implemented.");
          }}
          text={"¿Listo para explorar el demo?"}
        />
        {/* Footer */}
        <div className=" border-top pt-2">
          <p className="text-muted small">
            Este es un demo técnico. No se procesan pagos reales.
            <br />
            Implementación de referencia para desarrolladores.
          </p>
        </div>
      </Container>
    </Container>
  );
};
