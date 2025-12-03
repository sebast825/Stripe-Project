import { Badge, Card, Col } from "react-bootstrap"

export const FeatureCard = ({title,text,tag, bgColor = "primary"}:{title:string,text:string, tag?:string, bgColor?:string})=>{
   return (

      <Col md={4}>
          <Card className={`h-100 border-${bgColor} shadow`}>
            <Card.Body className="text-center p-4">
              <div className={`bg-${bgColor} text-white rounded-circle d-inline-flex justify-content-center align-items-center mb-3`}
                   style={{ width: '70px', height: '70px', fontSize: '2rem' }}>
                💳
              </div>
              <Card.Title as="h4" className="mb-3">
               {title}
              </Card.Title>
              <Card.Text className="text-muted">
             {text}
              </Card.Text>
              {tag &&    <Badge bg={bgColor} className="mt-2 p-2">
                  {tag}
                </Badge>
      }
            </Card.Body>
          </Card>
        </Col>
   )
}