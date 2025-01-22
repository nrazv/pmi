import { InstalledTool } from "../models/InstalledTool";
import { Project } from "../models/Project";
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

export const fetchInstalledTools = async (): Promise<InstalledTool[]> => {
  const response = await axiosInstance.get<InstalledTool[]>("tool/installed");
  return response.data;
};
