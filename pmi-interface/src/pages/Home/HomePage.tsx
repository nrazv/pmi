import { useQuery } from "@tanstack/react-query";
import { fetchAllProjects } from "../../services/ApiService";
import { Project } from "../../models/Project";
import { styled, Typography, Box } from "@mui/material";
import { Container, flexbox } from "@mui/system";
import ProjectsSearchAndFilter from "../../components/ProjectsSearchAndFilter";
import ProjectPreview from "../../components/project/ProjectPreview";
import React from "react";
import NavBar from "../../components/navbar/NavBar";

const title = "Projects";
const subtitle =
  "Manage your penetration testing projects and security assessments";
const HomePage = () => {
  const { data } = useQuery({
    queryKey: ["projects"],
    queryFn: fetchAllProjects,
    refetchInterval: 9000,
  });

  return (
    <React.Fragment>
      <NavBar />
      <Container sx={{ marginTop: 3, padding: 0 }}>
        <StyledBox>
          <Typography variant="subtitle1" color="secondary">
            {title}
          </Typography>
          <Typography color="textSecondary" variant="body2">
            {subtitle}
          </Typography>
          <ProjectsSearchAndFilter />
        </StyledBox>
        <ProjectsBox gap={4}>
          {data?.map((p: Project) => (
            <ProjectPreview key={p.id} project={p} />
          ))}
        </ProjectsBox>
      </Container>
    </React.Fragment>
  );
};

const StyledBox = styled(Box)(({ theme }) => ({}));

const ProjectsBox = styled(Box)(({ theme }) => ({
  marginTop: 20,
  padding: "10px",
  display: "flex",
  flexWrap: "wrap",
  height: "70vh",
  overflowY: "scroll",
}));

export default HomePage;
