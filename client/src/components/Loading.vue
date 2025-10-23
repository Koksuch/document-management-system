<script setup lang="ts"></script>
<template>
  <div class="container"></div>
</template>

<style scoped>
.container {
  --uib-size: 60px;
  --uib-color: oklch(62.3% 0.214 259.815);
  --uib-speed: 1.2s;
  --uib-stroke: 6px;
  --mask-size: calc(var(--uib-size) / 2 - var(--uib-stroke));
  position: relative;
  display: flex;
  align-items: center;
  justify-content: flex-start;
  height: var(--uib-size);
  width: var(--uib-size);
  -webkit-mask: radial-gradient(
    circle var(--mask-size),
    transparent 99%,
    #000 100%
  );
  mask: radial-gradient(circle var(--mask-size), transparent 99%, #000 100%);
  background-image: conic-gradient(transparent 25%, var(--uib-color));
  animation: spin calc(var(--uib-speed)) linear infinite;
  border-radius: 50%;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
