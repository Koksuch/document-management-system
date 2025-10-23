<script setup lang="ts">
import {
  createDocumentItem,
  getDocumentItemById,
  updateDocumentItemById,
} from "@/api/documentItemsApi"
import { ref, computed, onMounted } from "vue"

import { useToast } from "vue-toastification"

const props = defineProps<{
  id?: number | null
  documentId: number
  close: () => void
  reloadData: () => void
}>()

const toast = useToast()

const isEdit = computed(() => !!props.id)
const isLoading = ref(false)

const form = ref({
  ordinal: 1,
  product: "",
  quantity: 1,
  price: 0,
  taxRate: 0,
})

const errors = ref({
  ordinal: "",
  product: "",
  quantity: "",
  price: "",
  taxRate: "",
})

function validate() {
  let valid = true
  Object.keys(errors.value).forEach(
    (k) => (errors.value[k as keyof typeof errors.value] = ""),
  )

  if (!form.value.ordinal || form.value.ordinal < 1) {
    errors.value.ordinal = "Ordinal must be at least 1"
    valid = false
  }
  if (!form.value.product.trim()) {
    errors.value.product = "Product is required"
    valid = false
  }
  if (!form.value.quantity || form.value.quantity < 1) {
    errors.value.quantity = "Quantity must be at least 1"
    valid = false
  }
  if (form.value.price < 0) {
    errors.value.price = "Price cannot be negative"
    valid = false
  }
  if (form.value.taxRate < 0 || form.value.taxRate > 100) {
    errors.value.taxRate = "Tax rate must be between 0 and 100"
    valid = false
  }

  return valid
}

async function handleSubmit() {
  if (!validate()) return
  const payload = {
    documentId: props.documentId,
    ordinal: form.value.ordinal,
    product: form.value.product.trim(),
    quantity: form.value.quantity,
    price: form.value.price,
    taxRate: form.value.taxRate,
  }

  try {
    isLoading.value = true

    if (isEdit.value && props.id) {
      await updateDocumentItemById(props.id, payload)
      toast.success("Item updated successfully")
      await props.reloadData()
      props.close()
    } else {
      await createDocumentItem(props.documentId, payload)
      toast.success("Item added successfully")
      await props.reloadData()
    }
  } catch (err) {
    console.error(err)
    toast.error("Error while saving document item")
  } finally {
    isLoading.value = false
  }
}

onMounted(async () => {
  if (isEdit.value && props.id) {
    try {
      isLoading.value = true
      const { data } = await getDocumentItemById(props.id)
      form.value = {
        ordinal: data.ordinal,
        product: data.product,
        quantity: data.quantity,
        price: data.price,
        taxRate: data.taxRate,
      }
    } catch {
      toast.error("Failed to load item data")
    } finally {
      isLoading.value = false
    }
  }
})
</script>

<template>
  <form
    @submit.prevent="handleSubmit"
    class="flex flex-wrap items-end gap-3 rounded-xl bg-white p-3"
  >
    <div class="flex w-20 flex-col">
      <label class="mb-1 text-xs font-medium">Ordinal</label>
      <input
        v-model.number="form.ordinal"
        type="number"
        min="1"
        class="w-full rounded-lg border p-2 text-sm"
        :class="{ 'border-red-500': errors.ordinal }"
      />
      <p v-if="errors.ordinal" class="text-xs text-red-500">
        {{ errors.ordinal }}
      </p>
    </div>

    <div class="flex min-w-[120px] flex-1 flex-col">
      <label class="mb-1 text-xs font-medium">Product</label>
      <input
        v-model="form.product"
        type="text"
        class="w-full rounded-lg border p-2 text-sm"
        :class="{ 'border-red-500': errors.product }"
      />
      <p v-if="errors.product" class="text-xs text-red-500">
        {{ errors.product }}
      </p>
    </div>

    <div class="flex w-24 flex-col">
      <label class="mb-1 text-xs font-medium">Quantity</label>
      <input
        v-model.number="form.quantity"
        type="number"
        min="1"
        class="w-full rounded-lg border p-2 text-sm"
        :class="{ 'border-red-500': errors.quantity }"
      />
      <p v-if="errors.quantity" class="text-xs text-red-500">
        {{ errors.quantity }}
      </p>
    </div>

    <div class="flex w-28 flex-col">
      <label class="mb-1 text-xs font-medium">Price</label>
      <input
        v-model.number="form.price"
        type="number"
        min="0"
        step="0.01"
        class="w-full rounded-lg border p-2 text-sm"
        :class="{ 'border-red-500': errors.price }"
      />
      <p v-if="errors.price" class="text-xs text-red-500">
        {{ errors.price }}
      </p>
    </div>

    <div class="flex w-28 flex-col">
      <label class="mb-1 text-xs font-medium">Tax %</label>
      <input
        v-model.number="form.taxRate"
        type="number"
        min="0"
        max="100"
        step="1"
        class="w-full rounded-lg border p-2 text-sm"
        :class="{ 'border-red-500': errors.taxRate }"
      />
      <p v-if="errors.taxRate" class="text-xs text-red-500">
        {{ errors.taxRate }}
      </p>
    </div>

    <div class="flex gap-2">
      <button
        type="button"
        class="rounded bg-gray-300 px-3 py-2 text-sm transition hover:bg-gray-400"
        @click="props.close()"
      >
        Cancel
      </button>
      <button
        type="submit"
        class="rounded bg-blue-600 px-4 py-2 text-sm text-white transition hover:bg-blue-700"
        :disabled="isLoading"
      >
        {{ isEdit ? "Update" : "Add" }}
      </button>
    </div>
  </form>
</template>
