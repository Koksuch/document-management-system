<script setup lang="ts">
import { useDocumentStore } from "@/store/documentStore"

const store = useDocumentStore()

const prevPage = () => {
  if (store.page > 1) {
    store.page -= 1
  }
}

const nextPage = () => {
  if (store.page < store.totalPages) {
    store.page += 1
  }
}
</script>

<template>
  <div class="flex items-center justify-between p-4">
    <button
      class="cursor-pointer rounded bg-blue-500 px-4 py-2 text-white transition-colors not-disabled:hover:bg-blue-600 disabled:opacity-50"
      @click="prevPage"
      :disabled="store.page === 1"
    >
      Previous
    </button>
    <span>Page {{ store.page }} of {{ store.totalPages }}</span>
    <div class="flex items-center gap-4">
      <label class="flex items-center gap-2">
        <span>Items per page:</span>
        <select
          v-model.number="store.pageSize"
          @change="store.page = 1"
          class="rounded border px-2 py-1"
        >
          <option :value="10">10</option>
          <option :value="15">15</option>
          <option :value="20">20</option>
          <option :value="50">50</option>
          <option :value="100">100</option>
          <option :value="store.totalCount">ALL</option>
        </select>
      </label>
    </div>
    <button
      class="cursor-pointer rounded bg-blue-500 px-4 py-2 text-white transition-colors not-disabled:hover:bg-blue-600 disabled:opacity-50"
      @click="nextPage"
      :disabled="store.page === store.totalPages"
    >
      Next
    </button>
  </div>
</template>
