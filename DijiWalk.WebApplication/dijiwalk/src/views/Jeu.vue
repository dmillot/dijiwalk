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
                                <q-select multiple
                                          use-input
                                          ref="equipeFilter"
                                          label="Filtrer par équipe"
                                          v-model="teamsFilterModel"
                                          :options="teamsFiltered"
                                          :option-value="opt => opt.id"
                                          :option-label="opt => opt.name"
                                          emit-value
                                          map-options
                                          @filter="filterEquipe"
                                          style="min-width: 250px; max-width: 300px">
                                </q-select>
                            </q-item>
                            <q-item>
                                <q-select multiple
                                          use-input
                                          ref="parcoursFilter"
                                          label="Filtrer par parcours"
                                          v-model="routesFilterModel"
                                          :options="routesFiltered"
                                          :option-value="opt => opt.id"
                                          :option-label="opt => opt.name"
                                          emit-value
                                          map-options
                                          @filter="filterParcours"
                                          style="min-width: 250px; max-width: 300px">
                                </q-select>
                            </q-item>
                            <q-item>
                                <q-select multiple
                                          use-input
                                          ref="transportFilter"
                                          label="Filtrer par transport"
                                          v-model="transportsFilterModel"
                                          :options="transportsFiltered"
                                          :option-value="opt => opt.id"
                                          :option-label="opt => opt.libelle"
                                          emit-value
                                          map-options
                                          @filter="filterTransport"
                                          style="min-width: 250px; max-width: 300px">
                                </q-select>
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd(false);" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
                    <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
                </q-card>
            </div>
            <div v-for="game in games" v-bind:key="game.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">

                    <div @click="openModalToEdit(game)" class="game-card">
                        <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />

                        <q-card-section>
                            <q-btn v-on:click.stop="openModalToDelete(game)"
                                   fab
                                   color="negative"
                                   icon="fas fa-trash"
                                   class="absolute"
                                   style="top: 0; right: 12px; transform: translateY(-50%); z-index: 999;" />

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

                    </div>

                    <q-separator />

                    <q-card-actions>
                        <q-btn flat round icon="fas fa-info-circle" />
                        <q-btn @click="openModalToGetInformations(game)" flat color="primary text-bold">
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

        <q-dialog v-model="manageGame">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <q-input v-if="isEditing" v-model="idGame" type="hidden" />
                        <div class="row col-12">
                            <div class="col-8">
                                <q-input v-bind:disable="getInformations" ref="date" color="primary" v-model="dateGame" type="date" name="dateGame" id="dateGame" lazy-rules :rules="[val => !!val || 'Veuillez renseigner une date']" />
                            </div>
                        </div>
                        <div v-if="getInformations" class="row col-12">
                            <div class="col-8">
                                <q-input label="Organisateur" v-model="organizerGame" type="text" disable />
                            </div>
                        </div>
                        <div class="row col-12">
                            <div class="col-8">
                                <q-select v-bind:disable="getInformations" use-input ref="transport" v-model="transportGame" option-value="id" option-label="libelle" :options="transports" label="Transport" id="transportGame" name="transportGame" lazy-rules :rules="[val => !!val || 'Veuillez renseigner un transport']" />
                            </div>
                            <div v-if="!getInformations" class="col-4 row justify-center items-center">
                                <q-btn color="primary" label="Ajouter" />
                            </div>
                        </div>
                        <div class="row col-12">
                            <div class="col-8">
                                <q-select v-bind:disable="getInformations" use-input ref="parcours" v-model="parcoursGame" option-value="id" option-label="name" :options="routes" label="Parcours" id="parcoursGame" name="parcoursGame" lazy-rules :rules="[val => !!val || 'Veuillez renseigner un parcours']" />
                            </div>
                            <div v-if="!getInformations" class="col-4 row justify-center items-center">
                                <q-btn color="primary" label="Ajouter" />
                            </div>
                        </div>
                        <div class="row col-12">
                            <div class="col-8">
                                <q-select v-bind:disable="getInformations" multiple use-input ref="equipe" label="Equipes" v-model="equipeGame"
                                          :options="teams"
                                          :option-value="opt => opt.id"
                                          :option-label="opt => opt.name"
                                          emit-value
                                          map-options
                                          style="min-width: 250px"
                                          lazy-rules
                                          :rules="[val => val.length > 0 || 'Veuillez renseigner au minimum une équipe']" />
                            </div>
                            <div v-if="!getInformations" class="col-4 row justify-center items-center">
                                <q-btn color="primary" label="Ajouter" />
                            </div>
                        </div>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="isEditing" label="Modifier" @click="updateGame()" color="secondary" />
                    <q-btn flat v-if="isAdding" label="Ajouter" color="primary" @click="addGame()" />
                </q-card-actions>
            </q-card>
        </q-dialog>
    </q-page>
</template>

