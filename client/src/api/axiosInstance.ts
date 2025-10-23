import axios from "axios"

const axiosInstance = axios.create({
  baseURL: "http://localhost:5190/api",
})

export default axiosInstance
