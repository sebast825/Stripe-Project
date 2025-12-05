import { useEffect, useState } from "react";
import { useDebounce } from "../hooks/useDebounce";

interface IInputRegex {
  onFraseRegexChage: (newFrase: string) => void;
  placeholder?: string;
}

function InputRegex({
  onFraseRegexChage,
  placeholder = "Buscar",
}: IInputRegex) {
  const [fraseRegex, setFraseRegex] = useState<string>("");
  const shouldFetch = useDebounce(fraseRegex, 500);

  useEffect(() => {
    if (!shouldFetch) return;
    onFraseRegexChage(fraseRegex);
  }, [fraseRegex, shouldFetch]);

  function updateRegEx(expresion: string) {
    setFraseRegex(expresion);
  }

  return (
    <div
      className="row m-2  d-flex flex-column justify-content-center align-items-center w-100"
      style={{ maxWidth: "400px" }}
    >
      <input
        className={`form-control input`}
        placeholder={placeholder}
        type="text"
        value={fraseRegex}
        onChange={(e) => updateRegEx(e.target.value)}
      />
    </div>
  );
}

export default InputRegex;
