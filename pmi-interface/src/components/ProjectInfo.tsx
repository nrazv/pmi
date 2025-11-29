import {
  Box,
  Button,
  Card,
  Chip,
  Divider,
  FormControl,
  MenuItem,
  Select,
  SelectChangeEvent,
  Stack,
  styled,
  Typography,
} from "@mui/material";
import React, { useState } from "react";
import { Project } from "../models/Project";
import LanguageIcon from "@mui/icons-material/Language";
import DnsIcon from "@mui/icons-material/Dns";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import AccessTimeIcon from "@mui/icons-material/AccessTime";
import DateTimeFormat from "../utils/DateTimeFormat";
import PeopleAltOutlinedIcon from "@mui/icons-material/PeopleAltOutlined";
import PersonAddAltOutlinedIcon from "@mui/icons-material/PersonAddAltOutlined";

interface Props {
  project: Project;
}

const ProjectInfo = ({ project }: Props) => {
  const [status, setStatus] = useState<string>("");

  const statuses = [
    "Not Started",
    "In Progress",
    "On Hold",
    "Completed",
    "Archived",
  ];
  const handleChange = (
    event: SelectChangeEvent<unknown>,
    _child: React.ReactNode
  ) => {
    setStatus(event.target.value as string);
  };

  return (
    <CardContainer>
      <Stack direction="row">
        <Box sx={{ flex: 1 }}>
          <ProjectName color="primary" variant="body1">
            {project.name}
          </ProjectName>
          <Typography color="textSecondary" mt={1} variant="body1">
            {project.description}
          </Typography>
          <TargetRecordsStyledBox>
            <Box sx={{ display: "flex", alignItems: "center" }}>
              <LanguageIcon sx={{ fontSize: 18 }} color="secondary" />
              <Typography
                ml={1}
                color="secondary"
                variant="body1"
                display={"inline-block"}
              >
                {project.domainName}
              </Typography>
            </Box>
            <Box sx={{ display: "flex", alignItems: "center" }} ml={2}>
              <DnsIcon sx={{ fontSize: 18 }} color="secondary" />
              <Typography
                ml={1}
                color="secondary"
                variant="body1"
                display={"inline-block"}
              >
                {project.ipAddress}
              </Typography>
            </Box>
          </TargetRecordsStyledBox>
          <Box sx={{ display: "flex" }} mt={2} mb={2}>
            {ProjectInformation(
              "Created: ",
              CalendarTodayIcon,
              DateTimeFormat(project.projectInfo.createdDate)
            )}
            {ProjectInformation(
              "Updated: ",
              AccessTimeIcon,
              DateTimeFormat(project.projectInfo.lastUpdated)
            )}
            {ProjectInformation("1 Collaborators", PeopleAltOutlinedIcon, "")}
          </Box>
        </Box>
        <Box width={250}>
          <Typography color="textSecondary" mt={1} variant="subtitle2" mb={1}>
            Status
          </Typography>
          <StatusSelectForm fullWidth variant="outlined" size="small">
            <SelectStatus
              id="select-status"
              value={status}
              onChange={handleChange}
            >
              {statuses.map((s) => (
                <SelectMenuItem value={s}>{s}</SelectMenuItem>
              ))}
            </SelectStatus>
          </StatusSelectForm>
          <Button
            fullWidth
            variant="contained"
            sx={{ textTransform: "capitalize" }}
            startIcon={<PersonAddAltOutlinedIcon />}
          >
            Invite Collaborator
          </Button>
        </Box>
      </Stack>
      <Divider />
      <Typography variant="body2" color="textSecondary" mt={2} mb={1}>
        Collaborators:
      </Typography>
      <CollaboratorChip
        label="charlie@security.co"
        variant="outlined"
        size="medium"
      />
    </CardContainer>
  );
};

const ProjectInformation = (
  title: string,
  Icon: React.ElementType,
  info: string
) => (
  <Box sx={{ display: "flex", alignItems: "center" }} mr={3}>
    <Icon sx={{ fontSize: 18, color: "#B3B3B3", marginRight: 1 }} />
    <Typography variant="body2" color="textSecondary">
      {title} {info}
    </Typography>
  </Box>
);

const CardContainer = styled(Card)(({ theme }) => ({
  padding: 25,
  borderRadius: 10,
  background: theme.palette.background.paper,
  border: `0.1rem solid ${theme.border.color.light}`,
}));

const TargetRecordsStyledBox = styled(Box)(({ theme }) => ({
  marginTop: 10,
  backgroundColor: "#0a0a0a",
  border: `1px solid ${theme.border.color.light}`,
  padding: 10,
  display: "flex",
  alignItems: "center",
  width: "fit-content",
  borderRadius: 5,
}));

const StatusSelectForm = styled(FormControl)(({ theme }) => ({
  background: theme.palette.background.default,
  marginBottom: 15,
}));

const SelectStatus = styled(Select)(({ theme }) => ({
  background: theme.palette.background.default,
}));

const SelectMenuItem = styled(MenuItem)(({ theme }) => ({
  "&.Mui-selected": {
    backgroundColor: theme.palette.background.default,
  },
}));

const CollaboratorChip = styled(Chip)(({ theme }) => ({
  background: theme.palette.background.default,
  color: theme.palette.secondary.main,
  borderColor: theme.border.color.light,
  paddingBottom: 0,
  paddingTop: 0,
}));

const ProjectName = styled(Typography)(({ theme }) => ({}));

export default ProjectInfo;
