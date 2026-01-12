import { ExecutedTool } from "../models/ExecutedTool";
import { InstalledTool } from "../models/InstalledTool";
import { ModuleExecutionRequest } from "../models/ModuleExecutionRequest";
import { Module } from "../models/Modules";
import { Project } from "../models/Project";
import { ToolExecuteRequest } from "../models/ToolExecuteRequest";
import axiosInstance from "./axios";

class ApiService {
  get<T>(path: string) {
    return async () => {
      const response = await axiosInstance.get<T>(path);
      return response;
    };
  }

  post<T>(path: string, body: Object) {
    return async () => {
      const response = await axiosInstance.post<T>(path, body);
      return response;
    };
  }
}

export function apiServiceProvider() {
  return new ApiService();
}

export const fetchAllProjects = async (): Promise<Project[]> => {
  const response = await axiosInstance.get<Project[]>("project/all");
  return response.data;
};

export const fetchProjectByName = async (name: string): Promise<Project> => {
  const response = await axiosInstance.get<Project>(`project/${name}`);
  return response.data;
};

export const fetchInstalledTools = async (): Promise<InstalledTool[]> => {
  const response = await axiosInstance.get<InstalledTool[]>("tool/installed");
  return response.data;
};

export const fetchExecutedToolsForProject = async (
  projectName: string
): Promise<ExecutedTool[]> => {
  const response = await axiosInstance.get<ExecutedTool[]>(
    `tool/executed/${projectName}`
  );
  return response.data;
};

export const executeTool = async (request: ToolExecuteRequest) => {
  const response = await axiosInstance.post("tool/execute", request);
  return response.data;
};

export const fetchModules = async (): Promise<Module[]> => {
  const response = await axiosInstance.get<Module[]>("modules/all");
  return response.data;
};

export const executeModule = async (request: ModuleExecutionRequest) => {
  const response = await axiosInstance.post("definedmodules/execute", request);
  return response.data;
};
