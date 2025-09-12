import { IconButton, Menu, MenuItem } from "@mui/material";
import React, { useState } from "react";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import ConfirmationDialog from "../ConfirmationDialog";

type Props = {
  projectId: string;
};

function ProjectMenu({ projectId }: Props) {
  const [openDeleteDialog, setOpenDeleteDialog] = useState<boolean>(false);
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const handleClose = () => setAnchorEl(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) =>
    setAnchorEl(event.currentTarget);

  const MenuItems = (
    <MenuItem onClick={() => setOpenDeleteDialog(true)}>Delete</MenuItem>
  );

  const deleteProjectById = async (id: string) => {
    const requestOptions = {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Host: "localhost:8080",
        "Access-Control-Allow-Origin": "*",
      },
    };

    try {
      const response = await fetch(
        `http://localhost:8080/api/project/${id}`,
        requestOptions
      );
      console.log(response);

      if (response.status === 200 || response.status === 204) {
        console.log("Project deleted successfully");
      } else {
        console.error("Failed to delete project:", response.status);
      }
    } catch (error) {
      console.error("Error deleting project:", error);
    }

    setOpenDeleteDialog(false);
  };

  return (
    <>
      <IconButton
        aria-label="more"
        id="long-button"
        aria-controls={open ? "long-menu" : undefined}
        aria-expanded={open ? "true" : undefined}
        aria-haspopup="true"
        onClick={handleClick}
      >
        <MoreVertIcon />
      </IconButton>
      <Menu
        id="long-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
      >
        {MenuItems}
      </Menu>
      <ConfirmationDialog
        open={openDeleteDialog}
        confirmAction={() => deleteProjectById(projectId)}
        cancelAction={() => setOpenDeleteDialog(false)}
        title="Delete project"
        textContent="Are you sure you want to delete this item?"
      />
    </>
  );
}

export default ProjectMenu;
