import { Box } from "@mui/material";

export interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function CustomTabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <Box p={0} role="tabpanel" hidden={value !== index} {...other}>
      {value === index && <Box mt={3}>{children}</Box>}
    </Box>
  );
}

export default CustomTabPanel;
