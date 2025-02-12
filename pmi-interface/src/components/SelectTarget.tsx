import {
  Box,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";

type Props = {
  menuItems: string[];
  handleSelectionChange: (event: SelectChangeEvent) => void;
  selectedValue: string;
  label?: string;
};

function CustomSelectMenu({
  menuItems,
  handleSelectionChange,
  selectedValue,
  label,
}: Props) {
  const menuItemList = menuItems.map((e: string, i: number) => (
    <MenuItem value={e} key={i}>
      {e}
    </MenuItem>
  ));

  return (
    <Box sx={{ minWidth: 100 }}>
      <FormControl fullWidth size="small">
        <InputLabel>{label ?? "None Label"}</InputLabel>
        <Select
          value={selectedValue}
          label={label}
          onChange={handleSelectionChange}
        >
          {menuItemList}
        </Select>
      </FormControl>
    </Box>
  );
}

export default CustomSelectMenu;
