import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
} from "@mui/material";
import React from "react";

type Props = {
  open: boolean;
  close?: () => void;
};

function NewProjectDialog({ open, close }: Props) {
  return (
    <React.Fragment>
      <Dialog
        open={open}
        onClose={close}
        PaperProps={{
          component: "form",
          onSubmit: (event: React.FormEvent<HTMLFormElement>) => {
            event.preventDefault();
            const formData = new FormData(event.currentTarget);
            console.log(formData.get("project_name"));
          },
        }}
        aria-labelledby="newProject-dialog-title"
        aria-describedby="newProject-dialog-description"
      >
        <DialogTitle id="newProject-dialog-title">{"Project name"}</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            required
            margin="dense"
            id="project_name"
            name="project_name"
            label="Project name"
            type="text"
            fullWidth
            variant="standard"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={close}>Cancel</Button>
          <Button type="submit">Create</Button>
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
}

export default NewProjectDialog;
