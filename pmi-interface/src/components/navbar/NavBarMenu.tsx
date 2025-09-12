import { IconButton, Menu, MenuItem, Typography } from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import React from "react";
import CreateProjectDialog from "../project/dialogs/CreateProjectDialog";

function NavBarMenu() {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [openDialog, setOpenDialog] = React.useState<boolean>(false);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => setAnchorEl(null);
  const handelOpenDialog = () => setOpenDialog(true);
  const handelCloseDialog = () => setOpenDialog(false);

  return (
    <div>
      <IconButton onClick={handleClick}>
        <MoreVertIcon />
      </IconButton>
      <Menu
        id="project-menu"
        anchorEl={anchorEl}
        onClose={handleClose}
        open={open}
      >
        <MenuItem onClick={handelOpenDialog}>
          <Typography variant="caption" fontWeight={600}>
            New Project
          </Typography>
        </MenuItem>
      </Menu>
      <CreateProjectDialog open={openDialog} close={handelCloseDialog} />
    </div>
  );
}

export default NavBarMenu;
