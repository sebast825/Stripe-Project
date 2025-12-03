import { Button } from "react-bootstrap";

export const CallToAction = ({
  action,
  text,
}: {
  action: () => void;
  text: string;
}) => {
  return (
    <>
      <div
        className="bg-dark d-flex flex-column flex-md-row justify-content-around text-white text-center
         py-5 rounded-3  w-100 p-3 p-sm-5 gap-4"
         
      >
        <h3 className="text-capitalize">{text}</h3>
        <Button size="lg" variant="info" onClick={action}>
          Ver Más
        </Button>
      </div>
    </>
  );
};
