import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import React from "react";

type Props = {
  open: boolean;
  handleOnClose?: () => void;
  title: string;
  textContent: string;
  confirmAction: () => void;
  cancelAction: () => void;
};

function ConfirmationDialog({
  open,
  handleOnClose,
  title,
  textContent,
  confirmAction,
  cancelAction,
}: Props) {
  return (
    <Dialog open={open} onClose={handleOnClose}>
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <DialogContentText>{textContent}</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={cancelAction}>No</Button>
        <Button onClick={confirmAction} autoFocus>
          Yes
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default ConfirmationDialog;
