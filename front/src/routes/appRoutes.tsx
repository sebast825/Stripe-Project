import { Routes, Route } from 'react-router-dom';
import Login from '../pages/login';

export const AppRoutes = () => (
  <Routes>
    <Route path="/" element={<Login />} />


  </Routes>
);