import React, { useEffect } from "react";
import { Project } from "../../models/Project";
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
    const items = projects?.map((project: Project, index) => (
      <ListItemButton
        divider
        selected={selectedIndex === index}
        onClick={() => handleListItemClick(index, project)}
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
