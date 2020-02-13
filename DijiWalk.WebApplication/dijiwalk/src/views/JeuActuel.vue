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
            <q-card-section class="flex column flex-center">
                <div class="q-pa-md">
                    <q-table title="Avancement des équipes participantes"
                             :data="dataGame"
                             :columns="columns"
                             row-key="name"
                             @row-click="onRowClick" />
                </div>
            </q-card-section>
            <q-card-section :disabled="game===null" class="flex column flex-center">
                <div class="row flex-center full-width justify-center q-mt-lg q-col-gutter-xl">

                    <Card link="jeuActif" :disabled="game===null" v-bind:params="{id: idGame}" icon="fas fa-hand-lizard" title="Jeu" description="Page de visualisation des information général du jeu" />
                    <Card link="" :disabled="game===null" icon="fas fa-clipboard-check" title="Validation" description="Page de gestion des problèmes de validation" />
                    <Card link="" :disabled="game===null" icon="fas fa-comments" title="Chat" description="Page de chat avec les participants" />

                </div>
            </q-card-section>
        </q-card>
    </q-page>
</template>

<script>
// @ is an alias to /src
import Card from '@/components/Card.vue'
import GameDataService from "@/services/GameDataService";

export default {
        name: 'jeuActuel',
        
        data() {
            return {
                games: null,
                game: null,
                idGame: 0,
                columns: [
                    {
                        name: 'name',
                        required: true,
                        label: 'Game',
                        align: 'left',
                        field: row => row.name,
                        format: val => `${val}`,
                        sortable: true
                    },
                    { name: 'creationDate', align: 'center', label: 'Date de Début', field: 'creationDate', sortable: true },
                    { name: 'organizer', align: 'center', label: 'Date de Fin', field: 'organizer', sortable: true }
                ],
                dataGame: [],
            }
        },

        components: {
            Card
        },
        computed:{
            gameActual : function(){
                
                return this.games.filter((g)=> g.active == true);
            }
        },


        created () {
            this.getActualGames();
        },
  
        methods: {
            async getActualGames() {
                if (this.games === null) {
                   await GameDataService.getAllActifs().then(response => {
                       this.games = response.data;
                   }).catch();
                }

                this.games.forEach(g => this.dataGame.push({
                    name: 'Game' + g.id,
                    idGame: g.id,
                    creationDate: g.creationDate,
                    organizer: g.organizer.firstName + ' ' + g.organizer.firstName
                })
                );
            },

            async onRowClick (evt, row) {
                this.game = null;

                await GameDataService.get(row.idGame).then(response => {
                    this.game = response.data;
                }).catch();
                this.idGame = this.game.id;
            }
        }   
    }
</script>
