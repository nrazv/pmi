import { useQuery } from "@tanstack/react-query";
import { useEffect, useRef, useState } from "react";
import { fetchAllProjects } from "../../services/ApiService";
import { Project } from "../../models/Project";
import Box from "@mui/material/Box";
import { Grid } from "@mui/material";
import ProjectList from "../../components/project/ProjectsList/ProjectList";
import NavigationTabs from "../../components/project/tabs/NavigationTabs";

const HomePage = () => {
  const [selectedProject, setSelectedProject] = useState<Project>();

  const { data } = useQuery({
    queryKey: ["projects"],
    queryFn: fetchAllProjects,
    refetchInterval: 9000,
  });

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        height: "91vh",
        overflow: "hidden",
      }}
    >
      <Grid
        container
        sx={{
          flex: 1,
          display: "flex",
          flexDirection: "row",
          overflow: "hidden",
        }}
      >
        <Grid item xs={2} sx={{ overflowY: "auto", height: "100%" }}>
          <Box
            sx={{
              padding: 2,
              height: "100%",
              borderRight: 1,
            }}
          >
            {data && (
              <ProjectList selectProject={setSelectedProject} projects={data} />
            )}
          </Box>
        </Grid>
        <Grid item flex={1} sx={{ overflowY: "hidden", height: "100%" }}>
          <Box
            sx={{
              padding: 2,
              height: "100%",
            }}
          >
            {selectedProject && <NavigationTabs project={selectedProject} />}
          </Box>
        </Grid>
      </Grid>
    </Box>
  );
};

export default HomePage;
