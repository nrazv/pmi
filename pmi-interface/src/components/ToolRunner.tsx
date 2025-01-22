import React, { useEffect, useState } from "react";
import { InstalledTool } from "../models/InstalledTool";
import { fetchInstalledTools } from "../services/ApiService";
import {
  Button,
  FormControl,
  SelectChangeEvent,
  TextField,
} from "@mui/material";
import SelectTarget from "./SelectTarget";

function ToolRunner() {
  const [installedTools, setInstalledTools] = useState<string[]>([]);
  const [target, setTarget] = React.useState<string>("");
  const handleChange = (event: SelectChangeEvent) => {
    setTarget(event.target.value as string);
  };

  useEffect(() => {
    fetchInstalledTools().then((data) => {
      const tools = data.map((e) => e.name);
      setInstalledTools(tools);
    });
  }, []);

  return (
    <FormControl sx={{ marginLeft: 2, display: "flex", flexDirection: "row" }}>
      <SelectTarget
        handleChange={handleChange}
        values={installedTools}
        selected={target}
        label="Tool"
      />
      <TextField
        sx={{ marginLeft: 2, minWidth: 400 }}
        label="Commands"
        variant="outlined"
      />
      <Button sx={{ marginLeft: 2, minWidth: 100 }} variant="contained">
        Run
      </Button>
    </FormControl>
  );
}

export default ToolRunner;
