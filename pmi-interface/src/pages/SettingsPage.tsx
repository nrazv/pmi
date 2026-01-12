import { Button, styled, Typography, Box } from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import SettingsIcon from "@mui/icons-material/Settings";
import { Outlet, useNavigate } from "react-router-dom";
import React, { useState } from "react";

type ButtonLinks = {
  path: string;
  text: string;
};

const settings: ButtonLinks[] = [
  { path: "settings", text: "Module Manager" },
  { path: "general", text: "General Settings" },
  { path: "packages", text: "Package Manager" },
];

const SettingsPage = () => {
  const [view, setView] = useState("a");
  const navigate = useNavigate();

  return (
    <Box paddingX={3}>
      <Box
        sx={{
          marginTop: 3,
        }}
      >
        <NavLink
          startIcon={<ArrowBackIcon />}
          disableTouchRipple
          variant="text"
          onClick={() => navigate("/")}
        >
          Back To Projects
        </NavLink>
      </Box>

      <Box display="flex" sx={{ alignItems: "center" }}>
        <SettingsIcon color="primary" sx={{ marginRight: 1, fontSize: 30 }} />
        <Typography variant="body1" color="primary" fontSize={18}>
          System Settings
        </Typography>
      </Box>
      <Box mt={4}>
        {settings.map((e) => (
          <Button
            variant="outlined"
            sx={{ margin: 1, textTransform: "capitalize" }}
            onClick={() => navigate(e.path)}
          >
            {e.text}
          </Button>
        ))}
      </Box>
      <Box>
        <Outlet />
      </Box>
    </Box>
  );
};

const NavLink = styled(Button)(({ theme }) => ({
  transition: "all 0.2s ease",
  color: theme.palette.text.secondary,
  background: "transparent",
  fontSize: 16,
  textTransform: "capitalize",
  "&:hover": {
    cursor: "pointer",
    color: theme.palette.primary.main,
  },
  marginBottom: 10,
}));

export default SettingsPage;
