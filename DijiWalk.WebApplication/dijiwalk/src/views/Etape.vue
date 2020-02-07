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
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="addStep = true; show=false" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>

            <div v-for="etape in etapes" v-bind:key="etape.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <q-img :src="etape.link" />

                    <q-card-section>
                        <q-btn @click="onDeleteStep(etape)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px;" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ etape.name }}
                            </div>
                        </div>
                        <div class="row items-center no-wrap text-grey">
                            <q-icon name="fas fa-calendar" />
                            <p class="q-ma-none q-ml-xs">{{ etape.creationDate }}</p>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <q-card-actions>
                        <q-btn flat round icon="fas fa-info-circle" />
                        <q-btn flat @click="addStep = true; show=true; fillForm(etape)" color="primary text-bold">
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
                    <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer l'étape {{ messageDeleteStep }} ?</span>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" @click="deletedStep" color="negative" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>




        <q-dialog v-model="addStep">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <div style="visibility:hidden" class="col-12">
                            <q-input v-model="id" label="id" />
                        </div>
                        <div class="col-12">
                            <q-input v-model="description" label="Description" />
                        </div>
                        <div class="col-12">
                            <q-input v-model="validation" label="Validation" />
                        </div>
                        <div class="col-12">
                            <q-input color="primary" v-model="creationDate" type="date" />
                        </div>
                        <div class="col-12">
                            <q-input v-model="name" label="Nom" />
                        </div>
                        <div class="col-12">
                            <q-input v-model.number="lat" type="number" label="Latitude" />
                        </div>
                        <div class="col-12">
                            <q-input v-model.number="lng" type="number" label="Longitude" />
                        </div>

                        <div class="col-8">
                            <q-select v-model="modelMission" option-value="id" option-label="name" :options="missionsOptions" label="Missions" id="missionEtape" name="missionsEtape" />
                        </div>

                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="show" label="Modifier" @click="updateStep" color="primary" v-close-popup />
                        <q-btn flat v-else label="Ajouter" @click="addedStep" color="primary" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

    </q-page>
</template>








<script>

    const parcours = [
        'Parcours 1', 'Parcours 2', 'Parcours 3', 'Parcours 4', 'Parcours 5'
    ]
    
    const missions = [
        'mission 1', 'Mission 2', 'Mission 3', 'Mission 4'
    ]
    

    import axios from 'axios'
    export default {
        name: 'etape',
        data() {
            return {
                modelParcours: null,
                parcoursOptions: parcours,
                modelMission: null,
                missionsOptions: null,
                addStep: false,
                messageDeleteStep: null,
                confirm: false,
                step: null,
                deleteStep: null,
                etapes: null,
                description: '',
                validation: '',
                creationDate: '',
                missions: null,
                name: '',
                lat: '',
                lng: '',
                id: 0
            }
        },
        created: function () {
            axios.get('api/step').then(resp => {
                this.etapes = resp.data;
            });
            
            axios.get('api/mission').then(resp => {
                this.missionsOptions = resp.data;
            });
        },

        methods: {

            fillForm(etape) {
                this.description = etape.description;
                this.validation = etape.validation;
                this.creationDate = etape.creationDate;
                this.name = etape.name;
                this.lat = etape.lat;
                this.lng = etape.lng;
                this.id = etape.id;
            },

            async updateStep() {

                this.step = {
                    Description: this.description,
                    Validation: this.validation,
                    CreationDate: this.creationDate,
                    Name: this.name,
                    Lat: this.lat,
                    Lng: this.lng
                };

                await axios.put('api/step/' + this.id + '/', this.step);

                axios.get('api/step').then(resp => {
                    this.etapes = resp.data;
                });
            },
                            
            addedStep() {
                this.step = {
                    Description: this.description,
                    Validation: this.validation,
                    CreationDate: this.creationDate,
                    Name: this.name,
                    Lat: this.lat,
                    Lng: this.lng
                };

                axios.post('api/step/', this.step).then(response => {
                    this.etapes.push(response.data);
                }).catch(reason => {
                    console.log(reason);
                });

                axios.get('api/step').then(resp => {
                    this.etapes = resp.data;
                });
            },

            onDeleteStep(etape) {
                this.confirm = true;
                this.messageDeleteStep = etape.name;
                this.deleteStep = etape.id;
            },

            deletedStep() {
                axios.delete('api/step/' + this.deleteStep).then(resp => {
                    if (resp.status == 200) {
                        var self = this;
                        this.etapes = this.etapes.filter(function (obj) {
                            return obj.id !== self.deleteStep;
                        });
                    }
                });
            },

            async filterMission(val, update) {
                update(() => {
                    if (val === '') {
                        this.missionsOptions = missions;
                    }
                    else {
                        const needle2 = val.toLowerCase()
                        this.missionsOptions.filter(
                            v => v.toLowerCase().indexOf(needle2) > -1
                        )
                    }
                })
            },

            filterParcours(val, update) {
                update(() => {
                    if (val === '') {
                        this.parcoursOptions = parcours
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.parcoursOptions = parcours.filter(
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