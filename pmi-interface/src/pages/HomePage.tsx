import { Box } from "@mui/material";

import { Project } from "../shared/Project";
import { apiServiceProvider } from "../services/ApiService";
import { useQuery } from "@tanstack/react-query";
import "./HomePage.css";
import TargetBar from "../components/target-bar/TargetBar";
import ProjectList from "../components/project/ProjectList";

const HomePage = () => {
  const apiService = apiServiceProvider();

  const { error, isLoading, data } = useQuery({
    queryKey: ["projects"],
    queryFn: apiService.get<Project[]>("project/all"),
  });

  const projects: Project[] = [];
  data?.data.map((p) => projects.push(p));

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "row",
        height: "-webkit-fill-available",
      }}
    >
      <ProjectList projects={projects} />

      <Box sx={{ flexGrow: 1, backgroundColor: "#eceff1" }}>
        <TargetBar />
      </Box>
    </Box>
  );
};

export default HomePage;
