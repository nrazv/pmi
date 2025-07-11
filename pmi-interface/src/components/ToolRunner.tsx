import React, { useEffect } from "react";
import { fetchInstalledTools } from "../services/ApiService";
import {
  Button,
  FormControl,
  SelectChangeEvent,
  TextField,
} from "@mui/material";
import CustomSelectMenu from "./SelectTarget";

type Props = {
  handleToolChange: (event: SelectChangeEvent) => void;
  handleToolArgumentsChange: (
    event: React.ChangeEvent<HTMLInputElement>
  ) => void;
  handelClickButton: () => void;
  toolToExecute: string;
  toolArguments: string;
};

function ToolRunner({
  toolToExecute,
  toolArguments,
  handleToolChange,
  handelClickButton,
  handleToolArgumentsChange,
}: Props) {
  const [installedTools, setInstalledTools] = React.useState<string[]>([]);

  useEffect(() => {
    fetchInstalledTools().then((data) => {
      const tools = data.map((e) => e.name);
      setInstalledTools(tools);
    });
  }, []);

  return (
    <FormControl sx={{ marginLeft: 2, display: "flex", flexDirection: "row" }}>
      <CustomSelectMenu
        handleSelectionChange={handleToolChange}
        menuItems={installedTools}
        selectedValue={toolToExecute}
        label="Tool"
      />
      <TextField
        sx={{ marginLeft: 2, minWidth: 400 }}
        variant="outlined"
        size="small"
        value={toolArguments}
        required
        onChange={handleToolArgumentsChange}
      />
      <Button
        sx={{ marginLeft: 2, minWidth: 100 }}
        variant="contained"
        onClick={handelClickButton}
        disabled={toolArguments == "" && toolToExecute == ""}
      >
        Run
      </Button>
    </FormControl>
  );
}

export default ToolRunner;
