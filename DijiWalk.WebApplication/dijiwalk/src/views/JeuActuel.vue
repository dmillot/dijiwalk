<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar class="bg-toolbar">
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/' " />
                <div class="row full-width justify-center">
                    <img src="https://storage.cloud.google.com/dijiwalk-test/logo-text.png" style="max-height:50px;" class="q-my-sm" />
                </div>
            </q-toolbar>
        </q-header>

        <h5 class="text-bold text-left">Jeu en cours</h5>
        <q-card class=" full-height q-px-xl q-py-lg">
            <q-card-section class="flex column flex">
                <div class="q-pa-md">
                    <q-carousel v-if="anyGame" v-model="slide"
                                transition-prev="scale"
                                transition-next="scale"
                                swipeable
                                @transition="transitionSlide"
                                animated
                                control-text-color="white"
                                navigation
                                padding
                                arrows
                                navigation-position="top"
                                height="400px"
                                class="bg-primary text-white shadow-1 rounded-borders">
                        <q-carousel-slide v-for="game in gamesActifs" v-bind:key="game.id" :name="game.id" class="column no-wrap flex-center" :img-src="game.route.routeSteps[0].step.validation">
                            <div class="absolute-bottom custom-caption">
                                <div class="text-h2">Jeu {{game.id}}</div>
                                <div class="row justify-center">
                                    <div class="text-h4 text-light">{{game.route.name}}</div>
                                    <q-btn class="q-mx-md text-bold" :color="game.statusColor" size="15px" flat icon="fas fa-circle" :label="game.statusLabel" @click="toValidation" />
                                </div>
                                <div class="row justify-center">
                                    <div class="text-subtitle2 q-mx-sm"><b>Organisateur:</b> {{game.organizer.firstName}} {{game.organizer.lastName}}</div>
                                    <q-separator vertical color="white" />
                                    <div class="text-subtitle2 q-mx-sm"><b>Date de création:</b> {{game.creationDate | formatDate}}</div>
                                </div>
                            </div>
                        </q-carousel-slide>
                    </q-carousel>
                </div>
            </q-card-section>
            <h4 v-if="!anyGame" class="no-margin  text-grey-5">
                PAS DE JEU EN COURS !
            </h4>
            <q-card-section :disabled="gameSelected===null" class="flex column flex-center">
                <div class="row flex-center full-width justify-center q-mt-lg q-col-gutter-xl">

                    <Card link="jeuActif" :disabled="gameSelected===null" v-bind:params="{id: gameSelected != null ? gameSelected : null}" icon="fas fa-dice" title="Jeu" description="Page de visualisation des information général du jeu" />
                    <Card link="validation" :disabled="gameSelected===null" v-bind:params="{id: gameSelected != null ? gameSelected : null}" icon="fas fa-clipboard-check" title="Validation" description="Page de gestion des problèmes de validation" />
                    <Card link="" :disabled="gameSelected===null" icon="fas fa-comments" title="Chat" description="Page de chat avec les participants" />

                </div>
            </q-card-section>
        </q-card>
    </q-page>
</template>

<script>
    // @ is an alias to /src
    import Card from '@/components/Card.vue'
    import GameDataService from "@/services/GameDataService"
    import moment from "moment"

    export default {

        name: 'jeuActuel',

        data: function () {
            return {
                slide: null,
                gamesActifs: null,
                gameSelected: null,
                anyGame: true
            }
        },

        components: {
            Card
        },

        created() {
            this.getActualGames();
        },
        filters: {
            formatDate: function (value) {
                if (!value) return ''
                return moment(String(value)).format('DD/MM/YYYY HH:mm')
            }
        },

        methods: {
            toValidation() {
               this.$router.push({ name: "validation", params: { idJeu: this.slide } });
            },
            getActualGames() {
                this.$q.loading.show()
                if (this.gamesActifs === null) {
                    GameDataService.getAllActifs().then(response => {
                        this.$q.loading.hide()
                        if (response.data != null && response.data.length != 0) {
                            this.gamesActifs = response.data;
                            this.gamesActifs.map(function (el) {
                                el.statusColor = "secondary";
                                el.statusLabel = "ÉQUIPES OK";
                                for (var i = 0; i < el.teamRoutes.length; i++) {
                                    if (el.teamRoutes[i].askValidationDate != null && el.teamRoutes[i].validate == false) {
                                        el.statusColor = "negative";
                                         el.statusLabel = "ÉQUIPES BLOQUÉES";
                                        break;
                                    }
                                }
                            })
                            this.slide = response.data[0].id
                        } else {
                            this.anyGame = false
                            this.slide = null
                        }
                    }).catch();
                }
            },

            transitionSlide(newGame) {
                this.gameSelected = newGame
            }
        }
    }
</script>
<style scoped>
    .custom-caption {
        text-align: center;
        padding: 12px;
        color: white;
        background-color: rgba(0, 0, 0, .5);
    }
</style>

