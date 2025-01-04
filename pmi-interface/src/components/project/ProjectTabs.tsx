import { Tab, Tabs } from "@mui/material";
import React from "react";

function ProjectTabs() {
  return (
    <Tabs indicatorColor="secondary">
      <Tab label="Project Info" />
      <Tab label="Item Two" />
      <Tab label="Item Three" />
    </Tabs>
  );
}

export default ProjectTabs;
