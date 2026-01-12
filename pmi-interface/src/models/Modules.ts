import { ExecutableTool } from "./ExecutableTool";

export interface Module {
  id: string;
  name: string;
  description: string;
  executablesTools: ExecutableTool[];
}
