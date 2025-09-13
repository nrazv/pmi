import { Box, FormControl, SelectChangeEvent } from "@mui/material";
import React, { useEffect, useState } from "react";
import { ToolExecuteRequest } from "../../models/ToolExecuteRequest";
import { Project } from "../../models/Project";
import { executeTool } from "../../services/ApiService";
import CustomSelectMenu from "../SelectTarget";
import ToolRunner from "../ToolRunner";
import ToolsContainer from "../runningTools/ToolsContainer";

type Props = {
  project: Project;
};

function ExecutionManagerTab({ project }: Props) {
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

  const runTool = () => {
    executeTool(executionRequest);
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
  }, []);

  return (
    <Box>
      <FormControl sx={{ display: "flex", flexDirection: "row" }}>
        <CustomSelectMenu
          handleSelectionChange={handleTargetChange}
          menuItems={targets}
          selectedValue={executionRequest.target}
          label="Target"
        />
        <ToolRunner
          toolArguments={executionRequest.arguments}
          toolToExecute={executionRequest.tool}
          handelClickButton={runTool}
          handleToolChange={handleToolChange}
          handleToolArgumentsChange={handleToolArgumentsChange}
        />
      </FormControl>
      <ToolsContainer project={project} />
    </Box>
  );
}

export default ExecutionManagerTab;
