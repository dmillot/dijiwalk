<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/jeuActuel' " />
                <q-toolbar-title>DijiWalk</q-toolbar-title>
            </q-toolbar>
        </q-header>
        <h5 class="text-bold text-left">Game {{id}}</h5>

        <q-card class="my-card full-height q-px-xl q-py-lg">
            <q-card-section class="flex column flex-center">
                <h4 class="text-bold text-red-14 q-ma-none q-mb-lg">Information générale du Game {{id}}</h4>

                <div class="q-pa-md">
                    <q-list dense bordered padding class="rounded-borders">
                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Date de Création</q-item-label>
                                <q-item-label>Le jeu a été cree le {{game.creationDate | formatDate}}</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Nom de l'organisateur</q-item-label>
                                <q-item-label>Il a été cree par {{game.organizer.firstName}} {{game.organizer.lastName}}</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Parcours</q-item-label>
                                <q-item-label>Le parcours choisi est le "{{game.route.name}}"</q-item-label>
                                <q-item-label caption>Decription : {{game.route.description}}</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-section>
                                    <img src="https://geeko.lesoir.be/wp-content/uploads/sites/58/2019/03/capture-e1552573152526.png">
                                </q-item-section>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Handicap</q-item-label>
                                <q-item-label v-if="this.handicap===true">Le parcours choisi est prévu pour des personnes a mobilité réduite</q-item-label>
                                <q-item-label v-else>Le parcours choisi n'est pas prévu pour des personnes a mobilité réduite</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Temps prévu</q-item-label>
                                <q-item-label>Le temps prévu pour ce parcours est de {{game.route.time | formatTime}}</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Moyen de Transport</q-item-label>
                                <q-item-label>Le moyen de transport prévu sur ce parcours est le "{{game.transport.libelle}}"</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label>
                                    <div class="q-pa-md">
                                        <q-table title="Avancement des équipes participantes"
                                                 :data="dataGame"
                                                 :columns="columns"
                                                 row-key="name" />
                                    </div>
                                </q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label>
                                    <div class="q-pa-md">
                                        <q-table title="Avancement des équipes participantes par étapes"
                                                 :data="steps"
                                                 :columns="stepsColumns"
                                                 row-key="name"
                                                 @row-click="onRowClick" />
                                    </div>
                                </q-item-label>
                            </q-item-section>
                        </q-item>
                    </q-list>
                </div>
            </q-card-section>
        </q-card>


        <q-dialog v-model="viewTeam">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <div class="q-pa-md">

                            <div v-for="player in players" v-bind:key="player.name">
                                <q-item>
                                    <q-item-section avatar>
                                        <q-avatar rounded>
                                            <img :src="player.picture">
                                        </q-avatar>
                                    </q-item-section>
                                    <q-item-section v-if="player.captain === team.idCaptain">{{player.playerName}}<i class="fas fa-crown"></i></q-item-section>
                                    <q-item-section v-else>{{player.playerName}}</q-item-section>
                                </q-item>

                            </div>

                        </div>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>






    </q-page>
</template>

<script>

import GameDataService from "@/services/GameDataService";
import TeamDataService from "@/services/TeamDataService";
import moment from 'moment'

export default {
        name: 'jeuActif',
        props: ['idJeu'],
        data() {
            return {
                id: null,
                game: null,
                team: null,
                time: null,
                plays: null,
                viewTeam: false,
                handicap: false,
                columns: [
                    {
                        name: 'name',
                        required: true,
                        label: 'Equipe',
                        align: 'left',
                        field: row => row.name,
                        format: val => `${val}`,
                        sortable: true
                    },
                    { name: 'dateDeDebut', align: 'center', label: 'Date de Début', field: 'dateDeDebut', sortable: true },
                    { name: 'dateDeFin', align: 'center', label: 'Date de Fin', field: 'dateDeFin', sortable: true },
                    { name: 'score', align: 'center', label: 'Score', field: 'score', sortable: true }
                ],
                dataGame: [],
                steps: [],
                stepsColumns: [
                    {
                        name: 'name',
                        required: true,
                        label: 'Equipe',
                        align: 'left',
                        field: row => row.name,
                        format: val => `${val}`,
                        sortable: true
                    },
                    { name: 'etape', align: 'center', label: 'Nom de l\'étape', field: 'stepName', sortable: true },
                    { name: 'etapeNumber', align: 'center', label: 'Numéro de l\'étape actuel', field: 'etapeNumber', sortable: true }
                ],
                players: []
            }
        },
        created() {
            this.id = this.$route.params.idJeu;
            this.getGameInfo();
        },
        methods: {
            async getGameInfo() {
                if (this.game === null) {
                   await GameDataService.get(this.id).then(response => {
                       this.game = response.data;
                   }).catch();
                }
                this.plays = this.game.plays;

                this.plays.forEach(thisPlay => this.dataGame.push({
                    name: thisPlay.team.name,
                    dateDeDebut: this.$options.filters.formatDate(thisPlay.startDate),
                    dateDeFin: this.$options.filters.formatDate(thisPlay.endDate),
                    score: thisPlay.score
                })
                );
                
                this.handicap = this.game.route.handicap;

                this.game.teamRoutes.forEach(tr => {
                    if(tr.validationDate===null){
                        this.steps.push({
                            name: tr.team.name,
                            stepName: tr.routeStep.step.name,
                            etapeNumber: tr.routeStep.order,
                            idTeam: tr.team.id
                        });
                    }
                });
            },   

            async onRowClick (evt, row) {
                this.team = null;

                await TeamDataService.get(row.idTeam).then(response => {
                    this.team = response.data;
                }).catch();
                this.players = [];
                this.team.teamPlayers.forEach(tp => this.players.push({
                    name: this.team.name,
                    captain: tp.player.id,
                    playerName: tp.player.firstName + ' ' + tp.player.lastName,
                    picture: tp.player.picture
                })
                );

                this.viewTeam = true;
            }

        },

        filters: {
            formatDate: function (value) {
                if (!value) return ''
                return moment(String(value)).format('DD/MM/YYYY à hh:mm')
            },

            formatTime: function (value) {
                if (!value) return ''
                var parts = value.split(':');
                var date =  parts[0] + 'h' + parts[1]+'min';
                return date;
            }
        }

}
</script>
