<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-power-off" @click="onDisconnected" />
                <q-toolbar-title>DijiWalk</q-toolbar-title>
            </q-toolbar>
        </q-header>

        <h5 class="text-bold text-left">Accueil</h5>

        <div class="row flex-center full-width justify-center q-col-gutter-xl">

            <div class="col-12 col-grow">
                <q-card @click="onJeuxActuels" link="/jeuActuel" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <p class="text-white text-bold q-ma-none text-h4">JEU(X) ACTUEL(S)</p>
                </q-card>
            </div>
            <Card link="/parcours" icon="fas fa-route" title="Parcours" description="Page de gestion d'un parcours" />
            <Card link="/participant" icon="fas fa-user" title="Participants" description="Page de gestion des participants" />
            <Card link="/equipe" icon="fas fa-users" title="Equipes" description="Page de gestion des equipes ainsi que la definition du leader du groupe" />
            <Card link="/etape" icon="fas fa-map-marker-alt" title="Etapes" description="Page de gestion des etapes" />
            <Card link="/" :disabled="true" icon="fas fa-bus" title="Moyens de transport" description="Page de gestion des moyens de transport" />
            <Card link="/jeu" icon="fas fa-hand-lizard" title="Jeux" description="Page de gestion des jeux" />

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
        },
        methods: {
            onJeuxActuels() {
                 this.$router.push("/jeuActuel")
            },
            onDisconnected() {
                this.$q.cookies.remove('JWTToken')
                this.$q.sessionStorage.remove("connectedOrganizer")
                this.$router.push("/login")
            }
        }
}
</script>
