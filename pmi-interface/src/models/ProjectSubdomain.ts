export interface ProjectSubdomain {
  id: string;
  domain: string;
  validated: boolean;
  validatedBy?: string | null;
  validationResult?: string | null;
  projectId: string;
}
