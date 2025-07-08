import { Box } from "@mui/material";
import ExecutedToolsList from "./ExecutedToolsList";
import { Project } from "../../../models/Project";
import { useState } from "react";
import { ExecutedTool } from "../../../models/ExecutedTool";
import { BorderColor } from "@mui/icons-material";

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
  height: 950,
};

type Props = {
  project: Project;
};

function RunningToolsContainer({ project }: Props) {
  const [executionResult, setExecutionResult] = useState<ExecutedTool>();

  const handelExecutedToolSelect = (executedTool: ExecutedTool) => {
    setExecutionResult(executedTool);
  };

  return (
    <Box sx={containerStyle}>
      <Box sx={runningToolStyle}>
        <ExecutedToolsList
          handelSelect={handelExecutedToolSelect}
          project={project}
        />
      </Box>
      <Box sx={runningToolResultStyle}>
        <pre>{executionResult?.executionResult}</pre>
      </Box>
    </Box>
  );
}

export default RunningToolsContainer;
