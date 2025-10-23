import { defineStore } from "pinia"
import { ref, watch } from "vue"
import type {
  DocumentFormDataType,
  DocumentsSortFieldType,
  DocumentType,
} from "../types/types"
import {
  createNewDocument,
  deleteDocumentById,
  getDocuments,
  updateDocumentById,
} from "../api/documentsApi"
import { DocumentsSortFields } from "../helpers/enums"
import { useToast } from "vue-toastification"

const toast = useToast()

export const useDocumentStore = defineStore("documents", () => {
  const documents = ref<DocumentType[]>([])
  const totalCount = ref(0)
  const page = ref(1)
  const pageSize = ref(15)
  const totalPages = ref(1)
  const sortBy = ref<DocumentsSortFieldType>(DocumentsSortFields.ID)
  const sortDesc = ref(false)
  const isLoading = ref(false)
  const search = ref("")
  const dateFrom = ref<string | null>(null)
  const dateTo = ref<string | null>(null)

  async function fetchDocuments() {
    isLoading.value = true
    try {
      const res = await getDocuments({
        page: page.value,
        pageSize: pageSize.value,
        sortBy: sortBy.value,
        descending: sortDesc.value,
        search: search.value,
        dateFrom: dateFrom.value,
        dateTo: dateTo.value,
      })
      documents.value = res.data.items
      totalCount.value = res.data.totalCount
      totalPages.value = res.data.totalPages
      return res
    } catch (error) {
      toast.error("Error fetching documents. Try again later.")
      return error
    } finally {
      isLoading.value = false
    }
  }

  async function clearFilters() {
    search.value = ""
    dateFrom.value = null
    dateTo.value = null
    await fetchDocuments()
  }

  async function createDocument(data: DocumentFormDataType) {
    const res = await createNewDocument(data)
    await fetchDocuments()
    return res
  }

  async function updateDocument(id: number, data: DocumentFormDataType) {
    const res = await updateDocumentById(id, data)
    await fetchDocuments()
    return res
  }

  async function deleteDocument(id: number) {
    try {
      const res = await deleteDocumentById(id)
      await fetchDocuments()
      toast.success("Document deleted successfully")
      return res
    } catch (error) {
      toast.error("Error deleting document")
      return error
    }
  }

  watch([page, pageSize, sortBy, sortDesc], fetchDocuments)

  return {
    documents,
    totalCount,
    totalPages,
    page,
    pageSize,
    sortBy,
    sortDesc,
    search,
    dateFrom,
    dateTo,
    isLoading,
    fetchDocuments,
    createDocument,
    updateDocument,
    deleteDocument,
    clearFilters,
  }
})
