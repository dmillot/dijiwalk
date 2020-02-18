<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar class="bg-toolbar">
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/' " />
                <q-toolbar-title>DijiWalk</q-toolbar-title>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>

            <div v-for="transport in transports" v-bind:key="transport.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <!--<q-img><i class={{image[transport.id - 1]}}></i></q-img>-->
                    
                    <q-card-section>
                        <q-btn @click="openModalToDelete(transport)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px; transform: translateY(+50%);" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ transport.libelle }}
                            </div>
                        </div>
                        <div class="row items-center no-wrap text-grey">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ transport.description }}
                            </div>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <q-card-actions>
                        <q-btn flat round icon="fas fa-info-circle" />
                        <q-btn flat @click="addTransport = true; show=true; fillForm(transport)" color="primary text-bold">
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
                    <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer le transport {{ messageDeleteTransport }} ?</span>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" @click="deletedTransport" color="negative" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>




        <q-dialog v-model="addTransport">
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
                            <q-input v-model="libelle" label="Libelle" />
                        </div>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="show" label="Modifier" @click="updateTransport" color="primary" v-close-popup />
                        <q-btn flat v-else label="Ajouter" @click="addedTransport" color="primary" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

    </q-page>
</template>








<script>

    const parcours = [
        'Parcours 1', 'Parcours 2', 'Parcours 3', 'Parcours 4', 'Parcours 5'
    ]
    
    const icons = [
        "fas fa-walking", "fas fa-subway", "fas fa-biking", "fas fa-bus", "fas fa-transporter" , "fas fa-skating", "fas fa-dolly-flatbed-empty", "fas fa-taxi", "fas fa-dolly-empty" 
    ]
    

    //import axios from 'axios'
    import TransportDataService from "@/services/TransportDataService";
    import RouteDataService from "@/services/RouteDataService";


    export default {
        name: 'transport',
        data() {
            return {
                addTransport: false,
                messageDeleteTransport: null,
                routesFiltered: null,
                routes: null,
                routesFilterModel: null,
                confirm: false,
                transport: null,
                deleteTransport: null,
                transports: null,
                description: '',
                libelle: '',
                image: icons,
                id: 0
            }
        },
        created: function () {
            this.getAllRoutes();
            this.getAllTransport();
        },

        methods: {
            
            getAllTransport () {
                if (this.transports === null) {
                    TransportDataService.getAll().then(response => {
                        this.transports = response.data;
                    }).catch();
                }
            },
            
            getAllRoutes () {
                if (this.routes === null) {
                    RouteDataService.getAll().then(response => {
                        this.routes = response.data;
                    }).catch();
                }
            },

            fillForm(transport) {
                this.description = transport.description;
                this.libelle = transport.libelle;
                this.id = transport.id;
            },

            

            openModalToAdd() {
                this.show = false;
                this.addTransport = true;
            },
            
            openModalToUpdate() {
                this.show = false;
                this.addTransport = true;
                
                this.transport = {
                    Description: this.description,
                    Libelle: this.libelle,
                };
            },

            openModalToDelete(transport) {
                this.confirm = true;
                this.messageDeleteTransport = transport.libelle;
                this.deleteTransport = transport.id;
            },

            async updateTransport() {

                this.transport = {
                    Description: this.description,
                    Libelle: this.libelle
                };

                await TransportDataService.update(this.id, this.transport).then(response => {
                    this.transports.update(response.data);
                }).catch();
            },
                            
            async addedTransport() {
                
                this.transport = {
                    Description: this.description,
                    Libelle: this.libelle,
                };

                await TransportDataService.create(this.transport
                ).then(response => {
                    this.transports.push(response.data);
                }).catch();


                //this.getAllTransport();
            },

            deletedTransport() {

                
                var id = this.deleteTransport;
                TransportDataService.delete(this.deleteTransport).then(() => {

                    this.transports = this.transports.filter(function (obj) {
                        return obj.id !== id;
                    });
                }).catch();
                                             
                //axios.delete('api/transport/' + this.deleteTransport).then(resp => {
                //    if (resp.status == 200) {
                //        var self = this;
                //        this.transports = this.transports.filter(function (obj) {
                //            return obj.id !== self.deleteTransport;
                //        });
                //    }
                //});
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