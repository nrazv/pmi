import { Box, Typography } from "@mui/material";
import DateTimeFormat from "../../utils/DateTimeFormat";
import { ProjectInformation } from "../../models/ProjectInfo";

type ProjectInfoPanelProps = {
  projectInfo: ProjectInformation;
};

function ProjectInfo({ projectInfo }: ProjectInfoPanelProps) {
  const createdDate = new Date(projectInfo.createdDate);
  const lastUpdated = new Date(projectInfo.lastUpdated);
  return (
    <Box sx={{ display: "flex" }}>
      <Box>
        <Typography>Name:</Typography>
        <Typography>Created Date:</Typography>
        <Typography>Last Update:</Typography>
      </Box>

      <Box sx={{ marginLeft: 4 }}>
        <Typography>{projectInfo.name}</Typography>
        <Typography>
          <DateTimeFormat date={createdDate} />
        </Typography>
        <Typography>
          <DateTimeFormat date={lastUpdated} />
        </Typography>
      </Box>
    </Box>
  );
}

export default ProjectInfo;
