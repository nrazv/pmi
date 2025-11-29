import "@mui/material/styles";

declare module "@mui/material/styles" {
  interface Theme {
    border: {
      color: {
        light: string;
      };
    };
  }

  interface ThemeOptions {
    border?: {
      color?: {
        light?: string;
      };
    };
  }
}
