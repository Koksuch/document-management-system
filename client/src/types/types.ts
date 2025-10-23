import type { DocumentsSortFields } from "../helpers/enums"

export type DocumentType = {
  id: number
  type: string
  date: Date
  firstName: string
  lastName: string
  city: string
}

export type DocumentItemType = {
  id: number
  documentId: number
  ordinal: number
  product: string
  quantity: number
  price: number
  taxRate: number
}

export type ApiParamsType = {
  page: number
  pageSize: number
  sortBy: DocumentsSortFieldType
  descending: boolean
  search: string
  dateFrom: string | null
  dateTo: string | null
}

export type DocumentsSortFieldType =
  (typeof DocumentsSortFields)[keyof typeof DocumentsSortFields]

export type DocumentFormDataType = Omit<DocumentType, "id">

export type DocumentItemFormDataType = Omit<
  DocumentItemType,
  "id" | "documentId"
>
