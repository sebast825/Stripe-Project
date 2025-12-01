interface CardMessageProps {
  message?: string;
  type?: "error" | "warning";
}

export function CardMessage({ message = "Ha ocurrido un error",type="error" }: CardMessageProps) {
  return (
<div className={`alert ${type=="error"? "alert-danger" :"alert-warning" } text-center my-3`}>
      {message}
    </div>
  );
}