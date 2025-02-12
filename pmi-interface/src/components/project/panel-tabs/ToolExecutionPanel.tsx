import { FormControl, SelectChangeEvent } from "@mui/material";
import CustomSelectMenu from "../../SelectTarget";
import React, { useEffect } from "react";
import { Project } from "../../../models/Project";
import ToolRunner from "../../ToolRunner";
import useWebSocket from "react-use-websocket";

type Props = {
  project: Project;
};

type ToolExecuteRequest = {
  target: string;
  tool: string;
  arguments: string | null;
};

const URL = "ws://localhost:8080/ws";

function ToolExecutionPanel({ project }: Props) {
  const { sendJsonMessage, lastMessage } = useWebSocket(URL);

  const [response, setResponse] = React.useState<string>("");
  const [target, setTarget] = React.useState<string>("");
  const [toolToExecute, setToolToExecute] = React.useState<string>("");
  const [toolArguments, setToolArguments] = React.useState<string>("");

  const targets: string[] = [project.domainName, project.ipAddress];

  const handleTargetChange = (event: SelectChangeEvent) => {
    setTarget(event.target.value as string);
  };

  const handleToolChange = (event: SelectChangeEvent) => {
    setToolToExecute(event.target.value as string);
  };

  const handleToolArgumentsChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setToolArguments(event.target.value);
  };

  const runTool = () => {
    sendToolExecution();
    clearForm();
  };

  const clearForm = (): void => {
    setTarget("");
    setToolToExecute("");
    setToolArguments("");
  };

  const sendToolExecution = () => {
    const request: ToolExecuteRequest = {
      target: target,
      tool: toolToExecute,
      arguments: toolArguments,
    };

    sendJsonMessage(request);
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
          selectedValue={target}
          label="Target"
        />
        <ToolRunner
          toolArguments={toolArguments}
          toolToExecute={toolToExecute}
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
