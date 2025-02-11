import { Tab, Tabs, Tooltip } from "@mui/material";
import React from "react";
import { Project } from "../../../models/Project";
import ProjectInfoPanel from "../panel-tabs/ProjectInfoPanel";
import TerminalIcon from "@mui/icons-material/Terminal";
import ToolExecutionPanel from "../panel-tabs/ToolExecutionPanel";
import AutoModeIcon from "@mui/icons-material/AutoMode";
import RunningToolsPanel from "../panel-tabs/RunningToolsPanel";
import CustomTabPanel from "./CustomTabPanel";

type ProjectTabsProps = {
  project: Project;
};

function ProjectTabs({ project }: ProjectTabsProps) {
  const [tabIndex, setTabIndex] = React.useState(0);
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabIndex(newValue);
  };

  return (
    <>
      <Tabs indicatorColor="secondary" value={tabIndex} onChange={handleChange}>
        <Tab label="Project Info" id="info-tab" />

        <Tooltip title="Execute Tool">
          <Tab icon={<TerminalIcon />} id="item-tab" />
        </Tooltip>

        <Tooltip title="Running Tools">
          <Tab icon={<AutoModeIcon />} id="info-tab-2" />
        </Tooltip>
      </Tabs>

      <CustomTabPanel value={tabIndex} index={0}>
        <ProjectInfoPanel projectInfo={project.projectInfo} />
      </CustomTabPanel>

      <CustomTabPanel value={tabIndex} index={1}>
        <ToolExecutionPanel project={project} />
      </CustomTabPanel>

      <CustomTabPanel value={tabIndex} index={2}>
        <RunningToolsPanel />
      </CustomTabPanel>
    </>
  );
}

export default ProjectTabs;
