import React from "react";
import { ExecutedTool } from "../../models/ExecutedTool";
import { Box, Card, CircularProgress, styled, Typography } from "@mui/material";

type Props = {
  executedProcess: ExecutedTool;
};

const ExecutedProcess = ({ executedProcess }: Props) => {
  return (
    <StyledCard>
      <Box
        sx={{
          display: "flex",
          alignItems: "baseline",
        }}
      >
        <CircularProgress size={15} />
        <Typography variant="body1" ml={2} sx={{ flex: 1 }}>
          {executedProcess.name}
        </Typography>
        <Typography color="primary" variant="button" ml={2}>
          {executedProcess.status}
        </Typography>
      </Box>
    </StyledCard>
  );
};

const StyledCard = styled(Card)(({ theme }) => ({
  background: theme.palette.background.default,
  border: `0.1rem solid ${theme.border.color.light}`,
  borderRadius: 5,
  height: 250,
  padding: 10,
}));

export default ExecutedProcess;
