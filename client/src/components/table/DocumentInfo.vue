<script setup lang="ts">
import { useDocumentStore } from "@/store/documentStore"
import type { DocumentType } from "@/types/types"
import { inject, ref } from "vue"
import ItemsInfo from "./ItemsInfo.vue"

defineProps<{
  document: DocumentType
}>()

const openForm = inject("openEditDocumentForm") as (id: number) => void
const store = useDocumentStore()
const isVisible = ref(false)
</script>

<template>
  <div
    class="grid w-full grid-cols-[0.5fr_0.5fr_1fr_1fr_1fr_1fr_1fr_0.5fr] px-4 py-3"
  >
    <button
      @click="isVisible = !isVisible"
      :class="`w-20 cursor-pointer place-self-start fill-gray-500 transition-all duration-300 hover:fill-blue-500 ${!isVisible ? '' : 'rotate-180'}`"
    >
      <v-icon name="md-arrowdropdown" scale="1.3" class="fill-inherit" />
    </button>
    <div>{{ document.id }}</div>
    <div>{{ document.type }}</div>
    <div>{{ document.date.toString().split("T")[0] }}</div>
    <div>{{ document.firstName }}</div>
    <div>{{ document.lastName }}</div>
    <div>{{ document.city }}</div>
    <div class="flex gap-3">
      <div
        @click="openForm(document.id)"
        class="cursor-pointer fill-orange-400 p-1 hover:animate-bounce"
      >
        <v-icon name="md-modeeditoutline-outlined" class="fill-inherit" />
      </div>
      <div
        class="cursor-pointer fill-red-500 p-1 hover:animate-bounce"
        @click="store.deleteDocument(document.id)"
      >
        <v-icon name="md-delete-round" class="fill-inherit" />
      </div>
    </div>
  </div>
  <div v-if="isVisible" class="border-t border-b border-gray-300">
    <ItemsInfo :documentId="document.id" />
  </div>
</template>
