import {
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  ListSubheader,
} from "@mui/material";
import { useEffect, useState } from "react";
import { Project } from "../shared/Project";

const apiUrl = process.env.REACT_APP_API_URL;

function HomePage() {
  const [projects, setProjects] = useState<Project[]>();

  const fetchProjects = () => {
    const requestOptions = {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
      },
    };

    fetch(`${apiUrl}project/all`, requestOptions)
      .then((response) => response.json())
      .then((response) => {
        const projects = response as Project[];
        setProjects(projects);
      });
  };

  const listItems = () => {
    const items = projects?.map((project: Project) => (
      <ListItem disablePadding key={project.name}>
        <ListItemButton>
          <ListItemText primary={project.name} />
        </ListItemButton>
      </ListItem>
    ));
    return items;
  };

  useEffect(() => fetchProjects(), []);

  return (
    <Box sx={{ width: "100%" }}>
      <List
        color="secondary"
        sx={{ width: "30%", maxWidth: 200 }}
        component="nav"
        subheader={
          <ListSubheader component="div" id="nested-list-subheader">
            Projects
          </ListSubheader>
        }
      >
        {listItems()}
      </List>
    </Box>
  );
}

export default HomePage;
