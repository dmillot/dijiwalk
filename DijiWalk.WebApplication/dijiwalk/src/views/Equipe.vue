<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>

                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/' " />
                <q-toolbar-title>DijiWalk</q-toolbar-title>

            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>
            <div v-for="equipe in equipes" v-bind:key="equipe.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <div @click="openModalToEdit(equipe)" class="game-card">
                        <div class="row">
                            <div v-for="participant in equipe.teamPlayers" v-bind:key="participant.idTeam + participant.idPlayer" class="col-6">
                                <q-img :src="participant.player.picture" :ratio="1" style="max-width: 250px; max-height: 250px;" />
                            </div>
                        </div>

                        <q-card-section>
                            <q-btn v-on:click.stop="openModalToDelete(equipe)"
                                   fab
                                   color="negative"
                                   icon="fas fa-trash"
                                   class="absolute"
                                   style="top: 0; right: 12px; transform: translateY(-50%); z-index: 999;" />

                            <div class="row no-wrap">
                                <div class="col text-left text-bold text-h6 ellipsis">
                                    {{ equipe.name }}
                                </div>
                            </div>
                            <div class="row items-center no-wrap text-grey">
                                <q-icon name="fas fa-star" />
                                <p class="q-ma-none q-ml-sm">{{ equipe.captain.firstName }} {{ equipe.captain.lastName }}</p>
                            </div>
                        </q-card-section>
                    </div>
                    <q-separator />
                    <q-card-actions>
                        <q-btn flat @click="openModalToGetInformations(equipe)" class="dijiwalk-secondary" round icon="fas fa-info-circle" />
                        <q-btn flat @click="openModalToGetInformations(equipe)" class="dijiwalk-secondary" color="primary text-bold">
                            Informations
                        </q-btn>
                    </q-card-actions>
                </q-card>
            </div>
        </div>
        <q-dialog v-model="confirm">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="col-2">
                        <q-avatar icon="fas fa-exclamation-triangle" color="negative" text-color="white" size="70px" />
                    </div>
                    <div class="row col-10">
                        <span>Êtes-vous sûr de vouloir supprimer l'équipe {{ messageDeleteTeam }}</span>
                        <span class="text-caption text-negative q-mt-sm" color="negative" style="font-size: 10px;">{{ messageBonus }}</span>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" color="negative" @click="deletedTeam" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-model="manageTeam">
            <q-card>
                <q-card-section class="row items-center">
                    <q-input v-if="isEditing" v-model="idTeam" type="hidden" />
                    <q-input ref="name" class="col-12 q-my-xs" label="Nom de l'équipe *" color="primary" v-model="nameTeam" name="nameTeam" id="nameTeam" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un nom.']">
                        <template v-slot:prepend>
                            <q-icon name="fas fa-users" />
                        </template>
                    </q-input>
                    <div class="row items-center justify-center col-12">
                        <q-select class="col-10 q-my-xs" multiple use-input fill-input ref="participants" label="Participants" v-model="participantTeam" name="participantTeam" id="participantTeam" option-value="id" :option-label="(item) => item === null ? 'Pas de participant' : item.firstName + ' ' + item.lastName" :options="participantsOptions" lazy-rules :rules="[val => val != null && val.length > 0 || 'Veuillez renseigner au minimum un participant']" @filter="filterParticipants" hint="Vous pouvez en rajouter un (bouton à droite) !" @input="changeSelection">
                            <template v-slot:option="scope">
                                <q-item v-bind="scope.itemProps" v-on="scope.itemEvents">
                                    <q-avatar>
                                        <q-img :src="scope.opt.picture" style="width:50px; height: 50px;"/>
                                    </q-avatar>
                                    <q-item-section class="q-ml-sm">
                                        <q-item-label v-html="scope.opt.lastName" />
                                        <q-item-label caption>{{ scope.opt.firstName }}</q-item-label>
                                    </q-item-section>
                                </q-item>
                            </template>
                        </q-select>
                        <div class="col-1 q-ml-md">
                            <q-btn color="primary" @click="navigateTo('/participant')" rounded icon="fas fa-plus" />
                        </div>
                    </div>
                    <q-select class="col-12 q-my-xs" ref="captain" label="Capitaine" v-model="captainTeam" name="captainTeam" id="captainTeam" option-value="id" :option-label="(item) => item === null ? 'Pas de capitaine' : item.firstName + ' ' + item.lastName" :options="captainAvailable" lazy-rules :rules="[val => val != null || 'Veuillez renseigner un capitaine.']" @filter="filterCaptain" options-selected-class="text-primary">
                        <template v-slot:option="scope">
                            <q-item v-bind="scope.itemProps" v-on="scope.itemEvents">
                                <q-avatar>
                                    <q-img :src="scope.opt.picture" style="width:50px; height: 50px;" />
                                </q-avatar>
                                <q-item-section class="q-ml-sm">
                                    <q-item-label v-html="scope.opt.lastName" />
                                    <q-item-label caption>{{ scope.opt.firstName }}</q-item-label>
                                </q-item-section>
                            </q-item>
                        </template>
                    </q-select>
                </q-card-section>
                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="isEditing" label="Modifier" @click="updateTeam" color="secondary" />
                    <q-btn v-if="isAdding" label="Ajouter" @click="addedTeam" color="secondary" />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-if="equipeSelected !== null" v-model="informations">
            <q-card class="modal-informations">
                <q-card-section class="items-center q-pb-none">
                    <div class="row">
                        <div v-for="participant in equipeSelected.teamPlayers" v-bind:key="participant.id + '-equipe' + equipeSelected.id" class="col-6">
                            <q-img :src="participant.player.picture" class="rounded-borders q-mt-sm q-px-sm" :ratio="1" style="max-width: 250px; max-height: 250px;">
                                <div class="absolute-bottom text-subtitle1 text-center">
                                    <q-icon name="fas fa-star" v-if="participant.player.id == equipeSelected.captain.id" />
                                    {{ participant.player.firstName }} {{ participant.player.lastName }}
                                </div>
                            </q-img>
                        </div>
                    </div>
                    <h5 class="q-mt-sm q-mb-none">Équipe "{{ equipeSelected.name }}"</h5>
                </q-card-section>
                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

    </q-page>
