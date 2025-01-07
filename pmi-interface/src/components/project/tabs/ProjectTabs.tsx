import { Box, Tab, Tabs } from "@mui/material";
import React from "react";
import TabPanelProps from "./TabPanelProps";
import { Project } from "../../../models/Project";

function CustomTabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
    </div>
  );
}

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
      <Tabs
        indicatorColor="secondary"
        value={tabIndex}
        onChange={handleChange}
        aria-label="basic tabs example"
      >
        <Tab label="Project Info" id="info-tab" />
        <Tab label="Item Two" id="item-tab" />
        <Tab label="Item Three" id="info-tab-2" />
      </Tabs>
      <CustomTabPanel value={tabIndex} index={0}>
        Item One Test
      </CustomTabPanel>
      <CustomTabPanel value={tabIndex} index={1}>
        Item Two
      </CustomTabPanel>
      <CustomTabPanel value={tabIndex} index={2}>
        Item Three
      </CustomTabPanel>
    </>
  );
}

export default ProjectTabs;
