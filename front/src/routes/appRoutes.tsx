import { Routes, Route } from "react-router-dom";
import Login from "../pages/login";
import Dashboard from "../pages/dashboard";
import { PlansPage } from "../pages/plansPage";
import RegisterPage from "../pages/registerPage";
import DashboardAdminPage from "../pages/admin/DashboardAdminPage";
import { UserDetailPage } from "../pages/admin/userDetailPage";
import { HomePage } from "../pages/homePage";
import SuccessActionPage from "../pages/actions/successPage";

export const AppRoutes = () => (
  <Routes>
    <Route path="/login" element={<Login />} />
    <Route path="/dashboard" element={<Dashboard />} />
    <Route path="/plans" element={<PlansPage />} />
    <Route path="/register" element={<RegisterPage />} />
    <Route path="/admin/dashboard" element={<DashboardAdminPage />} />
    <Route path="/users/:id" element={<UserDetailPage />} />
    <Route path="/" element={<HomePage />} />
    <Route path="/success" element={<SuccessActionPage />} />
  </Routes>
);
