import { createApp } from "vue"
import "./style.css"
import App from "./App.vue"
import { addIcons, OhVueIcon } from "oh-vue-icons"
import {
  BiArrowUp,
  BiPlusSquare,
  MdArrowdropdown,
  MdDeleteRound,
  MdModeeditoutlineOutlined,
} from "oh-vue-icons/icons"
import { createPinia } from "pinia"
import Toast, { POSITION } from "vue-toastification"
import type { PluginOptions } from "vue-toastification"
import "vue-toastification/dist/index.css"

addIcons(
  MdArrowdropdown,
  MdDeleteRound,
  MdModeeditoutlineOutlined,
  BiArrowUp,
  BiPlusSquare,
)

const options: PluginOptions = {
  transition: "Vue-Toastification__fade",
  maxToasts: 20,
  newestOnTop: true,
  position: POSITION.BOTTOM_RIGHT,
}

const pinia = createPinia()
const app = createApp(App)

app.use(pinia)
app.use(Toast, options)
app.component("v-icon", OhVueIcon)
app.mount("#app")
