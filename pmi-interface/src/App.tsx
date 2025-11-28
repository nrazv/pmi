import React from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import HomePage from "./pages/Home/HomePage";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import ProjectPage from "./pages/ProjectPage";

function App() {
  const queryClient = new QueryClient();
  return (
    <React.Fragment>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/project/:name" element={<ProjectPage />} />
          </Routes>
        </BrowserRouter>
      </QueryClientProvider>
    </React.Fragment>
  );
}

export default App;
