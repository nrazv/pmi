import {
  Box,
  FormControl,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import React from "react";
import { Module } from "../models/Modules";

type Props = {
  modules: Module[];
  moduleChange: (module: string) => void;
};

export const ModuleSelect = ({ modules, moduleChange }: Props) => {
  const [value, setValue] = React.useState<string>("");

  const handleChange = (event: SelectChangeEvent) => {
    setValue(event.target.value);
    moduleChange(event.target.value);
  };

  return (
    <Box>
      <FormControl fullWidth size="small">
        <Select
          sx={{ backgroundColor: "#0A0A0A" }}
          value={value}
          onChange={handleChange}
          displayEmpty
          renderValue={(selected) => {
            if (selected === "") {
              return <em>Choose a module...</em>;
            }
            return selected;
          }}
        >
          <MenuItem value="" disabled>
            <em>Choose a module...</em>
          </MenuItem>
          {modules.map((e: Module, i) => (
            <MenuItem value={e.name} key={i}>
              {e.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Box>
  );
};
