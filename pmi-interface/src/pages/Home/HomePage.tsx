import { Box } from "@mui/material";

import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { fetchAllProjects } from "../../services/ApiService";
import { Project } from "../../models/Project";
import ProjectList from "../../components/project/ProjectList";
import ProjectToolBar from "../../components/ProjectToolBar";
import ProjectTabs from "../../components/project/tabs/ProjectTabs";

const mainBoxStyles = { display: "flex", flexDirection: "row", height: "94vh" };
const toolBarBoxStyles = { flexGrow: 1, backgroundColor: "#eceff1" };

const HomePage = () => {
  const [selectedProject, setSelectedProject] = useState<Project>();
  const { error, isLoading, data } = useQuery({
    queryKey: ["projects"],
    queryFn: fetchAllProjects,
  });

  return (
    <Box sx={mainBoxStyles}>
      {data && (
        <ProjectList selectProject={setSelectedProject} projects={data} />
      )}

      <Box sx={toolBarBoxStyles}>
        <ProjectToolBar project={selectedProject} />
        {selectedProject && <ProjectTabs project={selectedProject} />}
      </Box>
    </Box>
  );
};

export default HomePage;
