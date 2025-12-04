import { ActionStatusPage } from "../../components/actionStatusPage";

export default function ErrorActionPage() {
  return (
    <ActionStatusPage
      title="Ocurrió un error"
      text="La acción no pudo completarse. Intenta nuevamente."
      variant="danger"
    />
  );
}
