import { FormControl, SelectChangeEvent } from "@mui/material";
import React, { useEffect, useState } from "react";
import useWebSocket from "react-use-websocket";
import { Project } from "../../models/Project";
import CustomSelectMenu from "../SelectTarget";
import ToolRunner from "../ToolRunner";
import RunningTool from "../RunningTool";

type Props = {
  project: Project;
};

type ToolExecuteRequest = {
  target: string;
  tool: string;
  arguments: string;
};

const URL = "ws://localhost:8080/ws";

function ToolExecutionPanel({ project }: Props) {
  const { sendJsonMessage, lastMessage } = useWebSocket(URL);

  const [executionRequest, setRequest] = useState<ToolExecuteRequest>({
    target: "",
    tool: "",
    arguments: "",
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
    });
  };

  return (
    <div>
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
      <RunningTool lastMessage={lastMessage} />
    </div>
  );
}

export default ToolExecutionPanel;
