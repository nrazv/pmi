import { ProjectInformation } from "./ProjectInfo";
import { ProjectSubdomain } from "./ProjectSubdomain";

export type Project = {
  id: string;
  name: string;
  description: string;
  domainName: string;
  ipAddress: string;
  subdomains: ProjectSubdomain[];
  projectInfo: ProjectInformation;
};
