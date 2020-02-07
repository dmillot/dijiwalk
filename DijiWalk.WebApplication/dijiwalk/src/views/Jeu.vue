<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>

                <q-toolbar-title>DijiWalk</q-toolbar-title>

                <div class="q-ml-md cursor-pointer non-selectable">
                    <q-icon name="fas fa-search" />
                    <q-menu>
                        <q-list bordered separator style="min-width: 100px">
                            <q-item>
                                <q-select filled
                                          use-input
                                          use-chips
                                          v-model="teamsFilterModel"
                                          option-id="id"
                                          option-label="name"
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par équipe"
                                          :options="teamsFiltered"
                                          @filter="filterEquipe"
                                          style="width: 250px" />
                            </q-item>
                            <q-item>
                                <q-select filled
                                          use-input
                                          use-chips
                                          v-model="routesFilterModel"
                                          option-id="id"
                                          option-label="name"
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par parcours"
                                          :options="routesFiltered"
                                          @filter="filterParcours"
                                          style="width: 250px" />
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
                    <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
                </q-card>
            </div>
            <div v-for="game in games" v-bind:key="game.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />

                    <q-card-section>
                        <q-btn @click="openModalToDelete(game)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px; transform: translateY(-50%);" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                Jeu n°{{ game.id }}
                            </div>
                        </div>
                        <div class="row items-center no-wrap text-grey">
                            <q-icon name="fas fa-calendar" />
                            <p class="q-ma-none q-ml-xs">{{ game.creationDate | formatDate }}</p>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <q-card-actions>
                        <q-btn flat round icon="fas fa-info-circle" />
                        <q-btn @click="openModalToEdit(game)" flat color="primary text-bold">
                            Informations
                        </q-btn>
                    </q-card-actions>
                </q-card>
            </div>
        </div>

        <q-dialog v-model="confirm">
            <q-card>
                <q-card-section class="row items-center">
                    <q-avatar icon="fas fa-exclamation-triangle" color="negative" text-color="white" />
                    <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer le jeu n°{{ selectedGameId }} ?</span>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" color="negative" @click="deleteGame()" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-model="editJeu">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <div class="col-12">
                            <q-input color="primary" v-model="dateGame" type="date" name="dateGame" id="dateGame" />
                        </div>
                        <div class="col-8">
                            <q-select v-model="parcoursGame" option-value="id" option-label="name" :options="routes" label="Parcours" id="parcoursGame" name="parcoursGame" />
                        </div>
                        <div class="col-8">
                            <q-select v-model="equipeGame" option-value="id" option-label="name" :options="teams" label="Equipes" id="equipeGame" name="equipeGame" />
                        </div>
                        <div class="col-4 row justify-center items-center">
                            <q-btn color="primary" label="Ajouter" />
                        </div>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Ajouter" color="primary" @click="addGame()" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-model="addJeu">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <div class="col-12">
                            <q-input color="primary" v-model="dateGame" type="date" name="dateGame" id="dateGame" />
                        </div>
                        <div class="col-8">
                            <q-select v-model="parcoursGame" option-value="id" option-label="name" :options="routes" label="Parcours" id="parcoursGame" name="parcoursGame" />
                        </div>
                        <div class="col-8">
                            <q-select v-model="equipeGame" option-value="id" option-label="name" :options="teams" label="Equipes" id="equipeGame" name="equipeGame" />
                        </div>
                        <div class="col-4 row justify-center items-center">
                            <q-btn color="primary" label="Ajouter" />
                        </div>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Ajouter" color="primary" @click="addGame()" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>
    </q-page>
</template>

<script>

    import moment from 'moment'
    import GameDataService from "@/services/GameDataService";
    import RouteDataService from "@/services/RouteDataService";
    import TeamDataService from "@/services/TeamDataService";
    import PlayDataService from "@/services/PlayDataService";

    export default {
        name: 'jeu',

        data() {
            return {
                games: null,
                routes: null,
                teams: null,
                teamsFiltered: null,
                routesFiltered: null,
                teamsFilterModel: null,
                routesFilterModel: null,
                selectedGameId: null,
                confirm: false,
                dateGame: null,
                parcoursGame: null,
                equipeGame: null,
                addJeu: false,
                editJeu: false
            }
        },

        created () {
            this.getAllGames();
            this.getAllRoutes();
            this.getAllTeams();
        },

        methods: {

            getAllRoutes () {
                if (this.routes === null) {
                    RouteDataService.getAll().then(response => {
                        this.routes = response.data;
                    }).catch(reason => {
                        console.log(reason);
                    });
                }
            },

            getAllTeams () {
                if (this.teams === null) {
                    TeamDataService.getAll().then(response => {
                        this.teams = response.data;
                    }).catch(reason => {
                        console.log(reason);
                    });
                }
            },

            getAllGames () {
                if (this.games === null) {
                    GameDataService.getAll().then(response => {
                        this.games = response.data;
                    }).catch(reason => {
                        console.log(reason);
                    });
                }
            },

            openModalToAdd () {
                this.getAllRoutes(),
                this.getAllTeams(),
                this.addJeu = true
            },

            openModalToDelete (game) {
                this.confirm = true,
                this.selectedGameId = game.id
            },

            openModalToEdit (game) {
                this.editJeu = true,
                this.selectedGameId = game.id
            },

            deleteGame () {
                var id = this.selectedGameId;
                GameDataService.delete(this.selectedGameId).then(response => {

                    this.games = this.games.filter(function( obj ) {
                        return obj.id !== id;
                    });

                    console.log(response);
                }).catch(reason => {
                    console.log(reason);
                });
            },

            addGame() {

                GameDataService.create({
                    IdRoute: this.parcoursGame.id,
                    CreationDate: this.dateGame
                }).then(response => {
                    this.games.push(response.data);
                    PlayDataService.create({
                        IdGame: response.data.id,
                        IdTeam: this.equipeGame.id
                    }).then(response => {
                        console.log(response);
                    }).catch(reason => {
                        console.log(reason);
                    });


                }).catch(reason => {
                    console.log(reason);
                });
            },

            filterParcours (val, update) {

                if (val === '') {
                    update(() => {
                        this.routesFiltered = this.routes
                    })
                    return
                }

                update(() => {
                    const needle = val.toLowerCase()
                    this.routesFiltered = this.routes.filter(v => v.name.toLowerCase().indexOf(needle) > -1)
                })
            },

            filterEquipe(val, update) {

                if (val === '') {
                    update(() => {
                        this.teamsFiltered = this.teams
                    })
                    return
                }

                update(() => {
                    const needle = val.toLowerCase()
                    this.teamsFiltered = this.teams.filter(v => v.name.toLowerCase().indexOf(needle) > -1)
                })
            }
        },

        filters: {
            formatDate: function (value) {
                if (!value) return ''
                return moment(String(value)).format('DD/MM/YYYY à hh:mm')
            }
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>