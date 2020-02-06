<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>


                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/' " />
                <q-toolbar-title>DijiWalk</q-toolbar-title>

                <div class="q-mr-md cursor-pointer">
                    <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-search" />
                    <q-menu>
                        <q-list bordered separator style="min-width: 100px">
                            <q-item>
                                <q-select filled
                                          v-model="modelJeu"
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par jeux"
                                          :options="jeuxOptions"
                                          @filter="filterJeux"
                                          style="width: 250px" />
                            </q-item>
                            <q-item>
                                <q-select filled
                                          v-model="modelEquipe"
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par équipe"
                                          :options="equipesOptions"
                                          @filter="filterEquipe"
                                          style="width: 250px" />
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>
            <div v-for="participant in participants" v-bind:key="participant.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <q-img :src="participant.picture" />

                    <q-card-section>
                        <q-btn @click="openModalToDelete(participant)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px; transform: translateY(-50%);" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ participant.firstName }} {{ participant.lastName }} / {{ participant.Login }}
                            </div>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <q-card-actions>
                        <q-btn flat round icon="fas fa-info-circle" />
                        <q-btn flat color="primary text-bold">
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
                    <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer ce {{ messageDeleteParticipant }} ?</span>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" color="negative" @click="deletedParticipant" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>
        <q-dialog v-model="addParticipant">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <div class="col-6">
                            <q-input label="Prénom" color="primary" option-value="id" option-label="name" v-model="prenomParticipant" name="prenomParticipant" id="prenomParticipant" />
                        </div>
                        <div class="col-6">
                            <q-input label="Nom" color="primary" option-value="id" option-label="name" v-model="nomParticipant" name="nomParticipant" id="nomParticipant" />
                        </div>
                        <div class="col-12">
                            <q-input label="Login" color="primary" option-value="id" option-label="name" v-model="loginParticipant" name="loginParticipant" id="loginParticipant" />
                        </div>
                        <div class="col-6">
                            <q-input label="Mot de passe" v-model="passwordParticipant" filled :type="isPwd ? 'password' : 'text'" name="passwordParticipant" id="passwordParticipant">
                                <template v-slot:append>
                                    <q-icon :name="isPwd ? 'visibility_off' : 'visibility'"
                                            class="cursor-pointer"
                                            @click="isPwd = !isPwd" />
                                </template>
                            </q-input>
                        </div>
                        <div class="col-6">
                            <q-input label="Mot de passe de confirmation" v-model="passwordConfirmParticipant" filled :type="isPwd ? 'password' : 'text'" name="passwordConfirmParticipant" id="passwordConfirmParticipant">
                                <template v-slot:append>
                                    <q-icon :name="isPwd ? 'visibility_off' : 'visibility'"
                                            class="cursor-pointer"
                                            @click="isPwd = !isPwd" />
                                </template>
                            </q-input>
                        </div>
                        <div class="col-12">
                            <q-input label="Email" color="primary" option-value="id" option-label="name" v-model="emailParticipant" name="emailParticipant" id="emailParticipant" />
                        </div>
                    </div>
                </q-card-section>
                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Ajouter" color="primary" @click="addParticipant()" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

    </q-page>
</template>

<script>

    import axios from 'axios'

    export default {
        name: 'participant',
        data() {
            return {
                modelEquipe: null,
                modelJeu: null,
                equipesOptions: null,
                jeuxOptions: null,
                participants: null,
                confirm: false,
                addParticipant: false,
                messageDeleteParticipant: null,
                deleteParticipant: null,
                prenomParticipant: null,
                nomParticipant: null,
                loginParticipant: null,
                passwordParticipant: null,
                passwordConfirmParticipant: null,
                emailParticipant: null
            }
        },
         created() {

            axios.get("api/player").then(resp => {
                this.participants = resp.data
            });

        },
        methods: {
             openModalToAdd () {
                this.addParticipant = true
            },
             openModalToDelete(participant) {
                this.confirm = true;
                this.messageDeleteParticipant = participant.firstName + " " + participant.lastName;
                this.deleteParticipant = participant.id;
            },
            // addGame() {

            //    GameDataService.create({
            //        IdRoute: this.parcoursGame.id,
            //        CreationDate: this.dateGame
            //    }).then(response => {
            //        this.games.push(response.data);
            //        PlayDataService.create({
            //            IdGame: response.data.id,
            //            IdTeam: this.equipeGame.id
            //        }).then(response => {
            //            console.log(response);
            //        }).catch(reason => {
            //            console.log(reason);
            //        });


            //    }).catch(reason => {
            //        console.log(reason);
            //    });
            //},
            deletedParticipant() {
                axios.delete("api/player/" + this.deleteParticipant).then(resp => {
                    if (resp.data.status == 1) {
                        var self = this;
                        this.participants = this.participants.filter(function (obj) {
                            return obj.id !== self.deleteParticipant;
                        });
                    } else {
                        this.$q.notify({
                            message: resp.data.message,
                            color: 'negative',
                            icon: 'fas fa-exclamation-triangle',
                            position: 'top'
                        })
                    }
                })
            },
            filterEquipe(val, update) {
                update(() => {
                    if (val === '') {
                        this.equipesOptions = this.equipes
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.equipesOptions = this.equipes.filter(
                            v => v.toLowerCase().indexOf(needle) > -1
                        )
                    }
                })
            },
            filterJeux(val, update) {
                update(() => {
                    if (val === '') {
                        this.jeuxOptions = this.jeux
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.jeuxOptions = this.jeux.filter(
                            v => v.toLowerCase().indexOf(needle) > -1
                        )
                    }
                })
            }
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>