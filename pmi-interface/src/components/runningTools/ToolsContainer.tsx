import { Box, Typography } from "@mui/material";
import ExecutedToolsList from "./ExecutedToolsList";
import { useEffect, useState } from "react";
import ExecutedToolResult from "./ExecutedToolResult";
import { ExecutedTool } from "../../models/ExecutedTool";
import { fetchExecutedToolsForProject } from "../../services/ApiService";
import { Project } from "../../models/Project";

const containerStyle = {
  display: "flex",
  height: "100%",
};

const runningToolStyle = {
  width: "20%",
};

const runningToolResultStyle = {
  flex: 1,
  overflow: "auto",
  maxWidth: 1500,
};

type Props = {
  project?: Project;
  executedTools: ExecutedTool[];
};

function ToolsContainer({ executedTools }: Props) {
  const [executionResult, setExecutionResult] = useState<string>();

  const handelExecutedToolSelect = (executedTool: ExecutedTool) => {
    setExecutionResult(executedTool.executionResult);
  };

  if (executedTools.length <= 0) {
    return (
      <Typography variant="caption" color="textDisabled">
        Nothing executed on this project
      </Typography>
    );
  }

  return (
    <Box sx={containerStyle}>
      <Box sx={runningToolStyle}>
        <ExecutedToolsList
          handelSelect={handelExecutedToolSelect}
          executedTools={executedTools}
        />
      </Box>
      <Box sx={runningToolResultStyle}>
        {executionResult && <ExecutedToolResult toolOutput={executionResult} />}
      </Box>
    </Box>
  );
}

export default ToolsContainer;
