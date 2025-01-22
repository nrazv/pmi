import { Typography } from "@mui/material";
import { ProjectInfo } from "../../../models/ProjectInfo";
import DateTimeFormat from "../../../utils/DateTimeFormat";

type ProjectInfoPanelProps = {
  projectInfo: ProjectInfo;
};

function ProjectInfoPanel({ projectInfo }: ProjectInfoPanelProps) {
  const createdDate = new Date(projectInfo.createdDate);
  const lastUpdated = new Date(projectInfo.lastUpdated);
  return (
    <>
      <div className="flex">
        <Typography className="pr-4" align="left" variant="h6" gutterBottom>
          Name:
        </Typography>
        <Typography variant="h6" gutterBottom>
          {projectInfo.name}
        </Typography>
      </div>
      <div className="flex">
        <Typography className="pr-6" align="left" variant="h6" gutterBottom>
          Created Date:
        </Typography>
        <Typography variant="h6" gutterBottom>
          <DateTimeFormat date={createdDate} />
        </Typography>
      </div>
      <div className="flex">
        <Typography className="pr-4" align="left" variant="h6" gutterBottom>
          Last Update:
        </Typography>
        <Typography variant="h6" gutterBottom>
          <DateTimeFormat date={lastUpdated} />
        </Typography>
      </div>
    </>
  );
}

export default ProjectInfoPanel;
