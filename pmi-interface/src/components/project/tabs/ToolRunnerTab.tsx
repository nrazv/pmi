import {
  Box,
  Button,
  Card,
  FormControl,
  OutlinedInput,
  Stack,
  styled,
  Typography,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import { ToolExecuteRequest } from "../../../models/ToolExecuteRequest";
import { Project } from "../../../models/Project";
import {
  executeTool,
  fetchExecutedToolsForProject,
} from "../../../services/ApiService";
import SelectTarget from "../../SelectTarget";
import { ExecutedTool } from "../../../models/ExecutedTool";
import { Terminal } from "lucide-react";
import ToolSearch from "../tool/ToolSearch";
import SelectTool from "../tool/SelectTool";
import RunningProcessContainer from "../../running-process/RunningProcessContainer";

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
    projectName: project.name,
  });

  const targets: string[] = [project.domainName, project.ipAddress];

  const handleTargetChange = (target: string): void => {
    setRequest({ ...executionRequest, target: target });
  };

  const handleToolChange = (tool: string) => {
    setRequest({ ...executionRequest, tool: tool });
  };

  const handleToolArgumentsChange = (
    event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>
  ) => {
    setRequest({
      ...executionRequest,
      arguments: event.target.value as string,
    });
  };

  const isDisabled =
    !executionRequest.target?.trim() ||
    !executionRequest.tool?.trim() ||
    !executionRequest.projectName?.trim();

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
    // setRequest({ ...executionRequest, projectName: project.name });
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
          <SelectTool
            selectTool={handleToolChange}
            selectedTool={executionRequest.tool}
          />
          <SelectTarget
            selectTarget={handleTargetChange}
            selectedTarget={executionRequest.target}
            project={project}
          />
          <Typography variant="subtitle2" color="secondary" mt={3}>
            Tool Arguments
          </Typography>
          <FormControl variant="outlined" fullWidth>
            <StyledOutlinedInput
              size="small"
              required
              placeholder="e.g. tool arguments..."
              value={executionRequest.arguments}
              onChange={(e) => handleToolArgumentsChange(e)}
            />
          </FormControl>
          <Button
            fullWidth
            variant="contained"
            sx={{ marginTop: 3 }}
            onClick={runTool}
            disabled={isDisabled}
          >
            Run Tool
          </Button>
        </StyledCard>
        <RunningProcessContainer executedTools={executedTools} />
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

export default ToolRunnerTab;
