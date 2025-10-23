import type { DocumentItemFormDataType } from "../types/types"
import axiosInstance from "./axiosInstance"

export async function getAllDocumentItems() {
  const response = await axiosInstance.get("/documentitems")
  return response
}

export async function getDocumentItemsByDocumentId(documentId: number) {
  const response = await axiosInstance.get(
    `/documents/${documentId}/documentitems`,
  )
  return response
}

export async function getDocumentItemById(id: number) {
  const response = await axiosInstance.get(`/documentitems/${id}`)
  return response
}

export async function createDocumentItem(
  documentId: number,
  data: DocumentItemFormDataType,
) {
  const response = await axiosInstance.post(
    `/documentitems/${documentId}`,
    data,
  )
  return response
}

export async function updateDocumentItemById(
  id: number,
  data: DocumentItemFormDataType,
) {
  const response = await axiosInstance.put(`/documentitems/${id}`, data)
  return response
}

export async function deleteDocumentItemById(id: number) {
  const response = await axiosInstance.delete(`/documentitems/${id}`)
  return response
}
