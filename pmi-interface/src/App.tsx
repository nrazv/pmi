import React from "react";
import "./App.css";
import NavBar from "./components/NavBar";
import HomePage from "./pages/HomePage";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Box } from "@mui/system";
import { Paper } from "@mui/material";

function App() {
  const queryClient = new QueryClient();
  return (
    <React.Fragment>
      <QueryClientProvider client={queryClient}>
        <NavBar />
        <Box sx={{ marginTop: "64px", height: "90vh" }}>
          <HomePage />
        </Box>
      </QueryClientProvider>
    </React.Fragment>
  );
}

export default App;
