<script setup lang="ts">
import { onMounted, ref } from "vue"
import type { DocumentItemType } from "@/types/types"
import {
  deleteDocumentItemById,
  getDocumentItemsByDocumentId,
} from "@/api/documentItemsApi"
import { useToast } from "vue-toastification"
import AddNewBtn from "../buttons/AddNewBtn.vue"
import ItemForm from "../forms/ItemForm.vue"
import Loading from "../Loading.vue"

const props = defineProps<{
  documentId: number
}>()

const toast = useToast()
const items = ref<DocumentItemType[]>([])
const isLoading = ref(false)
const isAddFormVisible = ref(false)
const editingItemId = ref<number | null>(null)

const openAddForm = () => {
  isAddFormVisible.value = true
}

const closeAddForm = () => {
  isAddFormVisible.value = false
}

const openEditForm = (id: number) => {
  editingItemId.value = id
}
const closeEditForm = () => {
  editingItemId.value = null
}

const removeItem = async (id: number) => {
  try {
    await deleteDocumentItemById(id)
    await fetchItems()
    toast.success("Item deleted successfully")
  } catch (error) {
    toast.error(error.response?.data?.message || "Failed to delete item")
  } finally {
  }
}

const fetchItems = async () => {
  try {
    isLoading.value = true
    const res = await getDocumentItemsByDocumentId(props.documentId)
    console.log("Fetched document items:", res.data)
    items.value = res.data
  } catch (error) {
    console.error("Error fetching document items:", error)
  } finally {
    isLoading.value = false
  }
}

onMounted(async () => {
  await fetchItems()
})
</script>
<template>
  <div v-if="isLoading" class="flex justify-center px-4 py-3">
    <Loading />
  </div>
  <div v-else-if="items.length === 0">
    <p class="flex justify-center px-4 py-3 font-semibold">No items found.</p>
  </div>
  <div
    v-else
    class="grid grid-cols-[0.5fr_1fr_1fr_1fr_1fr_0.5fr] px-4 py-3 [&>div]:py-2"
  >
    <!-- <div>ID</div> -->
    <div class="font-semibold">Ordinal</div>
    <div class="font-semibold">Product</div>
    <div class="font-semibold">Quantity</div>
    <div class="font-semibold">Price</div>
    <div class="font-semibold">Tax Rate</div>
    <div class="font-semibold">Actions</div>
    <div
      v-for="item in items"
      class="col-span-6 grid grid-cols-[0.5fr_1fr_1fr_1fr_1fr_0.5fr] border-t-2 border-dashed border-gray-300"
      :key="item.id"
    >
      <!-- <div>{{ item.id }}</div> -->
      <div>{{ item.ordinal }}</div>
      <div>{{ item.product }}</div>
      <div>{{ item.quantity }}</div>
      <div>{{ item.price }}</div>
      <div>{{ item.taxRate }}</div>
      <div class="flex gap-3">
        <div
          @click="openEditForm(item.id)"
          class="cursor-pointer fill-orange-400 p-1 hover:animate-bounce"
        >
          <v-icon name="md-modeeditoutline-outlined" class="fill-inherit" />
        </div>
        <div
          class="cursor-pointer fill-red-500 p-1 hover:animate-bounce"
          @click="removeItem(item.id)"
        >
          <v-icon name="md-delete-round" class="fill-inherit" />
        </div>
      </div>
      <div class="col-span-6">
        <ItemForm
          v-if="editingItemId === item.id"
          :documentId="documentId"
          :id="item.id"
          :close="closeEditForm"
          :reloadData="fetchItems"
        />
      </div>
    </div>
  </div>
  <div class="flex justify-center py-2">
    <AddNewBtn :click="openAddForm" text="Add New Item" />
  </div>
  <div>
    <ItemForm
      v-if="isAddFormVisible"
      :documentId="documentId"
      :close="closeAddForm"
      :reloadData="fetchItems"
    />
  </div>
</template>
