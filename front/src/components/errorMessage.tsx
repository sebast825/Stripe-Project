interface ErrorMessageProps {
  message?: string;
}

export function ErrorMessage({ message = "Ha ocurrido un error" }: ErrorMessageProps) {
  return (
    <div className="alert alert-danger text-center my-3">
      {message}
    </div>
  );
}
