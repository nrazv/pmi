import {
  Backdrop,
  Button,
  CircularProgress,
  Container,
  styled,
} from "@mui/material";
import React, { useState } from "react";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { useNavigate, useParams } from "react-router-dom";
import ProjectInfo from "../components/ProjectInfo";
import { useQuery } from "@tanstack/react-query";
import { fetchProjectByName } from "../services/ApiService";

const ProjectPage = () => {
  const { name } = useParams();
  const { data, isLoading } = useQuery({
    queryKey: ["project", name],
    queryFn: () => fetchProjectByName(name!),
    enabled: !!name,
  });

  const navigate = useNavigate();

  return (
    <React.Fragment>
      <Container sx={{ marginTop: 3, padding: 0 }}>
        <NavLink
          startIcon={<ArrowBackIcon />}
          disableTouchRipple
          variant="text"
          onClick={() => navigate("/")}
        >
          Back To Projects
        </NavLink>
        <ProjectInfo />
      </Container>
      <Backdrop open={isLoading}>
        <CircularProgress color="primary" />
      </Backdrop>
    </React.Fragment>
  );
};

const NavLink = styled(Button)(({ theme }) => ({
  transition: "all 0.2s ease",
  color: theme.palette.text.disabled,
  background: "transparent",
  fontSize: 16,
  textTransform: "capitalize",
  "&:hover": {
    cursor: "pointer",
    color: theme.palette.primary.main,
  },
}));

export default ProjectPage;
