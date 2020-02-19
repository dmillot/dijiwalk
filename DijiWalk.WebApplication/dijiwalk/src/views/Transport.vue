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
                    <div @click="openModalToEdit(transport)" >
                        <q-card-section>
                            <q-btn v-on:click.stop="openModalToDelete(transport)"
                                   fab
                                   color="negative"
                                   icon="fas fa-trash"
                                   class="absolute"
                                   style="top: 0; right: 12px; transform: translateY(+50%); z-index: 999;" />

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
                    </div>

                    <q-separator />

                    <q-card-actions>
                        <q-btn flat @click="openModalToGetInformations(transport)" class="dijiwalk-secondary" round icon="fas fa-info-circle" />
                        <q-btn flat @click="openModalToGetInformations(transport)" class="dijiwalk-secondary" color="primary text-bold">
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
                        <q-avatar icon="fas fa-exclamation-triangle" color="negative" text-color="white" />
                    </div>
                    <div class="row col-10">
                        <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer le transport {{ messageDeleteTransport }} ?</span>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" @click="deletedTransport" color="negative" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>




        <q-dialog v-model="manageTransport">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <q-input v-if="isEditing" v-model="idTransport" name="" type="hidden" />
                        <q-input ref="libelle" class="col-6 q-pl-sm q-my-xs" label="Libelle *" color="primary" option-value="id" option-label="name" v-model="nomTransport" name="nomTransport" id="nomTransport" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un libelle.']">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-address-card" />
                            </template>
                        </q-input>
                        <q-input ref="description" class="col-6 q-pl-sm q-my-xs" label="Description *" color="primary" option-value="id" option-label="name" v-model="descriptionTransport" name="descriptionTransport" id="descriptionTransport" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner une description.']">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-address-card" />
                            </template>
                        </q-input>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="isEditing" label="Modifier" @click="updateTransport" color="secondary" />
                    <q-btn v-if="isAdding" label="Ajouter" @click="addedTransport" color="secondary" />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-if="transportSelected !== null" v-model="informations">
            <q-card class="modal-informations">
                <q-card-section class="items-center">
                    <h5 class="q-my-sm">{{ transportSelected.libelle }}</h5>
                    <p>{{ transportSelected.description }}</p>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

    </q-page>
</template>


<script>
    //import axios from 'axios'
    import TransportDataService from "@/services/TransportDataService";
    
    export default {
        name: 'transport',
        data() {
            return {
                addTransport: false,
                messageDeleteTransport: null,
                confirm: false,
                deleteTransport: null,
                transports: null,
                transport: null,
                description: '',
                show: false,
                libelle: '',
                manageTransport: null,
                nomTransport: null,
                descriptionTransport: null,
                transportSelected: null,
                isEditing: false,
                isAdding: false,
                informations: false,
                idTransport: null
            }
        },
        created: function () { 
            this.getAllTransport();
        },

        methods: {

            getAllTransport() {
                this.$q.loading.show()
                if (this.transports === null) {
                    TransportDataService.getAll().then(response => {
                        this.transports = response.data;
                        this.$q.loading.hide()
                    }).catch();
                }
            },


            openModalToGetInformations(transport) {
                this.isAdding = false;
                this.isEditing = false;
                this.transportSelected = transport;
                this.informations = true;
            },

            openModalToAdd() {
                this.isEditing = false;
                this.isAdding = true;
                this.resetInput();
                this.manageTransport = true;
            },

            openModalToEdit(transport) {
                this.isEditing = true;
                this.isAdding = false;
                this.resetInput();
                this.fillForm(transport);
                this.manageTransport = true
            },
            resetInput() {
                this.nomTransport = null
                this.descriptionTransport = null

            },
            fillForm(transport) {
                this.transportSelected = transport

                this.nomTransport = transport.libelle
                this.descriptionTransport = transport.description
                this.idTransport = transport.id
            },
            onResetValidation() {
                this.errorlibelle = false
                this.errordescription= false
            },

            openModalToDelete(transport) {
                this.confirm = true;
                this.messageDeleteTransport = transport.libelle;
                this.deleteTransport = transport.id;
            },

            updateTransport() {
                this.$q.loading.show();

                this.transport = {
                    Description: this.descriptionTransport,
                    Libelle: this.nomTransport,
                    Id: this.idTransport
                };
                TransportDataService.update(this.idTransport, this.transport).then(response => {
                    this.$q.loading.hide()
                    if (response.data.status == 1) {
                        this.manageTransport = false;
                        this.onResetValidation();
                        this.transports[this.transports.map(e => e.id).indexOf(this.transportSelected.id)] = response.data.response;
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
                        setTimeout(this.onResetValidation, 3000);
                    }
                }).catch();
            },

            addedTransport() {
                if (this.$refs.libelle.validate() && this.$refs.description.validate()) {

                    this.$q.loading.show()
                    this.transport = {
                        Description: this.descriptionTransport,
                        Libelle: this.nomTransport,
                    };

                    TransportDataService.create(this.transport
                    ).then(response => {
                        this.$q.loading.hide()
                        if (response.data.status == 1) {
                            this.manageTransport = false;
                            this.onResetValidation();
                            this.transports.push(response.data.response);

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
                            setTimeout(this.onResetValidation, 3000);
                        }
                    })
                }
            },

            deletedTransport() {

                this.$q.loading.show()
                var id = this.deleteTransport;
                TransportDataService.delete(this.deleteTransport).then(response => {
                    this.$q.loading.hide()
                    if (response.data.status == 1) {
                        this.manageTransport = false;
                        this.transports = this.transports.filter(function (obj) {
                            return obj.id !== id;
                        });
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
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>