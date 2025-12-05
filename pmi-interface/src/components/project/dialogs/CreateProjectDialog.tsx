import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  styled,
  TextField,
  Divider,
} from "@mui/material";
import React, { useState } from "react";

type Props = {
  open: boolean;
  close?: () => void;
};

const apiUrl = process.env.REACT_APP_API_URL;

function CreateProjectDialog({ open, close }: Props) {
  const [name, setName] = useState<string>("");
  const [domainName, setDomainName] = useState<string>("");
  const [ipAddress, setIpAddress] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  const createProject = () => {
    const requestOptions = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Host: "localhost:8080",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({ name, domainName, ipAddress, description }),
    };

    fetch(`${apiUrl}project/new`, requestOptions).then((response) => {
      console.log(response);
      if (response.status === 201) {
        close?.();
      }
    });
  };

  return (
    <React.Fragment>
      <StyledDialog
        maxWidth="xs"
        open={open}
        onClose={close}
        aria-labelledby="newProject-dialog-title"
        aria-describedby="newProject-dialog-description"
        PaperProps={{
          component: "form",
          onSubmit: (event: React.FormEvent<HTMLFormElement>) => {
            event.preventDefault();
          },
        }}
      >
        <DialogTitle variant="subtitle1" color="primary">
          {"Create New Project"}
        </DialogTitle>
        <Divider />
        <DialogContent>
          <StyledTextField
            fullWidth
            autoFocus
            required
            margin="dense"
            id="project_name"
            name="project_name"
            label="Project name"
            type="text"
            variant="outlined"
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setName(event.target.value);
            }}
          />

          <StyledTextField
            fullWidth
            autoFocus
            required
            margin="dense"
            id="project_description"
            name="project_description"
            label="Project description"
            multiline
            rows={3}
            variant="outlined"
            sx={{ marginBottom: 3 }}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setDescription(event.target.value);
            }}
          />

          <StyledTextField
            fullWidth
            margin="dense"
            id="domain_name"
            name="domain_name"
            label="Domain name"
            type="text"
            variant="outlined"
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setDomainName(event.target.value);
            }}
          />

          <StyledTextField
            fullWidth
            margin="dense"
            id="IPAddress"
            name="IPAddress"
            label="IP Address"
            type="text"
            variant="outlined"
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setIpAddress(event.target.value);
            }}
          />
        </DialogContent>

        <DialogActions>
          <Button variant="outlined" onClick={close}>
            Cancel
          </Button>
          <Button
            variant="contained"
            onClick={() => createProject()}
            type="submit"
          >
            Create
          </Button>
        </DialogActions>
      </StyledDialog>
    </React.Fragment>
  );
}

const StyledDialog = styled(Dialog)(({ theme }) => ({
  ".MuiDialog-paper": {
    background: theme.palette.background.paper,
    transition: "box-shadow 0.3s ease",
    boxShadow: "0px 0px 20px -5px #37ff14b0",
  },
}));

const StyledTextField = styled(TextField)(({ theme }) => ({
  background: theme.palette.background.default,
  "& .MuiOutlinedInput-notchedOutline": {
    borderColor: theme.border?.color?.light ?? theme.palette.divider,
  },

  "&:hover .MuiOutlinedInput-notchedOutline": {
    borderColor: theme.border?.color?.light ?? theme.palette.divider,
  },

  "&.Mui-focused .MuiOutlinedInput-notchedOutline": {
    borderColor: theme.border?.color?.light ?? theme.palette.divider,
  },
}));

export default CreateProjectDialog;
