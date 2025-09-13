import { Tab, Tabs, Tooltip } from "@mui/material";
import React from "react";
import { Project } from "../../../models/Project";
import TerminalIcon from "@mui/icons-material/Terminal";
import CustomTabPanel from "./CustomTabPanel";
import ProjectInfo from "../ProjectInfo";
import SettingsApplicationsIcon from "@mui/icons-material/SettingsApplications";
import ExecutionManagerTab from "../ExecutionManagerTab";

type ProjectTabsProps = {
  project: Project | undefined;
};

function NavigationTabs({ project }: ProjectTabsProps) {
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
          <ProjectInfo projectInfo={project.projectInfo} />
        )}
      </CustomTabPanel>

      <CustomTabPanel value={tabIndex} index={1}>
        {project && <ExecutionManagerTab project={project} />}
      </CustomTabPanel>
    </>
  );
}

export default NavigationTabs;
