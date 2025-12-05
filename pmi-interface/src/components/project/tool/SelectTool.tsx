import { Box, BoxProps, styled, Typography } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import React, { useState } from "react";
import { fetchInstalledTools } from "../../../services/ApiService";
import { InstalledTool } from "../../../models/InstalledTool";

type Props = {
  selectTool: (t: string) => void;
  selectedTool: string;
};

const SelectTool = ({ selectTool, selectedTool }: Props) => {
  const { data } = useQuery({
    queryKey: ["installedTools"],
    queryFn: fetchInstalledTools,
  });

  return (
    <Box mt={3}>
      <Typography variant="subtitle2" color="secondary" mb={1}>
        Select Tool *
      </Typography>
      <Box
        gap={1}
        sx={{
          display: "flex",
          flexDirection: "row",
          flexWrap: "wrap",
          overflowY: "scroll",
        }}
        maxHeight={220}
      >
        {data?.map((tool, index) => (
          <Tool
            key={index}
            selected={selectedTool === tool.name}
            tool={tool}
            onClick={() => selectTool(tool.name)}
          />
        ))}
      </Box>
    </Box>
  );
};

const ToolContainer = styled(Box)(({ theme }) => ({
  background: theme.palette.background.default,
  padding: 10,
  width: "48%",
  border: "0.1rem solid",
  borderColor: theme.palette.divider,
  borderRadius: 6,
  "&:hover": {
    cursor: "pointer",
  },
}));

type ToolProps = BoxProps & {
  selected: boolean;
  tool: InstalledTool;
};

const Tool = ({ selected, tool, ...props }: ToolProps) => {
  return (
    <ToolContainer sx={{ background: selected ? "#37ff141c" : "" }} {...props}>
      <Typography variant="subtitle1" color={selected ? "primary" : ""}>
        {tool.name}
      </Typography>
      <Typography
        variant="body2"
        color="textDisabled"
        sx={{
          overflow: "hidden",
          whiteSpace: "nowrap",
          textOverflow: "ellipsis",
          display: "block",
        }}
      >
        Information missing
      </Typography>
    </ToolContainer>
  );
};

export default SelectTool;
