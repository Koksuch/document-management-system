<script setup lang="ts">
import { provide, ref } from "vue"
import Header from "./components/Header.vue"
import ImportForm from "./components/forms/ImportForm.vue"
import DocumentForm from "./components/forms/DocumentForm.vue"
import FiltersForm from "./components/forms/FiltersForm.vue"
import Pagination from "./components/table/Pagination.vue"
import Table from "./components/table/Table.vue"

const isImportFormVisible = ref(false)
const isDocumentFormVisible = ref(false)
const documentId = ref<number | null>(null)

const openImportForm = () => {
  isImportFormVisible.value = true
}
const closeImportForm = () => {
  isImportFormVisible.value = false
}

const openDocumentForm = () => {
  documentId.value = null
  isDocumentFormVisible.value = true
}
const closeDocumentForm = () => {
  documentId.value = null
  isDocumentFormVisible.value = false
}
const openEditDocumentForm = (id: number) => {
  documentId.value = id
  isDocumentFormVisible.value = true
}

provide("openImportForm", openImportForm)
provide("closeImportForm", closeImportForm)
provide("openDocumentForm", openDocumentForm)
provide("closeDocumentForm", closeDocumentForm)
provide("openEditDocumentForm", openEditDocumentForm)
</script>

<template>
  <div class="mx-10 mt-6 p-8 text-black">
    <Header />
    <ImportForm v-if="isImportFormVisible" />
    <DocumentForm v-if="isDocumentFormVisible" :id="documentId" />
    <FiltersForm />
    <div class="mt-5 rounded-xl border border-gray-300 bg-white">
      <Table />
    </div>
    <Pagination />
  </div>
</template>
