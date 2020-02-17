<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/' " />
                <q-toolbar-title>DijiWalk</q-toolbar-title>
            </q-toolbar>
        </q-header>

        <h5 class="text-bold text-left">Jeu en cours</h5>
        <q-card class=" full-height q-px-xl q-py-lg">
            <q-card-section class="flex column flex">
                <div class="q-pa-md">
                    <q-carousel v-model="slide"
                                transition-prev="scale"
                                transition-next="scale"
                                swipeable
                                @transition="transitionSlide(game)"
                                animated
                                control-color="white"
                                navigation
                                padding
                                arrows
                                height="300px"
                                class="bg-primary text-white shadow-1 rounded-borders">
                        <q-carousel-slide v-for="game in gamesActifs" v-bind:key="game.id" :name="game.id" class="column no-wrap flex-center">
                            <h2>Jeu {{game.id}}</h2>
                            <div class="q-mt-md text-center column">
                                <label>{{game.route.description}}</label>
                                <label>Organisateur: {{game.organizer.firstName}} {{game.organizer.lastName}} </label>
                                <label>Date de création: {{game.creationDate | formatDate}}</label>
                            </div>
                            <div class="row q-mb-lg" style="align-items:center;">
                                <label>État de validation: </label>

                            </div>
                        </q-carousel-slide>
                    </q-carousel>
                </div>
            </q-card-section>
            <q-card-section :disabled="gameSelected===null" class="flex column flex-center">
                <div class="row flex-center full-width justify-center q-mt-lg q-col-gutter-xl">

                    <Card link="jeuActif" :disabled="gameSelected===null" v-bind:params="{id: idGame}" icon="fas fa-dice" title="Jeu" description="Page de visualisation des information général du jeu" />
                    <Card link="" :disabled="gameSelected===null" icon="fas fa-clipboard-check" title="Validation" description="Page de gestion des problèmes de validation" />
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
                slide: 'style',
                gamesActifs: null,
                gameSelected: null,


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
            async getActualGames() {
                if (this.gamesActifs === null) {
                    await GameDataService.getAllActifs().then(response => {
                        this.gamesActifs = response.data;
                    }).catch();
                }

                this.games.forEach(g => this.dataGame.push({
                    name: 'Game' + g.id,
                    idGame: g.id,
                    creationDate: g.creationDate,
                    organizer: g.organizer.name + ' ' + g.organizer.firstName
                })
                );
            },

            transitionSlide(game) {
                this.gameSelected = game
            },

            async onRowClick(evt, row) {
                this.gameSelected = null;

                await GameDataService.get(row.idGame).then(response => {
                    this.gameSelected = response.data;
                }).catch();
                this.idGame = this.gameSelected.id;
            }
        }
    }
</script>
