import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { createTheme, CssBaseline } from "@mui/material";
import { ThemeProvider } from "@emotion/react";
import AppTheme from "./utils/AppTheme";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

root.render(
  <React.StrictMode>
    <ThemeProvider theme={AppTheme}>
      <CssBaseline /> {/* resets and applies dark background */}
      <App />
    </ThemeProvider>
  </React.StrictMode>
);

reportWebVitals();
