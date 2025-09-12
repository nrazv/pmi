import { ProjectInformation } from "./ProjectInfo";

export type Project = {
  id: string;
  name: string;
  domainName: string;
  ipAddress: string;
  projectInfo: ProjectInformation;
};
