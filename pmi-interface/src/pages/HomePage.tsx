import { Box } from "@mui/material";

import { Project } from "../shared/Project";
import { apiServiceProvider } from "../services/ApiService";
import { useQuery } from "@tanstack/react-query";
import "./HomePage.css";
import ProjectToolBar from "../components/target-bar/ProjectToolBar";
import ProjectList from "../components/project/ProjectList";
import { useEffect, useState } from "react";
import ProjectTabs from "../components/project/ProjectTabs";

const HomePage = () => {
  const [selectedProject, setSelectedProject] = useState<Project>();
  const apiService = apiServiceProvider();
  const { error, isLoading, data } = useQuery({
    queryKey: ["projects"],
    queryFn: apiService.get<Project[]>("project/all"),
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
        <ProjectList selectProject={setSelectedProject} projects={data?.data} />
      )}

      <Box sx={{ flexGrow: 1, backgroundColor: "#eceff1" }}>
        <ProjectToolBar project={selectedProject} />
        <ProjectTabs />
      </Box>
    </Box>
  );
};

export default HomePage;
