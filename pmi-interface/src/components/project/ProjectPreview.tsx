import {
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  styled,
  Tooltip,
  Typography,
} from "@mui/material";
import { Project } from "../../models/Project";
import LanguageIcon from "@mui/icons-material/Language";
import DnsIcon from "@mui/icons-material/Dns";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import AccessTimeIcon from "@mui/icons-material/AccessTime";
import PeopleAltOutlinedIcon from "@mui/icons-material/PeopleAltOutlined";
import DateTimeFormat from "../../utils/DateTimeFormat";
import { useNavigate } from "react-router-dom";

type ProjectInfoPanelProps = {
  project: Project;
};

const ProjectPreview = ({ project }: ProjectInfoPanelProps) => {
  const navigate = useNavigate();
  const TargetRecordsBox = (
    <TargetRecordsStyledBox>
      <Box sx={{ display: "flex", alignItems: "center" }}>
        <LanguageIcon sx={{ fontSize: 15 }} color="secondary" />
        <Typography
          ml={1}
          color="secondary"
          variant="body2"
          display={"inline-block"}
        >
          {project.domainName}
        </Typography>
      </Box>
      <Box sx={{ display: "flex", alignItems: "center" }} mt={1}>
        <DnsIcon sx={{ fontSize: 15 }} color="secondary" />
        <Typography
          ml={1}
          color="secondary"
          variant="body2"
          display={"inline-block"}
        >
          {project.ipAddress}
        </Typography>
      </Box>
    </TargetRecordsStyledBox>
  );

  return (
    <StyledCard
      variant="outlined"
      onClick={() => navigate(`/project/${project.name}`)}
    >
      <CardHeader
        title={
          <Typography noWrap color="primary" maxWidth={220}>
            {project.name}
          </Typography>
        }
        action={
          <ProjectStatus
            disableRipple
            disableElevation
            variant="contained"
            size="small"
          >
            Completed
          </ProjectStatus>
        }
      />
      <CardContent sx={{ paddingTop: 0 }}>
        <Typography color="textSecondary" variant="body2" noWrap maxWidth={300}>
          {project.description}
        </Typography>
        {TargetRecordsBox}
        {ProjectInfo(
          "Created: ",
          CalendarTodayIcon,
          DateTimeFormat(project.projectInfo.createdDate)
        )}
        {ProjectInfo(
          "Updated: ",
          AccessTimeIcon,
          DateTimeFormat(project.projectInfo.lastUpdated)
        )}
        {ProjectInfo("1 Collaborators", PeopleAltOutlinedIcon, "")}
      </CardContent>
    </StyledCard>
  );
};

const ProjectInfo = (title: string, Icon: React.ElementType, info: string) => (
  <Box sx={{ display: "flex", alignItems: "center" }} mt={0.5}>
    <Icon sx={{ fontSize: 18, color: "#B3B3B3" }} />
    <Typography variant="body2" color="textSecondary" ml={1}>
      {title} {info}
    </Typography>
  </Box>
);

const ProjectStatus = styled(Button)(({ theme }) => ({
  paddingTop: 1,
  paddingBottom: 1,
  marginTop: 2,
  textTransform: "capitalize",
  backgroundColor: "#37ff141c",
  color: theme.palette.primary.main,
}));

const TargetRecordsStyledBox = styled(Box)(({ theme }) => ({
  marginTop: 10,
  backgroundColor: "#0a0a0a",
  border: "1px solid #2a2a2a",
  padding: 8,
  marginBottom: 20,
}));

const StyledCard = styled(Card)(({ theme }) => ({
  width: 350,
  borderRadius: 12,
  transition: "box-shadow 0.3s ease",
  "&:hover": {
    cursor: "pointer",
    boxShadow: "0px 0px 22px -5px #37ff14b0",
  },
}));

export default ProjectPreview;
