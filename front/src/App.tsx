import "./App.css";
import { AppRoutes } from "./routes/appRoutes";
import "bootstrap/dist/css/bootstrap.min.css";
import { NavBar } from "./components/navbar";
import { useAuthRedirect } from "./hooks/auth/useAuthRedirect";

function App() {
  useAuthRedirect();
  return (
    <>
      <div className=" d-flex flex-column justify-content-start align-items-start vw-100 vh-100 ">
        <NavBar />
        <AppRoutes />
      </div>
    </>
  );
}

export default App;
