import { Box, FormControl, SelectChangeEvent } from "@mui/material";
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
import { Await } from "react-router-dom";

type Props = {
  project: Project;
};

function ExecutionManagerTab({ project }: Props) {
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

  const handleToolChange = (event: SelectChangeEvent) => {
    setRequest({ ...executionRequest, tool: event.target.value as string });
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
      <FormControl
        sx={{ display: "flex", flexDirection: "row", marginBottom: 2 }}
      >
        <CustomSelectMenu
          handleSelectionChange={handleTargetChange}
          menuItems={targets}
          selectedValue={executionRequest.target}
          label="Target"
        />
        <Box>
          <ToolRunner
            toolArguments={executionRequest.arguments}
            toolToExecute={executionRequest.tool}
            handelClickButton={runTool}
            handleToolChange={handleToolChange}
            handleToolArgumentsChange={handleToolArgumentsChange}
          />
        </Box>
      </FormControl>
      {project && <ToolsContainer executedTools={executedTools} />}
    </Box>
  );
}

export default ExecutionManagerTab;
