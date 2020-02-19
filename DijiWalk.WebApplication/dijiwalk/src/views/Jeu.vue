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
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
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
                                <div class="col text-left text-bold text-h5 ellipsis">
                                    Jeu n°{{ game.id }}
                                </div>
                            </div>
                            <div class="row no-wrap q-mb-sm">
                                <div class="col text-left text-h6 ellipsis">
                                    {{ game.route.name }}
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
                        <q-btn @click="openModalToGetInformations(game)" class="dijiwalk-secondary" flat round icon="fas fa-info-circle" />
                        <q-btn @click="openModalToGetInformations(game)" class="dijiwalk-secondary" flat>
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


                        <q-input ref="date" class="col-12" color="primary" v-model="dateGame" name="dateGame" id="dateGame" lazy-rules :rules="[val => !!val || 'Veuillez renseigner une date']">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-calendar" class="cursor-pointer">
                                    <q-popup-proxy transition-show="scale" transition-hide="scale">
                                        <q-date v-model="dateGame" mask="YYYY-MM-DD HH:mm" />
                                    </q-popup-proxy>
                                </q-icon>
                            </template>

                            <template v-slot:append>
                                <q-icon name="fas fa-clock" class="cursor-pointer">
                                    <q-popup-proxy transition-show="scale" transition-hide="scale">
                                        <q-time v-model="dateGame" mask="YYYY-MM-DD HH:mm" format24h />
                                    </q-popup-proxy>
                                </q-icon>
                            </template>
                        </q-input>
                        <div v-if="getInformations" class="row col-12">
                            <div class="col-8">
                                <q-input label="Organisateur" v-model="organizerGame" type="text" disable />
                            </div>
                        </div>
                        <div class="row col-12">
                            <div class="col-10">
                                <q-select use-input ref="transport" v-model="transportGame" option-value="id" option-label="libelle" :options="transports" label="Transport" id="transportGame" name="transportGame" lazy-rules :rules="[val => !!val || 'Veuillez renseigner un transport']" />
                            </div>
                            <div v-if="!getInformations" class="col-2 row justify-center items-center">
                                <q-btn color="primary" @click="navigateTo('/transport')" rounded icon="fas fa-plus" />
                            </div>
                        </div>
                        <div class="row col-12">
                            <div class="col-10">
                                <q-select use-input ref="parcours" v-model="parcoursGame" option-value="id" option-label="name" :options="routes" label="Parcours" id="parcoursGame" name="parcoursGame" lazy-rules :rules="[val => !!val || 'Veuillez renseigner un parcours']" />
                            </div>
                            <div v-if="!getInformations" class="col-2 row justify-center items-center">
                                <q-btn color="primary" @click="navigateTo('/parcours')" rounded icon="fas fa-plus" />
                            </div>
                        </div>
                        <div class="row col-12">
                            <div class="col-10">
                                <q-select multiple use-input ref="equipe" label="Equipes" v-model="equipeGame"
                                          :options="teams"
                                          :option-value="opt => opt.id"
                                          :option-label="opt => opt.name"
                                          emit-value
                                          map-options
                                          style="min-width: 250px"
                                          lazy-rules
                                          :rules="[val => val.length > 0 || 'Veuillez renseigner au minimum une équipe']" />
                            </div>
                            <div class="col-2 row justify-center items-center">
                                <q-btn color="primary" @click="navigateTo('/equipe')" rounded icon="fas fa-plus" />
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

        <q-dialog v-if="gameSelected !== null" v-model="informations">
            <q-card class="modal-informations">
                <q-card-section class="row items-center">
                    <div class="row col-12">
                        <div class="col-12">
                            <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />
                            <h5 class="q-my-sm">Game n°{{ gameSelected.id }}</h5>
                            <p class="q-mb-none text-bold">{{  gameSelected.route.name }}</p>
                            <p class="q-mt-none">{{ gameSelected.route.description }}</p>
                            <p class="text-grey">Organisé par {{ gameSelected.organizer.firstName }} {{ gameSelected.organizer.lastName }}</p>
                        </div>
                    </div>
                    <div class="row col-12" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                        <q-item>
                            <q-item-section avatar>
                                <q-icon color="grey" name="fas fa-calendar" />
                            </q-item-section>

                            <q-item-section>{{ gameSelected.creationDate | formatDate }}</q-item-section>
                        </q-item>
                    </div>
                    <div class="row col-12" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                        <q-item>
                            <q-item-section avatar>
                                <q-icon color="grey" name="fas fa-bicycle" />
                            </q-item-section>

                            <q-item-section>{{ gameSelected.transport.libelle }}</q-item-section>
                        </q-item>
                    </div>
                    <div class="row col-12" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                        <q-item>
                            <q-item-section avatar>
                                <q-icon color="grey" name="fas fa-route" />
                            </q-item-section>

                            <q-item-section>{{ gameSelected.route.name }}</q-item-section>
                        </q-item>
                    </div>
                    <div class="row col-12">
                        <q-list class="custom-expansion col-12">
                            <q-expansion-item expand-separator
                                              icon="fas fa-users"
                                              label="Équipes">
                                <q-expansion-item icon="fas fa-user-friends" v-for="team in selectedGameTeams" v-bind:key="team.id" v-bind:label="team.name" :header-inset-level="1" :content-inset-level="2" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                                    <q-card class="card-expansion">
                                        <q-card-section v-for="member in team.members" v-bind:key="member.id" class="row items-center">
                                            <q-avatar size="48px" class="q-mr-md">
                                                <q-img :src="member.picture" style="width:50px; height: 50px;" />
                                            </q-avatar>
                                            {{ member.firstName }} {{ member.lastName }}
                                            <q-icon v-if="team.id_captain == member.id" name="fas fa-star" class="q-ml-md" style="color: #ffd600 !important; font-size: 1.5em;" />
                                        </q-card-section>
                                    </q-card>
                                </q-expansion-item>
                            </q-expansion-item>
                        </q-list>
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

    import moment from 'moment'
    import GameDataService from "@/services/GameDataService"
    import RouteDataService from "@/services/RouteDataService"
    import TransportDataService from "@/services/TransportDataService"
    import TeamDataService from "@/services/TeamDataService"
    import TeamPlayerDataService from "@/services/TeamPlayerDataService"

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
                informations: false,
                selectedGameTeams: null
            }
        },

        created() {
            this.getAllGames();
            this.getAllRoutes();
            this.getAllTeams();
            this.getAllTransports();
        },

        methods: {
            navigateTo(page) {
                this.$router.push(page)
            },
            openModalToGetInformations(game) {
                this.isAdding = false;
                this.isEditing = false;
                this.gameSelected = game;

                var teams = [];
                game.plays.forEach(function (item) {
                    var players = [];
                    TeamPlayerDataService.get(item.idTeam).then(response => {
                       
                        response.data.forEach(function (i) {
                            players.push(i.player);
                        })

                    }).catch();

                    teams.push({
                        id: item.team.id,
                        name: item.team.name,
                        id_captain: item.team.idCaptain,
                        members: players
                    })
                })

                this.selectedGameTeams = teams;
                this.informations = true;
            },

            openModalToAdd() {
                this.isEditing = false;
                this.getInformations = false;
                this.isAdding = true;
                this.resetInput();
                this.manageGame = true;
                this.dateGame = moment().format("YYYY-MM-DD HH:mm");
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
                this.dateGame = moment(String(game.creationDate)).format('YYYY-MM-DD HH:mm');
                this.parcoursGame = game.route;
                this.equipeGame = listTeams;
                this.transportGame = game.transport;
                this.organizerGame = game.organizer.firstName + " " + game.organizer.lastName;
            },

            getAllRoutes() {
                if (this.routes === null) {
                    RouteDataService.getAll().then(response => {
                        this.routes = response.data;
                    }).catch();
                }
            },

            getAllTeams() {
                if (this.teams === null) {
                    TeamDataService.getAll().then(response => {
                        this.teams = response.data;
                    }).catch();
                }
            },

            getAllTransports() {
                if (this.transports === null) {
                    TransportDataService.getAll().then(response => {
                        this.transports = response.data;
                    }).catch();
                }
            },

            getAllGames() {
                this.$q.loading.show()
                GameDataService.getAll().then(response => {
                    this.games = response.data
                    this.$q.loading.hide()
                }).catch();
            },

            openModalToDelete(game) {
                this.confirm = true,
                    this.selectedGameId = game.id
            },

            updateGame() {
                if (this.$refs.date.validate() && this.$refs.transport.validate() && this.$refs.parcours.validate() && this.$refs.equipe.validate()) {
                    if (this.equipeGame.length > 0) {
                         this.$q.loading.show()
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
                            Route: this.parcoursGame,
                            IdTransport: this.transportGame.id,
                            IdOrganizer: 1,
                            Plays: plays
                        }).then(response => {
                            this.$q.loading.hide()
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
                                this.manageGame = false;
                                this.$q.notify({
                                    icon: 'fas fa-exclamation-triangle',
                                    color: 'negative',
                                    message: response.data.message,
                                    position: 'top'
                                })
                            }
                        })
                    }
                }
            },

            deleteGame() {
                this.$q.loading.show()
                var id = this.selectedGameId;
                GameDataService.delete(this.selectedGameId).then(response => {
                    this.$q.loading.hide()
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

                }).catch();
            },

            addGame() {

                if (this.$refs.date.validate() && this.$refs.transport.validate() && this.$refs.parcours.validate() && this.$refs.equipe.validate()) {
                    this.$q.loading.show()
                    var listPlays = [];
                    this.equipeGame.forEach(function (item) {
                        if (Number.isInteger(item)) {
                            listPlays.push({ IdGame: 0, IdTeam: item });
                        } else {
                            listPlays.push({ IdGame: 0, IdTeam: item.id });
                        }
                    })
                    GameDataService.create({
                        IdRoute: this.parcoursGame.id,
                        Route: this.parcoursGame,
                        CreationDate: this.dateGame,
                        IdTransport: this.transportGame.id,
                        IdOrganizer: 1,
                        Plays: listPlays
                    }).then(response => {
                        this.$q.loading.hide();
                        if (response.data.status == 1) {
                            this.manageGame = false;

                            this.games.push(response.data.response);

                            this.$q.notify({
                                icon: 'fas fa-check-square',
                                color: 'secondary',
                                message: `Ajout du jeu n°${response.data.response.id} réussi !`,
                                position: 'top'
                            })
                        } else {
                            this.manageGame = false;
                            this.$q.notify({
                                icon: 'fas fa-exclamation-triangle',
                                color: 'negative',
                                message: response.data.message,
                                position: 'top'
                            })
                            setTimeout(this.onResetValidation, 3000);
                        }

                    }).catch();

                    this.manageGame = false;
                }
            },

            filterParcours(val, update) {

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
                return moment(String(value)).format('DD/MM/YYYY HH:mm')
            }
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>