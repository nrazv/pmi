import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import React from "react";
import { Box, Button, styled } from "@mui/material";
import SettingsIcon from "@mui/icons-material/Settings";
import NewProject from "./NewProject";
import { useNavigate } from "react-router-dom";

function NavBar() {
  const navigate = useNavigate();

  return (
    <StyledAppBar position="sticky">
      <Toolbar sx={toolbarStyles}>
        <img src="/icons/icon-192.png" alt="icon" style={iconStyle} />
        <Box marginLeft={2}>
          <Typography
            color="primary"
            variant="subtitle1"
            sx={{ textTransform: "uppercase" }}
          >
            Pentest Manager Interface
          </Typography>
          <Typography color="textDisabled" variant="body2">
            Security Testing & Project Management
          </Typography>
        </Box>
        <Button
          sx={{
            marginLeft: "auto",
            marginRight: 2,
            textTransform: "capitalize",
            fontSize: 15,
            borderColor: "#353535ff",
          }}
          variant="outlined"
          startIcon={<SettingsIcon />}
          onClick={() => navigate("/settings")}
        >
          Settings
        </Button>
        <NewProject />
      </Toolbar>
    </StyledAppBar>
  );
}

const StyledAppBar = styled(AppBar)(({ theme }) => ({
  margin: 0,
  padding: 15,
  alignItems: "center",
  backgroundColor: "#0A0A0A",
}));

const toolbarStyles: React.CSSProperties = {
  width: "70%",
};

const iconStyle: React.CSSProperties = {
  height: "50px",
  borderRadius: 6,
  border: "0.1rem solid #39FF14",
};

export default NavBar;
