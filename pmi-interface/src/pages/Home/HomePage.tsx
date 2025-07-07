import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { fetchAllProjects } from "../../services/ApiService";
import { Project } from "../../models/Project";
import ProjectList from "../../components/project/ProjectList";
import ProjectTabs from "../../components/project/tabs/ProjectTabs";
import Box from "@mui/material/Box";

const HomePage = () => {
  const [selectedProject, setSelectedProject] = useState<Project>();

  const { data } = useQuery({
    queryKey: ["projects"],
    queryFn: fetchAllProjects,
  });

  return (
    <Box sx={{ height: "90vh", display: "flex" }}>
      <Box sx={{ width: "10%", borderRight: "solid #e6e7e8" }}>
        {data && (
          <ProjectList selectProject={setSelectedProject} projects={data} />
        )}
      </Box>
      <Box sx={{ flex: 1 }}>
        {selectedProject && <ProjectTabs project={selectedProject} />}
      </Box>
    </Box>
  );
};

export default HomePage;
