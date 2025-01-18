import { ProjectInfo } from "./ProjectInfo";

export type Project = {
  id: string;
  name: string;
  createdDate: Date;
  lastUpdated?: Date;
  projectInfo: ProjectInfo;
};
