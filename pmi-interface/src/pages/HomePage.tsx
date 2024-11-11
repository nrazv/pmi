import {
  Box,
  ListItemIcon,
  MenuItem,
  MenuList,
  Paper,
  Typography,
} from "@mui/material";

import { Project } from "../shared/Project";
import { apiServiceProvider } from "../services/ApiService";
import { useQuery } from "@tanstack/react-query";
import FolderIcon from "@mui/icons-material/Folder";
import "./HomePage.css";

const HomePage = () => {
  const apiService = apiServiceProvider();
  const { error, isLoading, data } = useQuery({
    queryKey: ["projects"],
    queryFn: apiService.get<Project[]>("project/all"),
  });

  const listItems = () => {
    const items = data?.data.map((project: Project) => (
      <MenuItem sx={{ margin: 1, padding: 1 }} key={project.name}>
        <ListItemIcon>
          <FolderIcon fontSize="small" />
        </ListItemIcon>
        <Typography variant="body2" sx={{ color: "text.secondary" }}>
          {project.name}
        </Typography>
      </MenuItem>
    ));

    return items;
  };

  return (
    <Box
      className="home-page"
      sx={{
        display: "flex",
        flexDirection: "row",
        height: "-webkit-fill-available",
      }}
    >
      <Box>
        <MenuList>{listItems()}</MenuList>
      </Box>
      <Box sx={{ flexGrow: 1, background: "lightgrey", padding: 1 }}>Box B</Box>
    </Box>
  );
};

export default HomePage;
