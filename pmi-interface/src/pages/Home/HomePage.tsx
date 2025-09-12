import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { fetchAllProjects } from "../../services/ApiService";
import { Project } from "../../models/Project";
import ProjectList from "../../components/project/ProjectsList/ProjectList";
import NavigationTabs from "../../components/project/tabs/NavigationTabs";
import Box from "@mui/material/Box";

const HomePage = () => {
  const [selectedProject, setSelectedProject] = useState<Project>();

  const { data } = useQuery({
    queryKey: ["projects"],
    queryFn: fetchAllProjects,
    refetchInterval: 5000,
  });

  return (
    <Box sx={{ height: "90vh", display: "flex" }}>
      <Box sx={{ width: "12%", borderRight: "solid #e6e7e8" }}>
        {data && (
          <ProjectList selectProject={setSelectedProject} projects={data} />
        )}
      </Box>
      <Box sx={{ flex: 1 }}>
        {selectedProject && <NavigationTabs project={selectedProject} />}
      </Box>
    </Box>
  );
};

export default HomePage;
