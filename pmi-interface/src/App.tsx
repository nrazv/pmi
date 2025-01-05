import React from "react";
import "./App.css";
import HomePage from "./pages/HomePage";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Box } from "@mui/system";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import NavBar from "./components/navbar/NavBar";

function App() {
  const darkTheme = createTheme({
    palette: {
      mode: "dark",
    },
  });

  const queryClient = new QueryClient();
  return (
    <React.Fragment>
      <QueryClientProvider client={queryClient}>
        <CssBaseline />
        <NavBar />
        <Box sx={{ height: "90vh" }}>
          <HomePage />
        </Box>
      </QueryClientProvider>
    </React.Fragment>
  );
}

export default App;
