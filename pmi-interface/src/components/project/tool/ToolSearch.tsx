import {
  Box,
  FormControl,
  OutlinedInput,
  styled,
  Typography,
} from "@mui/material";

const searchPlaceHolder = "Search for a tool...";

const ToolSearch = () => {
  return (
    <Box mt={2}>
      <Typography variant="subtitle2" color="secondary">
        Search Tool
      </Typography>
      <FormControl variant="outlined" fullWidth>
        <StyledOutlinedInput
          size="small"
          required
          placeholder={searchPlaceHolder}
        />
      </FormControl>
    </Box>
  );
};

const StyledOutlinedInput = styled(OutlinedInput)(({ theme }) => ({
  background: theme.palette.background.default,
  borderRadius: 0,
  marginTop: 10,
  padding: 0,
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

export default ToolSearch;
