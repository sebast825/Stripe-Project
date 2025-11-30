import "./App.css";
import { AppRoutes } from "./routes/appRoutes";
import "bootstrap/dist/css/bootstrap.min.css";
import { NavBar } from "./components/navbar";
import { useAuthRedirect } from "./hooks/auth/useAuthRedirect";
import { ToastContainer, Flip } from "react-toastify";

function App() {
  useAuthRedirect();
  return (
    <>
      <div className=" d-flex flex-column justify-content-start align-items-start vw-100 vh-100 ">
        <NavBar />
        <AppRoutes />
        <ToastContainer
          position="top-right"
          autoClose={3000} 
          hideProgressBar={false}
          newestOnTop={true}
          closeOnClick
          pauseOnFocusLoss={true}
          theme="colored"
          transition={Flip}
        />
      </div>
    </>
  );
}

export default App;
