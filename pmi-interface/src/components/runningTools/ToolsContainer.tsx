import { Box } from "@mui/material";
import ExecutedToolsList from "./ExecutedToolsList";
import { useState } from "react";
import ExecutedToolResult from "./ExecutedToolResult";
import { Project } from "../../models/Project";
import { ExecutedTool } from "../../models/ExecutedTool";

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

function ToolsContainer({ project }: Props) {
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
        {executionResult && (
          <ExecutedToolResult executedTool={executionResult} />
        )}
      </Box>
    </Box>
  );
}

export default ToolsContainer;