<script>

    import moment from 'moment'
    import GameDataService from "@/services/GameDataService"
    import RouteDataService from "@/services/RouteDataService"
    import TransportDataService from "@/services/TransportDataService"
    import TeamDataService from "@/services/TeamDataService"
    import PlayDataService from "@/services/PlayDataService"

    export default {
        name: 'jeu',

        data() {
            return {
                games: null,
                routes: null,
                teams: null,
                transports: null,
                teamsFiltered: null,
                routesFiltered: null,
                transportsFiltered: null,
                teamsFilterModel: null,
                routesFilterModel: null,
                transportsFilterModel: null,
                selectedGameId: null,
                gameSelected: null,
                confirm: false,
                dateGame: null,
                idGame: null,
                parcoursGame: null,
                transportGame: null,
                equipeGame: null,
                manageGame: false,
                isEditing: false,
                isAdding: false,
                organizerGame: null,
                getInformations: false,
            }
        },

        created () {
            this.getAllGames();
            this.getAllRoutes();
            this.getAllTeams();
            this.getAllTransports();
        },

        methods: {

            openModalToGetInformations(game) {
                this.isAdding = false;
                this.isEditing = false;
                this.getInformations = true;
                this.fillForm(game);
                this.manageGame = true;
            },

            openModalToAdd() {
                this.isEditing = false;
                this.getInformations = false;
                this.isAdding = true;
                this.resetInput();
                this.manageGame = true;
            },

            openModalToEdit(game) {
                this.isEditing = true;
                this.getInformations = false;
                this.isAdding = false;
                this.resetInput();
                this.fillForm(game);
                this.manageGame = true
            },

            resetInput() {
                this.idGame = null
                this.dateGame = null
                this.parcoursGame = null
                this.equipeGame = null
                this.transportGame = null
            },

            fillForm(game) {
                var listTeams = [];
                game.plays.forEach(function (item) {
                    listTeams.push(item.team);
                });
                this.gameSelected = game;
                this.idGame = game.id;
                this.dateGame = moment(String(game.creationDate)).format('YYYY-MM-DD');
                this.parcoursGame = game.route;
                this.equipeGame = listTeams;
                this.transportGame = game.transport;
                this.organizerGame = game.organizer.firstName + " " + game.organizer.lastName;
                
            },

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

            getAllTransports () {
                if (this.transports === null) {
                    TransportDataService.getAll().then(response => {
                        this.transports = response.data;
                    }).catch(reason => {
                        console.log(reason);
                    });
                }
            },

            getAllGames () {
                GameDataService.getAll().then(response => { this.games = response.data }).catch(error => { console.log(error) });
            },

            openModalToDelete (game) {
                this.confirm = true,
                this.selectedGameId = game.id
            },

            updateGame() {
                if (this.$refs.date.validate() && this.$refs.transport.validate() && this.$refs.parcours.validate() && this.$refs.equipe.validate()) {
                    if (this.equipeGame.length > 0) {
                        var plays = [];
                        this.equipeGame.forEach((function (item) {
                            if (Number.isInteger(item)) {
                                plays.push({ IdGame: this.idGame, IdTeam: item });
                            } else {
                                plays.push({ IdGame: this.idGame, IdTeam: item.id });
                            }
                        
                        }).bind(this))

                        GameDataService.update(this.idGame, {
                            CreationDate: this.dateGame,
                            IdRoute: this.parcoursGame.id,
                            IdTransport: this.transportGame.id,
                            IdOrganizer: 1,
                            Plays: plays
                        }).then(response => {
                            if (response.data.status == 1) {
                                this.manageGame = false;
                                this.games[this.games.map(e => e.id).indexOf(this.gameSelected.id)] = response.data.response

                                this.$q.notify({
                                    icon: 'fas fa-check-square',
                                    color: 'secondary',
                                    message: response.data.message,
                                    position: 'top'
                                })
                            } else {
                                this.$q.notify({
                                    icon: 'fas fa-exclamation-triangle',
                                    color: 'negative',
                                    message: response.data.message,
                                    position: 'top'
                                })
                                setTimeout(this.onResetValidation, 3000);
                            }
                        })
                    }
                }
            },

            deleteGame () {
                var id = this.selectedGameId;
                GameDataService.delete(this.selectedGameId).then(response => {

                    if (response.data.status == 1) {
                        this.games = this.games.filter(function (obj) {
                            return obj.id !== id;
                        });

                        this.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: "Suppression du jeu réussie !",
                            position: 'top'
                        })

                    } else {
                        this.$q.notify({
                            message: response.data.message,
                            color: 'negative',
                            icon: 'fas fa-exclamation-triangle',
                            position: 'top'
                        })
                    }

                }).catch(reason => {
                    console.log(reason);
                });
            },

            addGame() {

                if (this.$refs.date.validate() && this.$refs.transport.validate() && this.$refs.parcours.validate() && this.$refs.equipe.validate()) {
                    GameDataService.create({
                        IdRoute: this.parcoursGame.id,
                        CreationDate: this.dateGame,
                        IdTransport: this.transportGame.id,
                        IdOrganizer: 1,
                    }).then(response => {
                        this.games.push(response.data.response);

                        var idGameCreated = response.data.response.id;

                        this.equipeGame.forEach(function (item) {
                            PlayDataService.create({
                                IdGame: idGameCreated,
                                IdTeam: item
                            }).then(() => {
                                //
                            }).catch(reason => {
                                console.log(reason);
                            });
                        })

                        this.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: `Ajout du jeu n°${idGameCreated} réussi !`,
                            position: 'top'
                        })

                    }).catch(reason => {
                        console.log(reason);
                    });

                    this.manageGame = false;
                }
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
            },

            filterTransport(val, update) {

                if (val === '') {
                    update(() => {
                        this.transportsFiltered = this.transports
                    })
                    return
                }

                update(() => {
                    const needle = val.toLowerCase()
                    this.transportsFiltered = this.transports.filter(v => v.libelle.toLowerCase().indexOf(needle) > -1)
                })
            }
        },

        filters: {
            formatDate: function (value) {
                if (!value) return ''
                return moment(String(value)).format('DD/MM/YYYY')
            }
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }

    .game-card:hover:after {
        content: "";
        position: absolute;
        top: 0;
        left: 0;
        background-color: black;
        opacity: 0.3;
        width: 100%;
        height: 100%;
        cursor: pointer;
    }
</style>