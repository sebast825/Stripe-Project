
import { BrowserRouter } from 'react-router-dom'
import './App.css'
import { AppRoutes } from './routes/appRoutes'
import 'bootstrap/dist/css/bootstrap.min.css';
import { NavBar } from './components/navbar';

function App() {

  return (
    <>
    <BrowserRouter>
              <div className='d-flex flex-column  align-items-center vh-100 vw-100'>

   <NavBar/>
    <AppRoutes />
  </div>

  </BrowserRouter>
  
    </>
  )
}

export default App
