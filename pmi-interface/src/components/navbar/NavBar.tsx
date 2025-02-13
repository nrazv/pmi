import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import ProjectMenu from "../project/ProjectMenu";

function NavBar() {
  return (
    <AppBar position="sticky" sx={{ margin: 0, padding: 0 }}>
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          PMI
        </Typography>
        <ProjectMenu />
      </Toolbar>
    </AppBar>
  );
}

export default NavBar;
