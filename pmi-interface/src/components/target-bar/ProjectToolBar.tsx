import { IconButton, Toolbar, Typography } from "@mui/material";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import { grey } from "@mui/material/colors";
import LocationSearchingIcon from "@mui/icons-material/LocationSearching";

function ProjectToolBar() {
  return (
    <Toolbar variant="dense">
      <Typography variant="h6" component="div" color="black">
        Project 5
      </Typography>

      <IconButton
        edge="start"
        sx={{ marginRight: 0, marginLeft: "auto", padding: 0 }}
      >
        <DeleteForeverIcon color="error" fontSize="large" />
      </IconButton>
    </Toolbar>
  );
}

export default ProjectToolBar;
