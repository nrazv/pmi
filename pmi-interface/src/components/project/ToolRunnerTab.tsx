import {
  Box,
  Card,
  FormControl,
  SelectChangeEvent,
  Stack,
  styled,
  Typography,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import { ToolExecuteRequest } from "../../models/ToolExecuteRequest";
import { Project } from "../../models/Project";
import {
  executeTool,
  fetchExecutedToolsForProject,
} from "../../services/ApiService";
import CustomSelectMenu from "../SelectTarget";
import ToolRunner from "../ToolRunner";
import ToolsContainer from "../runningTools/ToolsContainer";
import { ExecutedTool } from "../../models/ExecutedTool";
import { Terminal } from "lucide-react";
import ToolSearch from "./tool/ToolSearch";
import SelectTool from "./tool/SelectTool";

type Props = {
  project: Project;
};

function ToolRunnerTab({ project }: Props) {
  const [newExecution, setNewExecution] = useState<string>("");
  const [executedTools, setExecutedTools] = useState<ExecutedTool[]>([]);
  const [executionRequest, setRequest] = useState<ToolExecuteRequest>({
    target: "",
    tool: "",
    arguments: "",
    projectName: "",
  });

  const targets: string[] = [project.domainName, project.ipAddress];

  const handleTargetChange = (event: SelectChangeEvent) => {
    setRequest({ ...executionRequest, target: event.target.value as string });
  };

  const handleToolChange = (tool: string) => {
    setRequest({ ...executionRequest, tool: tool });
  };

  const handleToolArgumentsChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRequest({
      ...executionRequest,
      arguments: event.target.value as string,
    });
  };

  const runTool = async () => {
    const response = await executeTool(executionRequest);
    setNewExecution(response);
    clearForm();
  };

  const clearForm = (): void => {
    setRequest({
      target: "",
      tool: "",
      arguments: "",
      projectName: "",
    });
  };

  useEffect(() => {
    setRequest({ ...executionRequest, projectName: project.name });
    const loadTools = async () => {
      const executedTools = await fetchExecutedToolsForProject(
        project.name ?? ""
      );
      setExecutedTools(executedTools);
    };

    loadTools();
  }, [newExecution]);

  return (
    <Box>
      <Stack direction="row" spacing={3}>
        <StyledCard>
          {Header}
          <ToolSearch />
          <SelectTool selectTool={handleToolChange} />
        </StyledCard>
        <StyledCard>Running Processes</StyledCard>
      </Stack>
    </Box>
  );
}

const StyledCard = styled(Card)(({ theme }) => ({
  padding: 20,
  flex: 1,
  borderRadius: 10,
  background: theme.palette.background.paper,
  border: `0.1rem solid ${theme.border.color.light}`,
}));

const Header = (
  <Box display="flex">
    <Terminal color="#39FF14" />
    <Typography color="primary" ml={1}>
      Tool Runner
    </Typography>
  </Box>
);

export default ToolRunnerTab;
// <FormControl
//   sx={{ display: "flex", flexDirection: "row", marginBottom: 2 }}
// >
//   <CustomSelectMenu
//     handleSelectionChange={handleTargetChange}
//     menuItems={targets}
//     selectedValue={executionRequest.target}
//     label="Target"
//   />
//   <Box>
//     <ToolRunner
//       toolArguments={executionRequest.arguments}
//       toolToExecute={executionRequest.tool}
//       handelClickButton={runTool}
//       handleToolChange={handleToolChange}
//       handleToolArgumentsChange={handleToolArgumentsChange}
//     />
//   </Box>
// </FormControl>
// {project && <ToolsContainer executedTools={executedTools} />}
