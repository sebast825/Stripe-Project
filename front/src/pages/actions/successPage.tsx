import { ActionStatusPage } from "../../components/actionStatusPage";

export default function SuccessActionPage() {
  return (
    <ActionStatusPage
      title="Acción completada"
      text={`La operación se realizó correctamente.
Cuando terminemos de procesar la solicitud verás tu subscripción en el panel de usuarios.`}
      variant="success"
    />
  );
}
