import { Box, styled, Tab, Tabs } from "@mui/material";
import React from "react";
import { Project } from "../../../models/Project";
import CustomTabPanel from "./CustomTabPanel";
import ToolRunnerTab from "./ToolRunnerTab";
import ModulesFeatureTab from "./ModulesFeatureTab";
import SubdomainsTab from "./SubdomainsTab";

type ProjectTabsProps = {
  project: Project | undefined;
};

function ProjectFeaturesTabs({ project }: ProjectTabsProps) {
  const [tabIndex, setTabIndex] = React.useState(0);
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabIndex(newValue);
  };

  return (
    <Box mt={3} p={0}>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs value={tabIndex} onChange={handleChange}>
          <ProjectTab id="modules-tab" label="Modules" />
          <ProjectTab id="tools-tab" label="Tools" />
          <ProjectTab
            id="subdomains-tab"
            label={`Subdomains (${project?.subdomains.length ?? 0})`}
          />
          <ProjectTab id="processes-tab" label="Processes (0)" />
          <ProjectTab id="notes-tab" label="Notes (0)" />
        </Tabs>
      </Box>
      <CustomTabPanel value={tabIndex} index={0}>
        {project && <ModulesFeatureTab project={project} />}
      </CustomTabPanel>
      <CustomTabPanel value={tabIndex} index={1}>
        {project && <ToolRunnerTab project={project} />}
      </CustomTabPanel>
      <CustomTabPanel value={tabIndex} index={2}>
        {project && <SubdomainsTab project={project} />}
      </CustomTabPanel>
    </Box>
  );
}

const ProjectTab = styled(Tab)(({ theme }) => ({
  textTransform: "capitalize",
}));

export default ProjectFeaturesTabs;
