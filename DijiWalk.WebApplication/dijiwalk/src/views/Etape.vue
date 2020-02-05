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
                <q-card @click="addStep = true" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
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
                        <div class="col-12">
                            <q-input v-model="Description" label="Description" />
                        </div>
                        <div class="col-12">
                            <q-input v-model="Validation" label="Validation" />
                        </div>
                        <div class="col-12">
                            <q-input color="primary" v-model="CreationDate" type="date" />
                        </div>
                        <div class="col-12">
                            <q-input v-model="Name" label="Nom" />
                        </div>
                        <div class="col-12">
                            <q-input v-model.number="Lat" label="Latitude" />
                        </div>
                        <div class="col-12">
                            <q-input v-model.number="Lng" label="Longitude" />
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

    const parcours = [
        'Parcours 1', 'Parcours 2', 'Parcours 3', 'Parcours 4', 'Parcours 5'
    ]

    import axios from 'axios'
    export default {
        name: 'etape',
        data() {
            return {
                modelParcours: null,
                parcoursOptions: parcours,
                addStep: false,
                messageDeleteStep: null,
                confirm: false,
                step: null,
                deleteStep: null,
                etapes: null
            }
        },
        created: function () {
            axios.get('api/step').then(resp => {
                this.etapes = resp.data;
            });

        },

        methods: {

            addedStep() {
                this.step = {
                    Description: '',
                    Validation: '',
                    CreationDate: Date.now,
                    Name: '',
                    Lat: 0,
                    Lng: 0
                };
                axios.post('api/step/' + this.step);
                this.etapes.addStep(this.step);
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