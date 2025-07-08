import { Box, FormControl, SelectChangeEvent } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Project } from "../../models/Project";
import CustomSelectMenu from "../SelectTarget";
import ToolRunner from "../ToolRunner";
import RunningToolsContainer from "./runningTools/RunningToolsContainer";
import { ToolExecuteRequest } from "../../models/ToolExecuteRequest";
import useWebSocket from "react-use-websocket";

type Props = {
  project: Project;
};

const URL = process.env.REACT_APP_WS_URL ?? "";

function ToolExecutionPanel({ project }: Props) {
  const { sendJsonMessage, lastMessage } = useWebSocket(URL);

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
    sendJsonMessage(executionRequest);
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
    <Box sx={{ height: "75vh" }}>
      <FormControl
        sx={{ display: "flex", flexDirection: "row", marginBottom: 2 }}
      >
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
      <RunningToolsContainer project={project} />
    </Box>
  );
}

export default ToolExecutionPanel;
