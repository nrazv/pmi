import { Box, BoxProps, Stack, styled, Typography } from "@mui/material";
import { Project } from "../models/Project";
import { useState } from "react";
import LanguageIcon from "@mui/icons-material/Language";
import DnsIcon from "@mui/icons-material/Dns";

type Props = {
  selectTarget: (target: string) => void;
  project: Project;
  selectedTarget: string;
};

interface Target {
  target: string;
  icon: React.ElementType;
  label: string;
}

function SelectTarget({ selectTarget, project, selectedTarget }: Props) {
  const targets: Target[] = [
    { target: project.domainName, icon: LanguageIcon, label: "Domain" },
    { target: project.ipAddress, icon: DnsIcon, label: "IP Address" },
  ];

  return (
    <Box mt={3}>
      <Typography variant="subtitle1" color="secondary" mb={1}>
        Select Target *
      </Typography>
      <Box>
        {targets.map((t) => (
          <Target
            key={t.target}
            selected={selectedTarget === t.target}
            target={t.target}
            Icon={t.icon}
            label={t.label}
            onClick={() => selectTarget(t.target)}
          />
        ))}
      </Box>
    </Box>
  );
}
<DnsIcon sx={{ fontSize: 15 }} color="secondary" />;

const TargetContainer = styled(Box)(({ theme }) => ({
  background: theme.palette.background.default,
  padding: 10,
  border: "1px solid",
  borderColor: theme.palette.divider,
  borderRadius: 3,
  marginTop: 10,
  "&:hover": {
    cursor: "pointer",
  },
}));

type TargetProps = BoxProps & {
  selected: boolean;
  target: string;
  Icon: React.ElementType;
  label: string;
};

const Target = ({ selected, target, Icon, label, ...props }: TargetProps) => {
  return (
    <TargetContainer
      sx={{ background: selected ? "#37ff141c" : "" }}
      {...props}
    >
      <Stack direction="row" alignItems="center" gap={1}>
        <Box alignItems="center" justifyContent="center" display="flex">
          <Icon color="secondary" />
        </Box>
        <Box>
          <Typography
            p={0}
            m={0}
            fontSize={12}
            variant="subtitle2"
            color="textSecondary"
          >
            {label}
          </Typography>
          <Typography
            p={0}
            m={0}
            lineHeight={1}
            variant="subtitle2"
            color={selected ? "primary" : ""}
          >
            {target}
          </Typography>
        </Box>
      </Stack>
    </TargetContainer>
  );
};

export default SelectTarget;
