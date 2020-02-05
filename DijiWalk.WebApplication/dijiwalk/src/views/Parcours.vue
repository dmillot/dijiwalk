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
                                          v-model="modelStep"
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par étapes"
                                          :options="stepsOptions"
                                          @filter="filterSteps"
                                          style="width: 250px" />
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="addParcours = true" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
                    <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
                </q-card>
            </div>
            <div v-for="parcour in parcours" v-bind:key="parcour.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />

                    <q-card-section>
                        <q-btn @click="onDeleteParcours(parcour)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px; transform: translateY(-50%);" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ parcour.name }}
                            </div>
                        </div>
                        <div class="row items-center no-wrap text-grey">
                            <q-icon name="fas fa-calendar" />
                            <p class="q-ma-none q-ml-xs">{{ parcour.time }}</p>
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
                    <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer {{ messageDeleteParcours }} ?</span>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" @click="deletedParcours" color="negative" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-model="addParcours">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <div class="col-12">
                            <q-input v-model="text" label="Nom" />
                        </div>
                        <div class="col-12">
                            <q-input color="primary" v-model="date" type="date" />
                        </div>
                        <div class="col-8">
                            <q-select v-model="model" :options="stepsOptions" label="Steps" />
                        </div>
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

    const listeJeux = [
        { id: 1, name: "Jeu 1", date: "30/02/2020" },
        { id: 2, name: "Jeu 2", date: "30/03/2020" },
        { id: 3, name: "Jeu 3", date: "30/04/2020" },
        { id: 4, name: "Jeu 4", date: "30/05/2020" },
        { id: 5, name: "Jeu 5", date: "30/06/2020" },
        { id: 6, name: "Jeu 6", date: "30/07/2020" },
        { id: 7, name: "Jeu 7", date: "30/08/2020" },
    ]

    const listeEtape = [
        { id: 1, name: "Etape 1", link: "https://ekladata.com/Q33WRkQK-cgPuG5zGUU7dTqOEpA.jpg", date: "30/02/2020" },
        { id: 2, name: "Etape 2", link: "https://www.cap-voyage.com/wp-content/uploads/2015/11/Dijon-C%C3%B4te-dOr-Patrimoine.jpg", date: "30/03/2020" },
        { id: 4, name: "Etape 3", link: "https://lh5.googleusercontent.com/proxy/YqT0wR_8t7yT5PRq0Z9Ha7ThARpzQ6JFQHsYuIC6jJ5Ox2elNpEzKKr-_Q9d39vVgr8gHvuEUgECj11bY95ElK5XhcshkeBhDMBjBbgPWyxeCFlzn0-zy5om7-6UtDHvqRlKgZn_bixF", date: "30/05/2020" },
        { id: 5, name: "Etape 4", link: "https://blog.ruedesvignerons.com/wp-content/uploads/2018/01/Halle-de-dijon-blog.png", date: "30/06/2020" },
        { id: 3, name: "Etape 5", link: "https://www.macommune.info/wp-content/uploads/2015/04/main-square-in-dijon.jpg", date: "30/04/2020" },
        { id: 6, name: "Etape 6", link: "https://www.cityzeum.com/media/poi/small/383848.jpg", date: "30/06/2020" }
    ]
    export default {
        name: 'parcours',
        data() {
            return {
                modelJeu: null,
                modelStep: null,
                stepsOptions: listeEtape,
                jeuxOptions: listeJeux,
                parcours: null,
                confirm: false,
                addParcours: false,
                messageDeleteParcours: null,
                deleteParcours: null

            }
        },
        created() {

            axios.get("api/route").then(resp => {
                this.parcours = resp.data
            });

        },
        methods: {

            onDeleteParcours(parcour) {
                this.confirm = true;
                this.messageDeleteParcours = parcour.name;
                this.deleteParcours = parcour.id;
            },
            deletedParcours() {
                axios.delete("api/route/" + this.deleteParcours).then(resp => {
                    if (resp.data.status == 1) {
                        var self = this;
                        this.parcours = this.parcours.filter(function (obj) {
                            return obj.id !== self.deleteParcours;
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
            filterJeux(val, update) {
                update(() => {
                    if (val === '') {
                        this.jeuxOptions = listeJeux
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.jeuxOptions = listeJeux.filter(
                            v => v.toLowerCase().indexOf(needle) > -1
                        )
                    }
                })
            },
            filterSteps(val, update) {
                update(() => {
                    if (val === '') {
                        this.stepsOptions = listeEtape
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.stepsOptions = listeEtape.filter(
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