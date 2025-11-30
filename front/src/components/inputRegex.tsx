import { useState } from "react"

interface IInputRegex{
   onFraseRegexChage : (newFrase : string) => void,
   placeholder ?: string
}

function InputRegex({onFraseRegexChage,placeholder = "Buscar"}: IInputRegex) {
   const [fraseRegex, setFraseRegex] = useState<string>();

   function updateRegEx(expresion: string) {
      setFraseRegex(expresion);
      onFraseRegexChage(expresion)
    }

   return(
      <div className="row m-2  d-flex flex-column justify-content-center align-items-center w-100" style={{maxWidth:"400px"}}>
      <input
        className= {`form-control input`}
        placeholder={placeholder}
        type="text"
        value={fraseRegex}
        onChange={(e) => updateRegEx(e.target.value)}
      />
    </div>
   )
}

export default InputRegex;