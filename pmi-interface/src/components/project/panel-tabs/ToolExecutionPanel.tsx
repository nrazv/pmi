import { FormControl, SelectChangeEvent } from "@mui/material";
import SelectTarget from "../../SelectTarget";
import React from "react";
import { Project } from "../../../models/Project";
import ToolRunner from "../../ToolRunner";

type Props = {
  project: Project;
};

function ToolExecutionPanel({ project }: Props) {
  const [target, setTarget] = React.useState<string>("");
  const targets: string[] = [project.domainName, project.ipAddress];

  const handleChange = (event: SelectChangeEvent) => {
    setTarget(event.target.value as string);
  };

  return (
    <div>
      <FormControl sx={{ display: "flex", flexDirection: "row" }}>
        <SelectTarget
          handleChange={handleChange}
          values={targets}
          selected={target}
        />
        <ToolRunner />
      </FormControl>
    </div>
  );
}

export default ToolExecutionPanel;
