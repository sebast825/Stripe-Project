import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { QueryClientProvider } from "@tanstack/react-query";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { queryClient } from "./states/queryClient.ts";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <React.StrictMode>
      <QueryClientProvider client={queryClient}>
            <BrowserRouter>
        <App />
        </BrowserRouter>
      </QueryClientProvider>
    </React.StrictMode>
  </StrictMode>
);
