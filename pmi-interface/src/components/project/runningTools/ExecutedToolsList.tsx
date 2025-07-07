import ListItem from "@mui/material/ListItem";
import { Chip, List, ListItemButton, Typography } from "@mui/material";
import { fetchExecutedToolsForProject } from "../../../services/ApiService";
import { useQuery } from "@tanstack/react-query";
import { Project } from "../../../models/Project";
import React, { ReactElement } from "react";
import { ExecutedTool } from "../../../models/ExecutedTool";
import { ExecutionStatus } from "../../../models/ExecutionStatus";

type Props = {
  project: Project;
  handelSelect: (executedTool: ExecutedTool) => void;
};

function ExecutedToolsList({ project, handelSelect }: Props) {
  const { data } = useQuery({
    queryKey: ["executedTools"],
    queryFn: () => fetchExecutedToolsForProject(project.name ?? ""),
  });

  const [selectedIndex, setSelectedIndex] = React.useState(0);

  const handleListItemClick = (executedTool: ExecutedTool, index: number) => {
    setSelectedIndex(index);
    handelSelect(executedTool);
  };

  const listItemContent = (execTool: ExecutedTool): ReactElement => {
    return (
      <React.Fragment>{`  ${execTool.toolArguments}\u00A0`}</React.Fragment>
    );
  };

  const getChipColor = (status: string): "error" | "success" | "warning" => {
    switch (status) {
      case "Running":
        return "warning";
      case "Done":
        return "success";
      case "Failed":
        return "error";
      case "NotStarted":
        return "error";
      default:
        return "error";
    }
  };

  return (
    <List
      sx={{
        width: "100%",
        maxWidth: 360,
        bgcolor: "background.paper",
        maxHeight: "95%",
        overflow: "auto",
      }}
    >
      {data?.map((e, index: number) => (
        <React.Fragment key={e.id}>
          <ListItem
            divider
            alignItems="center"
            secondaryAction={
              <Chip
                size="small"
                label={e.status}
                color={getChipColor(e.status.toString())}
                sx={{ borderRadius: 1 }}
              />
            }
          >
            <ListItemButton
              selected={selectedIndex === index}
              onClick={() => handleListItemClick(e, index)}
            >
              <Typography variant="subtitle1" fontWeight={"bold"}>
                {e.name + ":\u00A0"}
              </Typography>
              {listItemContent(e)}
            </ListItemButton>
          </ListItem>
        </React.Fragment>
      ))}
    </List>
  );
}

export default ExecutedToolsList;
