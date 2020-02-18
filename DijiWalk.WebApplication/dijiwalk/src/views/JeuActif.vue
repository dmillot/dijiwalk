<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar class="bg-toolbar">
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/jeuActuel' " />
                <q-toolbar-title>DijiWalk</q-toolbar-title>
            </q-toolbar>
        </q-header>
        <h5 class="text-bold text-left">Jeu {{id}}</h5>

        <q-card class="my-card full-height q-px-xl q-py-lg">
            <q-card-section class="flex column flex-center">
                <h4 class="text-bold text-red-14 q-ma-none q-mb-lg">Information générale du jeu {{id}}</h4>

                <div class="q-pa-md" style="width: 75%;">
                    <q-list dense bordered padding class="rounded-borders">
                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Date de Création</q-item-label>
                                <q-item-label>Le jeu a été crée le {{game.creationDate | formatDate}}</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Nom de l'organisateur</q-item-label>
                                <q-item-label>Il a été crée par {{game.organizer.firstName}} {{game.organizer.lastName}}</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>Parcours</q-item-label>
                                <q-item-label>Le parcours choisi est le "{{game.route.name}}"</q-item-label>
                                <q-item-label caption>Description : {{game.route.description}}</q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <GmapMap ref="mapRef" :center="center" :zoom="12" style="width:100%;  height: 400px;">
                                    <GmapMarker :key="index" v-for="(m,index) in markers" :title="m.title" :position="m.position" @click="toggleInfoWindow(m)"></GmapMarker>
                                </GmapMap>
                            </q-item-section>
                        </q-item>

                        <q-separator spaced inset />

                        <q-item>
                            <q-item-section>
                                <q-item-label overline>PMR</q-item-label>
                                <q-item-label v-if="game.handicap===true">Le parcours choisi est prévu pour des personnes a mobilité réduite</q-item-label>
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
                            <q-item-section class="row">
                                <q-item-label overline>Équipes</q-item-label>
                                <div v-for="equipe in equipeGame" :key="equipe.id" @click="onTeam(equipe)" class="row bg-primary items-center justify-between shadow-5 q-my-sm q-pa-md equipe-row">
                                    <div class="row">
                                        <q-icon class="q-mr-md" size="25px" name="fas fa-users" color="white" />
                                        <h6 class="no-margin text-white">{{equipe.name}}</h6>
                                    </div>
                                    <div class="row items-center">
                                        <q-btn class="q-mr-md text-bold" :color="equipe.status.color" size="15px" flat icon="fas fa-circle" :label="equipe.status.label" @click="toValidation"/>
                                        <q-icon class="q-mr-sm" size="25px" name="fas fa-shoe-prints" color="white" />
                                        <h6 class="q-my-none q-ml-none q-mr-md text-white">{{equipe.status.etape}}</h6>
                                        <q-icon size="25px" name="fas fa-eye" color="white" />
                                    </div>
                                </div>
                            </q-item-section>
                        </q-item>
                    </q-list>
                </div>
            </q-card-section>
        </q-card>

        <q-dialog v-model="viewTeam">
            <q-card>
                <q-card-section class="row q-pb-none">
                    <div class="column">
                        <h4 class="q-my-md q-mx-none">Membres de l'équipe:</h4>
                        <div class="row">
                            <div v-for="participant in playerSelected" v-bind:key="participant.id" class="col-6">
                                <q-img :src="participant.picture" class="rounded-borders q-mt-sm q-px-sm" :ratio="1" style="max-width: 155px; max-height: 155px;">
                                    <div class="absolute-bottom text-subtitle1 text-center">
                                        <q-icon name="fas fa-star" v-if="participant.id == teamSelected.captain.id" />
                                        {{ participant.firstName }} {{ participant.lastName }}
                                    </div>
                                </q-img>
                            </div>
                        </div>
                    </div>
                    <q-separator class="q-mx-lg" vertical ></q-separator>
                    <div class="column">
                        <h4 class="q-my-md q-mx-none">Parcours de l'équipe:</h4>
                        <GmapMap class="shadow-5 q-mt-md" ref="mapRef" :center="center" :zoom="12" style="width:100%;  height: 400px;">
                            <GmapMarker :key="index" v-for="(m,index) in teamSelectedMarker" :title="m.title" :label="m.label" :position="m.position" @click="toggleInfoWindow(m)" :icon="m.icon"></GmapMarker>
                        </GmapMap>
                    </div>
                </q-card-section>
                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>



        <q-dialog v-if="showInfo" v-model="showInfo">
            <q-card class="modal-informations" style="width: 350px;">
                <q-card-section class="items-center">
                    <div v-if="!showDone" class="row justify-center q-my-md">
                        <q-avatar size="150px">
                            <q-img :src="infoWindowContext.picture" style="width:150px; height: 150px;" />
                        </q-avatar>
                    </div>
                    <div v-if="showDone" class="row justify-center q-my-md">
                        <q-img :src="infoWindowContext.picture" class="col-6 q-pr-sm image-step-left"  />
                        <q-img :src="infoWindowContext.validation" class="col-6 q-pl-sm image-step-right" />
                    </div>
                    <p class="text-bold">{{ infoWindowContext.title }}</p>
                    <p class="text-caption">{{ infoWindowContext.description }}</p>
                </q-card-section>
            </q-card>
        </q-dialog>





    </q-page>
