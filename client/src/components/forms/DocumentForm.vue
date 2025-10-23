<script setup lang="ts">
import { getDocumentById } from "@/api/documentsApi"
import { useDocumentStore } from "@/store/documentStore"
import { ref, computed, onMounted, inject } from "vue"
import { useToast } from "vue-toastification"

const props = defineProps<{
  id?: number | null
}>()

const closeForm = inject("closeDocumentForm") as () => void

const store = useDocumentStore()
const toast = useToast()

const isEdit = computed(() => !!props.id)
const isLoading = ref(false)

const form = ref({
  type: "",
  date: "",
  firstName: "",
  lastName: "",
  city: "",
})

const errors = ref({
  type: "",
  date: "",
  firstName: "",
  lastName: "",
  city: "",
})

function validate() {
  let valid = true
  Object.keys(errors.value).forEach(
    (key) => (errors.value[key as keyof typeof errors.value] = ""),
  )

  if (!form.value.type.trim()) {
    errors.value.type = "Type is required"
    valid = false
  }
  if (!form.value.date) {
    errors.value.date = "Date is required"
    valid = false
  }
  if (!form.value.firstName.trim()) {
    errors.value.firstName = "First name is required"
    valid = false
  }
  if (!form.value.lastName.trim()) {
    errors.value.lastName = "Last name is required"
    valid = false
  }
  if (!form.value.city.trim()) {
    errors.value.city = "City is required"
    valid = false
  }

  return valid
}

async function handleSubmit() {
  if (!validate()) return

  isLoading.value = true
  const payload = {
    type: form.value.type.trim(),
    date: new Date(form.value.date),
    firstName: form.value.firstName.trim(),
    lastName: form.value.lastName.trim(),
    city: form.value.city.trim(),
  }

  try {
    if (isEdit.value && props.id) {
      await store.updateDocument(props.id, payload)
      toast.success("Document updated successfully")
      closeForm()
    } else {
      await store.createDocument(payload)
      toast.success("Document created successfully")
      closeForm()
    }
  } catch (err) {
    toast.error(err.response?.data?.message || "Error while saving document")
  } finally {
    isLoading.value = false
  }
}

onMounted(async () => {
  if (isEdit.value && props.id) {
    try {
      isLoading.value = true
      const { data } = await getDocumentById(props.id)
      form.value = {
        type: data.type,
        date: data.date.split("T")[0],
        firstName: data.firstName,
        lastName: data.lastName,
        city: data.city,
      }
    } catch (err) {
      toast.error(err.response?.data?.message || "Failed to load document data")
    } finally {
      isLoading.value = false
    }
  }
})
</script>

<template>
  <div class="mx-auto mt-4 w-full max-w-lg rounded-2xl bg-white p-4 shadow-md">
    <h2 class="mb-4 text-xl font-semibold">
      {{ isEdit ? "Edit Document" : "Create New Document" }}
    </h2>

    <form @submit.prevent="handleSubmit" class="flex flex-col gap-3">
      <div>
        <label class="mb-1 block text-sm font-medium">Type</label>
        <input
          v-model="form.type"
          type="text"
          class="w-full rounded-lg border p-2"
          :class="{ 'border-red-500': errors.type }"
        />
        <p v-if="errors.type" class="text-sm text-red-500">
          {{ errors.type }}
        </p>
      </div>

      <div>
        <label class="mb-1 block text-sm font-medium">Date</label>
        <input
          v-model="form.date"
          type="date"
          class="w-full rounded-lg border p-2"
          :class="{ 'border-red-500': errors.date }"
        />
        <p v-if="errors.date" class="text-sm text-red-500">
          {{ errors.date }}
        </p>
      </div>

      <div>
        <label class="mb-1 block text-sm font-medium">First Name</label>
        <input
          v-model="form.firstName"
          type="text"
          class="w-full rounded-lg border p-2"
          :class="{ 'border-red-500': errors.firstName }"
        />
        <p v-if="errors.firstName" class="text-sm text-red-500">
          {{ errors.firstName }}
        </p>
      </div>

      <div>
        <label class="mb-1 block text-sm font-medium">Last Name</label>
        <input
          v-model="form.lastName"
          type="text"
          class="w-full rounded-lg border p-2"
          :class="{ 'border-red-500': errors.lastName }"
        />
        <p v-if="errors.lastName" class="text-sm text-red-500">
          {{ errors.lastName }}
        </p>
      </div>

      <div>
        <label class="mb-1 block text-sm font-medium">City</label>
        <input
          v-model="form.city"
          type="text"
          class="w-full rounded-lg border p-2"
          :class="{ 'border-red-500': errors.city }"
        />
        <p v-if="errors.city" class="text-sm text-red-500">
          {{ errors.city }}
        </p>
      </div>

      <div class="mt-3 flex justify-end gap-2">
        <button
          type="button"
          class="cursor-pointer rounded bg-gray-300 px-3 py-2 transition not-disabled:hover:bg-gray-400"
          @click="closeForm()"
        >
          Cancel
        </button>
        <button
          type="submit"
          class="cursor-pointer rounded bg-blue-500 px-4 py-2 text-white transition not-disabled:hover:bg-blue-600"
          :disabled="isLoading"
        >
          {{ isEdit ? "Update" : "Create" }}
        </button>
      </div>
    </form>
  </div>
</template>
