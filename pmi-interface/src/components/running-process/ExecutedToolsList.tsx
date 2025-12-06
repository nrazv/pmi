import ListItem from "@mui/material/ListItem";
import {
  CircularProgress,
  IconButton,
  List,
  ListItemButton,
  Typography,
} from "@mui/material";
import React, { ReactElement, useState } from "react";
import { ExecutedTool } from "../../models/ExecutedTool";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import DangerousIcon from "@mui/icons-material/Dangerous";

type Props = {
  executedTools: ExecutedTool[];
  handelSelect: (executedTool: ExecutedTool) => void;
};

function ExecutedToolsList({ executedTools, handelSelect }: Props) {
  const [selectedIndex, setSelectedIndex] = useState<number>();

  const handleListItemClick = (executedTool: ExecutedTool, index: number) => {
    setSelectedIndex(index);
    handelSelect(executedTool);
  };

  const listItemContent = (execTool: ExecutedTool): ReactElement => {
    return (
      <Typography
        noWrap
        variant="body2"
      >{`  ${execTool.toolArguments}\u00A0`}</Typography>
    );
  };

  const getExecutionStatus: Record<string, ReactElement> = {
    Running: (
      <IconButton edge="end">
        <CircularProgress size="24px" color="warning" />
      </IconButton>
    ),
    Done: (
      <IconButton edge="end">
        <CheckCircleIcon color="success" />
      </IconButton>
    ),
    Failed: (
      <IconButton edge="end">
        <DangerousIcon color="error" />
      </IconButton>
    ),
  };

  return (
    <List
      dense
      sx={{
        width: "100%",
        maxWidth: 360,
        bgcolor: "background.paper",
        maxHeight: "99%",
        overflow: "auto",
      }}
    >
      {executedTools?.map((e, index: number) => (
        <React.Fragment key={e.id}>
          <ListItem
            divider
            alignItems="center"
            secondaryAction={getExecutionStatus[e.status]}
          >
            <ListItemButton
              selected={selectedIndex === index}
              onClick={() => handleListItemClick(e, index)}
            >
              <Typography variant="subtitle2" fontWeight={"bold"}>
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
