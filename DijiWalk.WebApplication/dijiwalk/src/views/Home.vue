<template>
  <q-page class="flex flex-center q-px-xl">
    <div class="home">

      <h5 class="text-bold">Accueil</h5>

      <div class="row justify-center q-col-gutter-xl">

        <Card link="/" icon="fas fa-route" title="Parcours" description="Page de gestion d'un parcours" />
        <Card link="/" icon="fas fa-user" title="Participants" description="Page de gestion des participants" />
        <Card link="/" icon="fas fa-users" title="Équipes" description="Page de gestion des équipes ainsi que la définition du leader du groupe" />
        <Card link="/" icon="fas fa-map-marker-alt" title="Étapes" description="Page de gestion des étapes" />
        <Card link="/" icon="fas fa-bus" title="Moyens de transport" description="Page de gestion des moyens de transport" />
        <Card link="/" icon="fas fa-hand-lizard" title="Jeux" description="Page de gestion des jeux" />

      </div>
    </div>
  </q-page>
</template>

<script>
// @ is an alias to /src
import Card from '@/components/Card.vue'

export default {
  name: 'home',
  components: {
    Card
  }
}
</script>
