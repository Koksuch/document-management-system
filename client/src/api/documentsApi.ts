import type { ApiParamsType, DocumentFormDataType } from "../types/types"
import axiosInstance from "./axiosInstance"

export async function getDocuments(params: ApiParamsType) {
  const response = await axiosInstance.get("/documents", {
    params: params,
    paramsSerializer: {
      indexes: null,
    },
  })
  return response
}

export async function getDocumentById(id: number) {
  const response = await axiosInstance.get(`/documents/${id}`)
  return response
}

export async function createNewDocument(data: DocumentFormDataType) {
  const response = await axiosInstance.post("/documents", data)
  return response
}

export async function updateDocumentById(
  id: number,
  data: DocumentFormDataType,
) {
  const response = await axiosInstance.put(`/documents/${id}`, data)
  return response
}

export async function deleteDocumentById(id: number) {
  const response = await axiosInstance.delete(`/documents/${id}`)
  return response
}
