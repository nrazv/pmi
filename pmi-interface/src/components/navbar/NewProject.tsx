import { Button } from "@mui/material";
import React from "react";
import CreateProjectDialog from "../dialogs/CreateProjectDialog";
import AddIcon from "@mui/icons-material/Add";

function NewProject() {
  const [openDialog, setOpenDialog] = React.useState<boolean>(false);
  const handelCloseDialog = () => setOpenDialog(false);

  return (
    <>
      <Button
        onClick={() => setOpenDialog(true)}
        variant="contained"
        startIcon={<AddIcon />}
        sx={{ marginLeft: "auto", marginRight: 1, fontSize: 15 }}
      >
        New Project
      </Button>

      <CreateProjectDialog open={openDialog} close={handelCloseDialog} />
    </>
  );
}

export default NewProject;
