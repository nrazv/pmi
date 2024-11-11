import axios from "axios";

const axiosInstance = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
});

axiosInstance.interceptors.request.use(async (req) => {
  req.headers["Content-Type"] = "application/json";
  req.headers["Access-Control-Allow-Origin"] = "*";
  req.headers["Allow-Origin"] = "*";
  return req;
});

export default axiosInstance;
