import { ProjectStatus } from "./ProjectStatus";

export type ProjectInformation = {
  id: string;
  name: string;
  createdDate: Date;
  lastUpdated: Date;
  status: ProjectStatus;
};