</template>

<script>

    import TeamDataService from '../services/TeamDataService';
    import PlayerDataService from '../services/PlayerDataService';
    export default {
        name: 'equipe',
        data() {
            return {
                modelParcours: null,
                modelParticipants: null,

                participantsOptions: null,
                captainAvailable: null,

                equipes: null,
                participants: null,
                confirm: false,

                isEditing: false,
                isAdding: false,
                informations: false,

                manageTeam: false,
                messageDeleteTeam: null,
                messageBonus: "Si vous supprimer une équipe, cela supprimera ses liens avec les participants !",
                deleteTeam: null,

                equipeSelected: null,
                participantTeam: [],
                captainTeam: null,
                nameTeam: null,

                idTeam: null
            }
        },
        created() {
            this.getAllTeams();
            this.getAllParticipants();
        },
        methods: {
            changeSelection() {
                this.captainTeam = null;
                this.captainAvailable = this.participantTeam
            },
            navigateTo(page) {
                this.$router.push(page)
            },
            openModalToGetInformations(team) {
                this.isAdding = false;
                this.isEditing = false;
                this.equipeSelected = team;
                this.informations = true;
            },

            openModalToAdd() {
                this.isEditing = false;
                this.isAdding = true;
                this.resetInput();
                this.manageTeam = true;
            },

            openModalToEdit(team) {
                this.isEditing = true;
                this.isAdding = false;
                this.resetInput();
                this.fillForm(team);
                this.manageTeam = true
            },
            resetInput() {
                this.idTeam = null
                this.participantTeam = []
                this.captainTeam = null
                this.nameTeam = null
            },
            fillForm(team) {
                this.equipeSelected = team
                this.nameTeam = team.name
                this.idTeam = team.id
                var self = this;
                team.teamPlayers.forEach(function (item) {
                    self.participantTeam.push(item.player)
                });
                this.captainTeam = team.captain;
            },
            getAllTeams() {
                this.$q.loading.show()
                if (this.equipes === null) {
                    TeamDataService.getAll().then(response => {
                        this.equipes = response.data;
                        this.$q.loading.hide()
                    }).catch();
                }
            },
            getAllParticipants() {
                if (this.participants === null) {
                    PlayerDataService.getAll().then(response => {
                        this.participants = response.data;
                    }).catch();
                }
            },
            openModalToDelete(team) {
                this.confirm = true;
                this.messageDeleteTeam = team.name;
                this.deleteTeam = team.id;
            },
            addedTeam() {
                if (this.$refs.name.validate() && this.$refs.participants.validate() && this.$refs.captain.validate()) {
                    this.$q.loading.show()
                    var teamPlayers = [];
                    this.participantTeam.forEach((function (item) {
                        teamPlayers.push({ IdTeam: 0, IdPlayer: item.id });
                    }).bind(this))
                    TeamDataService.create({
                        Name: this.nameTeam,
                        Captain: this.captainTeam,
                        TeamPlayers: teamPlayers
                    }).then(response => {
                       this.$q.loading.hide()
                        if (response.data.status == 1) {
                            this.manageTeam = false;
                            this.equipes.push(response.data.response);

                            this.$q.notify({
                                icon: 'fas fa-check-square',
                                color: 'secondary',
                                message: response.data.message,
                                position: 'top'
                            })
                        } else {
                            var message = response.data.message;
                            this.$q.notify({
                                icon: 'fas fa-exclamation-triangle',
                                color: 'negative',
                                message: message,
                                position: 'top'
                            })
                        }
                    })
                }
            },
            updateTeam() {
                if (this.$refs.name.validate() && this.$refs.participants.validate() && this.$refs.captain.validate()) {
                   this.$q.loading.show()
                    var teamPlayers = [];
                    this.participantTeam.forEach((function (item) {
                        teamPlayers.push({ IdTeam: this.idTeam, IdPlayer: item.id });
                    }).bind(this))
                    TeamDataService.update(this.idTeam, {
                        Name: this.nameTeam,
                        Captain: this.captainTeam,
                        TeamPlayers: teamPlayers
                    }).then(response => {
                        this.$q.loading.hide()
                        if (response.data.status == 1) {
                            this.manageTeam = false;
                            this.equipes[this.equipes.map(e => e.id).indexOf(this.equipeSelected.id)] = response.data.response
                            this.$q.notify({
                                icon: 'fas fa-check-square',
                                color: 'secondary',
                                message: response.data.message,
                                position: 'top'
                            })
                        } else {
                             this.manageTeam = false;
                            var message = response.data.message;
                            this.$q.notify({
                                icon: 'fas fa-exclamation-triangle',
                                color: 'negative',
                                message: message,
                                position: 'top'
                            })
                        }
                    })
                }
            },
            onReset() {
                this.nameTeam = null
                this.participantTeam = null
                this.captainTeam = null
            },
            deletedTeam() {
                var self = this;
                var id = self.deleteTeam;
                this.$q.loading.show()
                TeamDataService.delete(id).then(response => {
                    this.$q.loading.hide()
                    if (response.data.status == 1) {
                        self.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: response.data.message,
                            position: 'top'
                        })
                        self.equipes = self.equipes.filter(function (obj) {
                            return obj.id !== self.deleteTeam;
                        });
                    } else {
                        self.$q.notify({
                            message: response.data.message,
                            color: 'negative',
                            icon: 'fas fa-exclamation-triangle',
                            position: 'top'
                        })
                    }

                }).catch();
            },
            filterParticipants(val, update) {
                if (val === '') {
                    update(() => {
                        this.participantsOptions = this.participants
                    })
                    return
                }

                update(() => {
                    const needle = val.toLowerCase()
                    this.participantsOptions = this.participants.filter(v => v.firstName.toLowerCase().indexOf(needle) > -1 || v.lastName.toLowerCase().indexOf(needle) > -1)
                })
            },
            filterCaptain(val, update) {
                if (val === '') {
                    update(() => {
                        this.captainAvailable = this.participantTeam
                    })
                    return
                }

                update(() => {
                    const needle = val.toLowerCase()
                    this.captainAvailable = this.participantTeam.filter(v => v.firstName.toLowerCase().indexOf(needle) > -1 || v.lastName.toLowerCase().indexOf(needle) > -1)
                })
            },
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>