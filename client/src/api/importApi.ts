import axiosInstance from "./axiosInstance"

export async function importDataToDb(formData: FormData) {
  const response = await axiosInstance.post(`/upload`, formData)
  return response
}
