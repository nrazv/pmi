import { FormControl, SelectChangeEvent } from "@mui/material";
import React, { useEffect, useState } from "react";
import useWebSocket from "react-use-websocket";
import { Project } from "../../models/Project";
import CustomSelectMenu from "../SelectTarget";
import ToolRunner from "../ToolRunner";

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

  const [response, setResponse] = React.useState<string>("");
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
    sendToolExecution();
    clearForm();
  };

  const clearForm = (): void => {
    setRequest({
      target: "",
      tool: "",
      arguments: "",
    });
  };

  const sendToolExecution = () => {
    sendJsonMessage(executionRequest);
  };

  useEffect(() => {
    if (lastMessage?.data) {
      setResponse((prev) => prev + "\n" + lastMessage?.data + "\n");
    }
  }, [lastMessage?.data]);

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
      <pre>{response}</pre>
    </div>
  );
}

export default ToolExecutionPanel;
