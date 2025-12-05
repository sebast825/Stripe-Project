import { useEffect, useState } from "react"

export const useDebounce = (searchTerm : string, delay? : number ) : boolean => {
   const [isAviable, setIsAviable] = useState<boolean>(false);
   useEffect(()=>{
      setIsAviable(false)
      const timer = setTimeout(()=>{
         setIsAviable(true)
      },delay??500)
      return ()=> clearInterval(timer);
   },[searchTerm,delay])
   
   return isAviable;
}