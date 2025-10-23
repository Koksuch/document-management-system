<script setup lang="ts">
import { onMounted } from "vue"
import DocumentInfo from "./DocumentInfo.vue"
import type { DocumentsSortFieldType } from "@/types/types"
import { useDocumentStore } from "@/store/documentStore"
import { DocumentsSortFields } from "@/helpers/enums"
import Loading from "../Loading.vue"

const store = useDocumentStore()

const setSortingParams = (sortBy: DocumentsSortFieldType) => {
  store.sortBy = sortBy
  store.sortDesc = !store.sortDesc
}

onMounted(async () => {
  await store.fetchDocuments()
  console.log(store.documents)
})
</script>

<template>
  <div>
    <div
      class="grid grid-cols-[0.5fr_0.5fr_1fr_1fr_1fr_1fr_1fr_0.5fr] rounded-t-lg bg-gray-300 px-4 py-3 font-medium"
    >
      <div></div>
      <div
        @click="setSortingParams(DocumentsSortFields.ID)"
        class="cursor-pointer transition-colors hover:text-emerald-500"
      >
        ID
        <v-icon
          name="bi-arrow-up"
          :class="`transition-all ${store.sortBy === DocumentsSortFields.ID ? 'fill-emerald-500' : ''} ${store.sortDesc && store.sortBy === DocumentsSortFields.ID ? 'rotate-180' : ''}`"
        />
      </div>
      <div
        @click="setSortingParams(DocumentsSortFields.TYPE)"
        class="cursor-pointer transition-colors hover:text-emerald-500"
      >
        Type
        <v-icon
          name="bi-arrow-up"
          :class="`transition-all ${store.sortBy === DocumentsSortFields.TYPE ? 'fill-emerald-500' : ''} ${store.sortDesc && store.sortBy === DocumentsSortFields.TYPE ? 'rotate-180' : ''}`"
        />
      </div>
      <div
        @click="setSortingParams(DocumentsSortFields.DATE)"
        class="cursor-pointer transition-colors hover:text-emerald-500"
      >
        Date
        <v-icon
          name="bi-arrow-up"
          :class="`transition-all ${store.sortBy === DocumentsSortFields.DATE ? 'fill-emerald-500' : ''} ${store.sortDesc && store.sortBy === DocumentsSortFields.DATE ? 'rotate-180' : ''}`"
        />
      </div>
      <div
        @click="setSortingParams(DocumentsSortFields.FIRST_NAME)"
        class="cursor-pointer transition-colors hover:text-emerald-500"
      >
        First Name
        <v-icon
          name="bi-arrow-up"
          :class="`transition-all ${store.sortBy === DocumentsSortFields.FIRST_NAME ? 'fill-emerald-500' : ''} ${store.sortDesc && store.sortBy === DocumentsSortFields.FIRST_NAME ? 'rotate-180' : ''}`"
        />
      </div>
      <div
        @click="setSortingParams(DocumentsSortFields.LAST_NAME)"
        class="cursor-pointer transition-colors hover:text-emerald-500"
      >
        Last Name
        <v-icon
          name="bi-arrow-up"
          :class="`transition-all ${store.sortBy === DocumentsSortFields.LAST_NAME ? 'fill-emerald-500' : ''} ${store.sortDesc && store.sortBy === DocumentsSortFields.LAST_NAME ? 'rotate-180' : ''}`"
        />
      </div>
      <div
        @click="setSortingParams(DocumentsSortFields.CITY)"
        class="cursor-pointer transition-colors hover:text-emerald-500"
      >
        City
        <v-icon
          name="bi-arrow-up"
          :class="`transition-all ${store.sortBy === DocumentsSortFields.CITY ? 'fill-emerald-500' : ''} ${store.sortDesc && store.sortBy === DocumentsSortFields.CITY ? 'rotate-180' : ''}`"
        />
      </div>
      <div>Actions</div>
    </div>
    <div class="flex justify-center px-4 py-3" v-if="store.isLoading">
      <Loading />
    </div>
    <DocumentInfo
      v-else
      v-for="document in store.documents"
      :key="document.id"
      :document="document"
    />
  </div>
</template>
