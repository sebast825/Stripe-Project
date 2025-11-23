import {

  Container

} from "react-bootstrap";

import { useUserStore } from "../states/auth/auth.store";

function Dashboard() {

const user = useUserStore((state) => state.user);

  return (
    <>
      <div className="flex-grow-1  d-flex flex-column vw-100 mt-5 pt-5 p-3 p-sm-5 ">
        <Container
          fluid
          className="bg-primary text-white text-center py-5 rounded-3 mb-4"
        >
          <h1 className="fw-bold">Bienvenido {user?.fullName}!</h1>
        </Container>
     

      </div>
    </>
  );
}

export default Dashboard;
