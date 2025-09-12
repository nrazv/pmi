import React, { useEffect, useState } from "react";
import { Project } from "../../../models/Project";
import {
  Box,
  List,
  ListItemButton,
  ListItemIcon,
  Typography,
} from "@mui/material";
import FolderIcon from "@mui/icons-material/Folder";
import ProjectMenu from "../ProjectMenu";

export type Props = {
  projects: Project[];
  selectProject: (project: Project) => void;
};

function ProjectList({ projects, selectProject }: Props) {
  const [selectedIndex, setSelectedIndex] = React.useState(0);
  const handleListItemClick = (index: number, project: Project) => {
    setSelectedIndex(index);
    selectProject(project);
  };

  useEffect(() => {
    selectProject(projects[0]);
  }, []);

  const listItems = () => {
    const items = projects?.map((project: Project, index) => {
      return (
        <ListItemButton
          divider
          selected={selectedIndex === index}
          onClick={() => handleListItemClick(index, project)}
          key={project.name}
        >
          <ListItemIcon>
            <FolderIcon fontSize="small" />
          </ListItemIcon>
          <Typography variant="button" sx={{ color: "text.secondary" }}>
            {project.name}
          </Typography>
          <Box sx={{ marginRight: 1, marginLeft: "auto" }}>
            <ProjectMenu projectId={project.id} />
          </Box>
        </ListItemButton>
      );
    });

    return items;
  };

  return (
    <Box>
      <List component="nav">{listItems()}</List>
    </Box>
  );
}

export default ProjectList;
