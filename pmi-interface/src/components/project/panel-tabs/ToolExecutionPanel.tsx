import { FormControl, SelectChangeEvent } from "@mui/material";
import CustomSelectMenu from "../../SelectTarget";
import React from "react";
import { Project } from "../../../models/Project";
import ToolRunner from "../../ToolRunner";

type Props = {
  project: Project;
};

function ToolExecutionPanel({ project }: Props) {
  const [target, setTarget] = React.useState<string>("");
  const [toolToExecute, setToolToExecute] = React.useState<string>("");
  const [toolArguments, setToolArguments] = React.useState<string>("");

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
    clearForm();
  };

  const clearForm = (): void => {
    setTarget("");
    setToolToExecute("");
    setToolArguments("");
  };

  const targets: string[] = [project.domainName, project.ipAddress];

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
    </div>
  );
}

export default ToolExecutionPanel;
