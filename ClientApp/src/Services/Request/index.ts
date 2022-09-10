import axios, { AxiosResponse } from 'axios';
import { CookieService } from '../Cookie';

export const BaseUrl = window.location.origin;

const Instance = axios.create({
  baseURL: `${BaseUrl}/api/`,
  timeout: 15000,
  headers: {
    "Content-Type": "application/json"
  }
});

Instance.interceptors.request.use(
  (config) => {
    let token = CookieService.get("AccessToken");
    if (token !== undefined) {
      // @ts-ignore
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

const responseBody = (response: AxiosResponse) => response?.data;

const RequestService = {
  get: (url: string) => Instance.get(url).then(responseBody),
  post: (url: string, body: {}) => Instance.post(url, body).then(responseBody),
  put: (url: string, body: {}) => Instance.put(url, body).then(responseBody),
  delete: (url: string) => Instance.delete(url).then(responseBody)
};

export { RequestService };