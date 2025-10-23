<script setup lang="ts">
import { inject } from "vue"

const openForm = inject("openImportForm") as () => void
</script>

<template>
  <button
    @click="openForm"
    class="cursor-pointer rounded bg-blue-500 px-4 py-2 text-white transition-colors hover:bg-blue-600"
  >
    Import Document
  </button>
</template>
