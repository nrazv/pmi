import { Tab, Tabs } from "@mui/material";
import React from "react";
import { Project } from "../../../models/Project";
import CustomTabPanel from "./ProjectInfoTab";
import ProjectInfoPanel from "../panel-tabs/ProjectInfoPanel";

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
        <Tab label="Item Two" id="item-tab" />
        <Tab label="Item Three" id="info-tab-2" />
      </Tabs>
      <CustomTabPanel value={tabIndex} index={0}>
        <ProjectInfoPanel projectInfo={project.projectInfo} />
      </CustomTabPanel>
      <CustomTabPanel value={tabIndex} index={1}>
        Demo tab
      </CustomTabPanel>
    </>
  );
}

export default ProjectTabs;
