import React from "react";
import { Project } from "../../shared/Project";
import {
  Box,
  List,
  ListItemButton,
  ListItemIcon,
  Typography,
} from "@mui/material";
import FolderIcon from "@mui/icons-material/Folder";

export type Props = {
  projects: Project[];
};

function ProjectList(props: Props) {
  const [selectedIndex, setSelectedIndex] = React.useState(1);
  const handleListItemClick = (
    event: React.MouseEvent<HTMLDivElement, MouseEvent>,
    index: number
  ) => {
    setSelectedIndex(index);
  };

  const listItems = () => {
    const items = props.projects.map((project: Project, index) => (
      <ListItemButton
        selected={selectedIndex === index}
        onClick={(event) => handleListItemClick(event, index)}
        sx={{ margin: 1, padding: 1 }}
        key={project.name}
      >
        <ListItemIcon>
          <FolderIcon fontSize="small" />
        </ListItemIcon>
        <Typography variant="body2" sx={{ color: "text.secondary" }}>
          {project.name}
        </Typography>
      </ListItemButton>
    ));

    return items;
  };

  return (
    <Box className="projects-box">
      <List component="nav">{listItems()}</List>
    </Box>
  );
}

export default ProjectList;
