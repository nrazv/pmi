import {
  Box,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";

type Props = {
  values: string[];
  handleChange: (event: SelectChangeEvent) => void;
  selected: string;
};

function SelectTarget({ values, handleChange, selected }: Props) {
  const menuItems = values.map((e: string, i: number) => (
    <MenuItem value={e} key={i}>
      {e}
    </MenuItem>
  ));
  return (
    <Box sx={{ minWidth: 100 }}>
      <FormControl fullWidth>
        <InputLabel>Target</InputLabel>
        <Select value={selected} label="Target" onChange={handleChange}>
          {menuItems}
        </Select>
      </FormControl>
    </Box>
  );
}

export default SelectTarget;
