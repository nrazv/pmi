import { ExecutionStatus } from "./ExecutionStatus";

export type ExecutedTool = {
  id: string;
  projectId: string;
  toolArguments: string;
  target: string;
  status: ExecutionStatus;
  executionResult: string;
  executionDate: Date;
  finishedDate: Date;
  name: string;
};
