import { Box } from "@mui/material";

import { useQuery } from "@tanstack/react-query";
import "./HomePage.css";
import { useState } from "react";
import { fetchAllProjects } from "../../services/ApiService";
import { Project } from "../../models/Project";
import ProjectList from "../../components/project/ProjectList";
import ProjectToolBar from "../../components/target-bar/ProjectToolBar";
import ProjectTabs from "../../components/project/tabs/ProjectTabs";

const HomePage = () => {
  const [selectedProject, setSelectedProject] = useState<Project>();
  const { error, isLoading, data } = useQuery({
    queryKey: ["projects"],
    queryFn: fetchAllProjects,
  });

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "row",
        height: "-webkit-fill-available",
      }}
    >
      {data && (
        <ProjectList selectProject={setSelectedProject} projects={data} />
      )}

      <Box sx={{ flexGrow: 1, backgroundColor: "#eceff1" }}>
        <ProjectToolBar project={selectedProject} />
        {selectedProject && <ProjectTabs project={selectedProject} />}
      </Box>
    </Box>
  );
};

export default HomePage;
