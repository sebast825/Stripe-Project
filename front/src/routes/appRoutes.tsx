import { Routes, Route } from 'react-router-dom';
import Login from '../pages/login';
import Dashboard from '../pages/dashboard';

export const AppRoutes = () => (
  <Routes>
    <Route path="/" element={<Login />} />
    <Route path="/dashboard" element={<Dashboard />} />


  </Routes>
);