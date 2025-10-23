<script setup lang="ts">
import { importDataToDb } from "@/api/importApi"
import { useDocumentStore } from "@/store/documentStore"
import { inject, ref } from "vue"
import { useToast } from "vue-toastification"

const closeForm = inject("closeImportForm") as () => void
const store = useDocumentStore()
const files = ref<File[]>([])
const toast = useToast()
const loading = ref(false)

const handleFileChange = (e: Event) => {
  files.value = Array.from((e.target as HTMLInputElement).files || [])
}

const upload = async () => {
  if (files.value.length !== 2) {
    toast.error("Please select 2 files to upload")
    return
  }

  const formData = new FormData()
  files.value.forEach((f) => formData.append("files", f))

  try {
    loading.value = true
    await importDataToDb(formData)
    await store.fetchDocuments()
    toast.success("Files uploaded successfully")
  } catch (err) {
    console.error("Error uploading files:", err)
    toast.error(err.response?.data?.message || "Error uploading files")
  } finally {
    loading.value = false
    closeForm()
  }
}
</script>

<template>
  <div class="mt-4 flex items-center justify-center">
    <div class="space-y-2 rounded-2xl bg-white p-6 shadow-lg">
      <h2 class="text-lg font-semibold">Import CSV</h2>
      <p>Select two .csv files to upload:</p>

      <input
        class="cursor-pointer"
        type="file"
        multiple
        @change="handleFileChange"
        accept=".csv"
      />

      <div class="flex justify-end gap-2">
        <button
          @click="closeForm"
          class="cursor-pointer rounded-lg bg-gray-200 px-4 py-2 transition-colors hover:bg-gray-300"
        >
          Cancel
        </button>
        <button
          :disabled="loading"
          @click="upload"
          class="cursor-pointer rounded-lg bg-blue-500 px-4 py-2 text-white transition-colors hover:bg-blue-600 disabled:opacity-50"
        >
          Send
        </button>
      </div>
    </div>
  </div>
</template>
