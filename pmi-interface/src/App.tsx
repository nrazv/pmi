import React from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Box } from "@mui/system";
import { createTheme } from "@mui/material/styles";
import NavBar from "./components/navbar/NavBar";
import HomePage from "./pages/Home/HomePage";

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
        <NavBar />
        <Box
          sx={{
            flex: 1,
          }}
        >
          <HomePage />
        </Box>
      </QueryClientProvider>
    </React.Fragment>
  );
}

export default App;