</template>

<script>
    import GameDataService from "@/services/GameDataService";
    import moment from 'moment'


    export default {
        name: 'jeuActif',
        props: ['idJeu'],
        data() {
            return {
                id: null,
                game: null,
                equipeGame: [],
                showInfo: false,
                infoWindowContext: null,
                markers: [],
                center: null,
                viewTeam: false,
                teamSelected: null,
                playerSelected: null,
                teamSelectedMarker: [],
                markerGood: {
                    url: "https://storage.cloud.google.com/dijiwalk-test/spotlight-poi-dotless1.png",
                    labelOrigin: {x: 13.5, y: 15.5}
                },
                markerBad: {
                    url: "https://storage.cloud.google.com/dijiwalk-test/spotlight-poi-dotless2.png",
                    labelOrigin: {x: 13.5, y: 15.5}
                },
                showDone: false

            }
        },
        created() {
            this.id = this.$route.params.idJeu;
            this.getGameInfo();
        },
        methods: {
            toValidation() {
               this.$router.push({ name: "validation", params: { idJeu: this.id } });
            },
            onTeam(equipe) {
                this.viewTeam = true;
                this.teamSelected = equipe;

                this.playerSelected = equipe.teamPlayers.map(function (el) {
                    return el.player;
                })
                this.initMapTeam(equipe.gameTeamRoutes);
            },
            toggleInfoWindow(context) {
                if (context.validation != null && context.validation != undefined) {
                    this.showDone = true;
                } else {
                    this.showDone = false;
                }
                this.infoWindowContext = context;
                this.showInfo = true;
            },
            initMap(routeSteps) {
                this.markers = [];
                this.center = { lat: routeSteps[0].step.lat, lng: routeSteps[0].step.lng };
                routeSteps.forEach(e => this.addMarker(e));
            },
            addMarker(routeStep) {
                this.markers.push({ id: routeStep.step.id, title: routeStep.step.name, label: routeStep.step.name, description: routeStep.step.description, picture: routeStep.step.validation, position: { lat: routeStep.step.lat, lng: routeStep.step.lng } });
            },
            initMapTeam(teamRoutes) {
                this.teamSelectedMarker = [];
                this.center = { lat: teamRoutes[0].routeStep.step.lat, lng: teamRoutes[0].routeStep.step.lng };
                teamRoutes.forEach(e => this.addMarkerTeam(e));
            },
            addMarkerTeam(teamRoute) {
                this.teamSelectedMarker.push({
                    id: teamRoute.routeStep.step.id,
                    title: teamRoute.routeStep.step.name,
                    label: { text: String(teamRoute.stepOrder), color: "white" },
                    description: teamRoute.routeStep.step.description,
                    picture: teamRoute.routeStep.step.validation,
                    position: { lat: teamRoute.routeStep.step.lat, lng: teamRoute.routeStep.step.lng },
                    icon: teamRoute.validationDate != null ? this.markerGood : this.markerBad,
                    validation: teamRoute.picture != null ? teamRoute.picture : null
                });
            },
            getGameInfo() {
                this.$q.loading.show()
                var self = this;
                if (self.game === null) {
                    GameDataService.get(self.id).then(response => {
                        self.game = response.data;

                        self.initMap(self.game.route.routeSteps)
                        var idTeamDone = null;
                        self.game.teamRoutes.forEach(function (el) {
                            self.game.plays.map(function (element) {
                                if (element.idTeam == el.idTeam) {
                                    if (element.team.gameTeamRoutes == null || element.team.gameTeamRoutes == undefined) {
                                        element.team.gameTeamRoutes = [];
                                        element.team.gameTeamRoutes.push(el)
                                    } else {
                                        element.team.gameTeamRoutes.push(el)
                                    }
                                }
                            })
                            if (el.idTeam != idTeamDone && el.validationDate == null) {
                                self.game.plays.map(function (element) {
                                    if (element.idTeam == el.idTeam) {
                                        element.team.status = { label: el.askValidationDate != null && !el.validate ? "BLOQUÉE" : "EN COURS", color: el.askValidationDate != null && !el.validate ? "negative" : "secondary", etape: el.stepOrder }
                                        idTeamDone = el.idTeam;
                                    }
                                })
                            }
                        })

                        self.game.plays.forEach(function (el) {
                            self.equipeGame.push(el.team)
                            return el
                        })
                        self.$q.loading.hide()
                    }).catch();

                }

            },
        },

        filters: {
            formatDate: function (value) {
                if (!value) return ''
                return moment(String(value)).format('DD/MM/YYYY à hh:mm')
            },

            formatTime: function (value) {
                if (!value) return ''
                var parts = value.split(':');
                var date = parts[0] + 'h' + parts[1] + 'min';
                return date;
            }
        }

    }
</script>
<style scoped>
    .equipe-row:hover {
        cursor: pointer !important;
    }

    .image-step-left{
        border-radius: 4px 0px 0px 4px !important;
    }

    .image-step-right{
         border-radius: 0px 4px 4px 0px !important;
    }

    .q-dialog__inner--minimized > div{
        max-width: 750px !important;
    }

</style>