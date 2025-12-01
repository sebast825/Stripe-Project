import { Button } from "react-bootstrap";
import { useRedirect } from "../../hooks/useRedirect";

interface IBackLink {
  variant?: string;
}

function BackLink({ variant }: IBackLink) {
  const { goToPreviousPage } = useRedirect();
  return (
    <div className="container justify-content-center d-flex">
      <Button
        variant={variant != undefined ? variant : "dark"}
        onClick={goToPreviousPage}
        className="btn-back"
      >
        Volver
      </Button>
    </div>
  );
}

export default BackLink;
