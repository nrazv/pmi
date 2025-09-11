import { Tab, Tabs, Tooltip } from "@mui/material";
import React from "react";
import { Project } from "../../../models/Project";
import TerminalIcon from "@mui/icons-material/Terminal";
import CustomTabPanel from "./CustomTabPanel";
import ProjectInfoPanel from "../ProjectInfoPanel";
import ToolExecutionPanel from "../ToolExecutionPanel";
import SettingsApplicationsIcon from "@mui/icons-material/SettingsApplications";

type ProjectTabsProps = {
  project: Project | undefined;
};

function ProjectTabs({ project }: ProjectTabsProps) {
  const [tabIndex, setTabIndex] = React.useState(0);
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabIndex(newValue);
  };

  return (
    <>
      <Tabs indicatorColor="secondary" value={tabIndex} onChange={handleChange}>
        <Tooltip title="Project Settings">
          <Tab id="settings-tab" icon={<SettingsApplicationsIcon />} />
        </Tooltip>

        <Tooltip title="Execute & Running Tool's">
          <Tab icon={<TerminalIcon />} id="execute-tab" />
        </Tooltip>
      </Tabs>

      <CustomTabPanel value={tabIndex} index={0}>
        {project?.projectInfo && (
          <ProjectInfoPanel projectInfo={project.projectInfo} />
        )}
      </CustomTabPanel>

      <CustomTabPanel value={tabIndex} index={1}>
        {project && <ToolExecutionPanel project={project} />}
      </CustomTabPanel>
    </>
  );
}

export default ProjectTabs;
