export type ExecutedTool = {
  id: string;
  projectId: string;
  toolArguments: string;
  target: string;
  status: string;
  executionResult: string;
  executionDate: Date;
  finishedDate: Date;
  name: string;
};
