import { Card, styled, Typography } from "@mui/material";
import { ExecutedTool } from "../../models/ExecutedTool";
import ExecutedProcess from "./ExecutedProcess";

type Props = {
  executedTools: ExecutedTool[];
};

const RunningProcessContainer = ({ executedTools }: Props) => {
  return (
    <StyledCard>
      <Typography variant="body1" color="primary">
        Running Process
      </Typography>
      {executedTools.map((e) => (
        <ExecutedProcess executedProcess={e} />
      ))}
    </StyledCard>
  );
};

const StyledCard = styled(Card)(({ theme }) => ({
  padding: 20,
  flex: 1,
  borderRadius: 10,
  background: theme.palette.background.paper,
  border: `0.1rem solid ${theme.border.color.light}`,
}));

export default RunningProcessContainer;
