import { Button, Card, Container } from "react-bootstrap"

export const ActionStatusPage= ({title,text,variant}:{title:string,text:string,variant:string})=>{
   return(
  <Container className="d-flex justify-content-center align-items-center vh-100">
      <Card className={`p-5 text-center shadow-sm border-${variant}`}>
        <h1 className={`mb-3 text-${variant}`}>{title}</h1>
        <h6 className="text-muted mb-4" style={{ whiteSpace: 'pre-line' }}>
          {text}
        </h6>
        <Button variant="secondary"  onClick={() => window.close()}>
          Cerrar esta pestaña
        </Button>
      </Card>
    </Container>
   )
}