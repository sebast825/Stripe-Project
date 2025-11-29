import { Routes, Route } from "react-router-dom";
import Login from "../pages/login";
import Dashboard from "../pages/dashboard";
import { PlansPage } from "../pages/plansPage";
import RegisterPage from "../pages/registerPage";

export const AppRoutes = () => (
  <Routes>
    <Route path="/" element={<Login />} />
    <Route path="/dashboard" element={<Dashboard />} />
    <Route path="/plans" element={<PlansPage />} />
    <Route path="/register" element={<RegisterPage />} />
  </Routes>
);
