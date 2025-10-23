<script setup lang="ts">
defineProps<{
  text: string
  click: () => void
}>()
</script>

<template>
  <button
    @click="click"
    class="cursor-pointer rounded bg-green-500 px-4 py-2 text-white transition-colors hover:bg-green-600"
  >
    <v-icon name="bi-plus-square" />
    {{ text }}
  </button>
</template>
