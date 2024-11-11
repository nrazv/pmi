import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
} from "@mui/material";
import React, { useState } from "react";

type Props = {
  open: boolean;
  close?: () => void;
};

const apiUrl = process.env.REACT_APP_API_URL;

function NewProjectDialog({ open, close }: Props) {
  const [name, setName] = useState<string>("");

  const createProject = () => {
    const requestOptions = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Host: "localhost:8080",
        "Access-Control-Allow-Origin": "*",
      },
    };

    fetch(`${apiUrl}project/new?projectName=${name}`, requestOptions).then(
      (response) => {
        if (response.status == 200) {
          close?.();
        }
      }
    );
  };

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
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setName(event.target.value);
            }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={close}>Cancel</Button>
          <Button onClick={() => createProject()} type="submit">
            Create
          </Button>
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
}

export default NewProjectDialog;
