import {
  Backdrop,
  Button,
  CircularProgress,
  Container,
  styled,
} from "@mui/material";
import React from "react";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { useNavigate, useParams } from "react-router-dom";
import ProjectInfo from "../components/ProjectInfo";
import { useQuery } from "@tanstack/react-query";
import { fetchProjectByName } from "../services/ApiService";
import ProjectFeaturesTabs from "../components/project/tabs/ProjectFeaturesTabs";

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
      <Container
        maxWidth={false}
        sx={{
          marginTop: 3,
          paddingBottom: 10,
          width: "70vw",
        }}
      >
        <NavLink
          startIcon={<ArrowBackIcon />}
          disableTouchRipple
          variant="text"
          onClick={() => navigate("/")}
        >
          Back To Projects
        </NavLink>
        {data && <ProjectInfo project={data} />}
        {data && <ProjectFeaturesTabs project={data} />}
      </Container>
      <Backdrop open={isLoading}>
        <CircularProgress color="primary" />
      </Backdrop>
    </React.Fragment>
  );
};

const NavLink = styled(Button)(({ theme }) => ({
  transition: "all 0.2s ease",
  color: theme.palette.text.secondary,
  background: "transparent",
  fontSize: 16,
  textTransform: "capitalize",
  "&:hover": {
    cursor: "pointer",
    color: theme.palette.primary.main,
  },
  marginBottom: 10,
}));

export default ProjectPage;
