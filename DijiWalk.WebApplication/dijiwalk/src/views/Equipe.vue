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
                                          v-model="modelParcours"
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par parcours"
                                          :options="parcoursOptions"
                                          @filter="filterParcours"
                                          style="width: 250px" />
                            </q-item>
                            <q-item>
                                <q-select filled
                                          v-model="modelParticipants"
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par participants"
                                          :options="participantsOptions"
                                          @filter="filterParticipants"
                                          style="width: 250px" />
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
                    <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
                </q-card>
            </div>
            <div v-for="equipe in equipes" v-bind:key="equipe.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <div class="row">
                        <div v-for="participant in equipe.teamPlayers" v-bind:key="participant.id" class="col-6">
                                <q-img class="flex" :src="participant.player.picture"
                                       style="width: 200px; height: 200px;" />
                        </div>
                    </div>

                    <q-card-section>
                        <q-btn 
                               @click="onDeleteTeam(equipe)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px; transform: translateY(-50%);" />

                        <q-btn fab
                               color="negative"
                               icon="fas fa-edit"
                               class="absolute"
                               style="top: 0; right: 100px; transform: translateY(-50%);" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ equipe.name }}
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
                    <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer {{ messageDeleteTeam }} ?</span>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" @click="deletedTeam" color="negative" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-model="addTeam">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        
                        <div class="col-4 row justify-center items-center">
                            <q-btn color="primary" label="Ajouter" />
                        </div>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Ajouter" color="primary" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>
    </q-page>
</template>

<script>

    import axios from 'axios'
    export default {
        name: 'equipe',
        data() {
            return {
                modelParcours: null,
                modelParticipants: null,
                parcoursOptions: null,
                participantsOptions: null,
                equipes: null,
                confirm: false,
                addTeam: false,
                messageDeleteTeam: null,
                deleteTeam: null
            }
        },
        created() {

            axios.get("api/team").then(resp => {
                this.equipes = resp.data
            });

        },
        methods: {
            // navigateToJeu () {
            //   this.$router.push({ name: 'home' })
            // },
            onDeleteTeam(equipe) {
                this.confirm = true;
                this.messageDeleteTeam = equipe.name;
                this.deleteTeam = equipe.id;
            },
            deletedTeam() {
                axios.delete("api/team/" + this.deleteTeam).then(resp => {
                    if (resp.data.status == 1) {
                        var self = this;
                        this.equipes = this.equipes.filter(function (obj) {
                            return obj.id !== self.deleteTeam;
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
            filterParcours(val, update) {
                update(() => {
                    if (val === '') {
                        this.parcoursOptions = this.parcours
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.parcoursOptions =  this.parcours.filter(
                            v => v.toLowerCase().indexOf(needle) > -1
                        )
                    }
                })
            },
            filterParticipants(val, update) {
                update(() => {
                    if (val === '') {
                        this.participantsOptions =  this.participants
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.participantsOptions =  this.participants.filter(
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