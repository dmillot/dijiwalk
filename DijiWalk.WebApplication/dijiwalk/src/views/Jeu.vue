<template>
  <div class="q-pa-md row items-start q-gutter-md">
      <q-card @click="onClick" class="my-card full-height">
        <q-img src="https://cdn.quasar.dev/img/parallax2.jpg"
              basic>
          <div class="absolute-bottom text-subtitle2 text-center">
            Title
          </div>
        </q-img>
      </q-card>
  </div>
</template>

<script>
export default {
  name: 'jeu',
  methods: {
    onClick () {
      this.$router.push({ name: 'home' })
    }
  }
}
</script>

<style scoped>
.my-card {
    width: 100%; max-width: 250px;
    cursor: pointer;
}

.my-card:hover::before {
    content: "";
    position: absolute;
    z-index: 10;
    border-radius: 4px;
    background-color: #000000;
    opacity: 0.6;
    width: 100%;
    height: 100%;
}

.my-card:hover::after {
    content: "";
}

.link-none {
    color: #000000;
    text-decoration: none;
}
</style>