<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar class="bg-toolbar">
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-power-off" @click="onDisconnected" />
                <div class="row full-width justify-center">
                    <img src="https://storage.cloud.google.com/dijiwalk-test/logo-text.png" style="max-height:50px;" class="q-my-sm" />
                </div>
                
            </q-toolbar>
        </q-header>
        <div class="row flex-center full-width justify-center q-col-gutter-xl q-mt-lg">

            <div class="col-12 col-grow">
                <q-card ripple="black" @click="onJeuxActuels" link="/jeuActuel" class="my-card full-height cursor-pointer row justify-center items-center first-card q-py-md">
                    <q-icon name="fas fa-chess" style="font-size: 4em"/>
                    <p class="text-bold q-ml-md q-ma-none text-h4">Jeux Actuels</p>
                </q-card>
            </div>
            <Card link="/jeu" icon="fas fa-dice" title="Jeux" description="Page de gestion des jeux" />
            <Card link="/parcours" icon="fas fa-route" title="Parcours" description="Page de gestion d'un parcours" />
            <Card link="/etape" icon="fas fa-map-marker-alt" title="Etapes" description="Page de gestion des etapes" />
            <Card link="/equipe" icon="fas fa-users" title="Equipes" description="Page de gestion des equipes ainsi que la definition du leader du groupe" />
            <Card link="/participant" icon="fas fa-user" title="Participants" description="Page de gestion des participants" />
            <Card link="/" :disabled="true" icon="fas fa-bus" title="Moyens de transport" description="Page de gestion des moyens de transport" />


        </div>

    </q-page>
</template>

<script>
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

