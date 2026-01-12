import React from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import ProjectPage from "./pages/ProjectPage";
import HomePage from "./pages/HomePage";
import SettingsPage from "./pages/SettingsPage";
import ModuleSettings from "./components/settigs/ModuleSettings";
import GeneralSettings from "./components/settigs/GeneralSettings";
import PackageManagerSettings from "./components/settigs/PackageManagerSettings";

function App() {
  const queryClient = new QueryClient();
  return (
    <React.Fragment>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/settings" element={<SettingsPage />}>
              <Route index element={<ModuleSettings />} />
              <Route path="general" element={<GeneralSettings />} />
              <Route path="packages" element={<PackageManagerSettings />} />
              <Route path="*" element={<ModuleSettings />} />
            </Route>
            <Route path="/project/:name" element={<ProjectPage />} />
          </Routes>
        </BrowserRouter>
      </QueryClientProvider>
    </React.Fragment>
  );
}

export default App;
