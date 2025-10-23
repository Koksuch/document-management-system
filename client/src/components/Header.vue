<script setup lang="ts">
import { inject } from "vue"
import AddNewBtn from "./buttons/AddNewBtn.vue"
import ImportBtn from "./buttons/ImportBtn.vue"

const openForm = inject("openDocumentForm") as () => void
</script>

<template>
  <div
    class="flex items-center justify-between border-b border-gray-300 pt-5 pb-7"
  >
    <h1 class="text-3xl">Document Management System</h1>
    <div class="flex items-center gap-4">
      <AddNewBtn :click="openForm" text="Add New Document" />
      <ImportBtn />
    </div>
  </div>
</template>
