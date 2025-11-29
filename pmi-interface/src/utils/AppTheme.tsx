import { createTheme } from "@mui/material";

const AppTheme = createTheme({
  border: {
    color: {
      light: "#2a2a2a",
    },
  },
  palette: {
    mode: "dark",
    primary: {
      main: "#39FF14", // neon green
      contrastText: "#0A0A0A",
    },

    secondary: {
      main: "#14F8FF", // cyan
      contrastText: "#0A0A0A",
    },

    error: {
      main: "#FF004F", // neon red
    },

    background: {
      default: "#0A0A0A", // primary background
      paper: "#1F1F1F", // secondary surfaces
    },

    text: {
      primary: "#FFFFFF",
      secondary: "#B3B3B3",
    },

    divider: "#2A2A2A",
  },

  typography: {
    fontFamily: "'Roboto Mono', monospace",
  },
  cssVariables: true,
});

export default AppTheme;
