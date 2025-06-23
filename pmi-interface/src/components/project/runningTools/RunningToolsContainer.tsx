import { Box } from "@mui/material";
import ExecutedToolsList from "./ExecutedToolsList";

const containerStyle = {
  display: "flex",
  height: "100%",
};

const runningToolStyle = {
  width: "20%",
};

const runningToolResultStyle = {
  flex: 1,
};

function RunningToolsContainer() {
  return (
    <Box sx={containerStyle}>
      <Box sx={runningToolStyle}>
        <ExecutedToolsList />
      </Box>
      <Box sx={runningToolResultStyle}>B</Box>
    </Box>
  );
}

export default RunningToolsContainer;
