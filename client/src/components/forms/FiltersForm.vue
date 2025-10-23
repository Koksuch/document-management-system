<script setup lang="ts">
import { useDocumentStore } from "@/store/documentStore"

const store = useDocumentStore()
</script>

<template>
  <form>
    <div class="flex items-end justify-center gap-2 pt-3">
      <input
        v-model="store.search"
        placeholder="Search text fields"
        class="rounded border p-2"
      />

      <div class="flex flex-col">
        <label for="dateFrom">From</label>
        <input
          id="dateFrom"
          type="date"
          v-model="store.dateFrom"
          class="rounded border p-2"
        />
      </div>

      <div class="flex flex-col">
        <label for="dateTo">To</label>
        <input
          id="dateTo"
          type="date"
          v-model="store.dateTo"
          class="rounded border p-2"
        />
      </div>

      <button
        type="submit"
        @click.prevent="store.fetchDocuments"
        class="cursor-pointer rounded bg-blue-500 px-3 py-2 text-white transition-colors not-disabled:hover:bg-blue-600 disabled:opacity-50"
        :disabled="store.isLoading"
      >
        Search
      </button>
      <button
        type="button"
        @click="store.clearFilters"
        class="cursor-pointer rounded bg-gray-300 px-3 py-2 text-black transition-colors not-disabled:hover:bg-gray-400 disabled:opacity-50"
        :disabled="store.isLoading"
      >
        Clear filters
      </button>
    </div>
  </form>
</template>
