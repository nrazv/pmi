import { IconButton, Toolbar, Typography } from "@mui/material";
import { grey } from "@mui/material/colors";
import LocationSearchingIcon from "@mui/icons-material/LocationSearching";

function TargetBar() {
  return (
    <Toolbar variant="dense">
      <IconButton
        edge="start"
        aria-label="menu"
        sx={{ mr: 2, color: grey[900] }}
      >
        <LocationSearchingIcon />
      </IconButton>
    </Toolbar>
  );
}

export default TargetBar;
