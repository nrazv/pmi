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
