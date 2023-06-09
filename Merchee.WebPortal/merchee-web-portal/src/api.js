import axios from 'axios';
import store from './store';
import router from './router';
//import { useNotification } from 'naive-ui'

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5031',
  headers: {
    'Content-Type': 'application/json',
  }
});

//const notification = useNotification();

axiosInstance.interceptors.request.use(
  (config) => {
      const token = localStorage.getItem('token');

      if (token) {
          config.headers['Authorization'] = `Bearer ${token}`;
      }

      return config;
  },

  (error) => {
      return Promise.reject(error);
  }
);

const http = {
  axiosInstance: axiosInstance,

  get(url, params = {}, config = {}) {
    return axiosInstance.get(url, { ...config, params })
      .then(response => response.data)
      .catch(error => {
        alert(error);
      });
  },

  post(url, data = {}, config = {}) {
    return axiosInstance.post(url, data, config)
      .then(response => response.data)
      .catch(error => {
        alert(error);
      });
  },

  put(url, data = {}, config = {}) {
    return axiosInstance.put(url, data, config)
      .then(response => response.data)
      .catch(error => {
        alert(error);
      });
  },

  // Define other HTTP methods as needed (put, delete, etc.)
};

axiosInstance.interceptors.response.use(function (response) {
  return response
}, function (error) {
  console.log(error.response.data)
  if (error.response.status === 401) {
    store.commit("logOut");
    router.push('/login')
  }
  return Promise.reject(error)
})

export default http;