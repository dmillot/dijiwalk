<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>

                <q-toolbar-title>DijiWalk</q-toolbar-title>

                <div class="q-ml-md cursor-pointer non-selectable">
                    <q-icon name="fas fa-search" />
                    <q-menu>
                        <q-list bordered separator style="min-width: 100px">
                            <!--<q-item>
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
                            </q-item>-->
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
                    <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
                </q-card>
            </div>
            <div v-for="parcour in parcours" v-bind:key="parcour.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <div @click="openModalToEdit(parcour)" class="game-card">
                        <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />

                        <q-card-section>
                            <q-btn @click="openModalToDelete(parcour)"
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
                                <q-icon name="fas fa-clock" />
                                <p class="q-ma-none q-ml-xs">{{ parcour.time | formatTime }}</p>
                                <q-icon v-if="parcour.handicap" class="q-ml-md" name="fas fa-wheelchair" color="negative" />
                            </div>
                        </q-card-section>
                    </div>
                    <q-separator />

                    <q-card-actions>
                        <q-btn flat @click="openModalToGetInformations(parcour)" class="dijiwalk-secondary" round icon="fas fa-info-circle" />
                        <q-btn @click="openModalToGetInformations(parcour)" class="dijiwalk-secondary" flat color="primary text-bold">
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
                        <span>Êtes-vous sûr de vouloir supprimer le parcours {{ messageDeleteParcours }} ?</span>
                        <span class="text-caption text-negative q-mt-sm" color="negative" style="font-size: 10px;">{{ messageBonus }}</span>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" @click="deletedParcours" color="negative" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-model="manageParcours">
            <q-card>
                <transition name="fade">
                    <div id="modalManage" v-show="loading"></div>
                </transition>
                <q-circular-progress v-show="loading" indeterminate size="100px" :thickness="0.22" color="negative" track-color="grey-3" class="absolute-center" />

                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <q-input v-if="isEditing" v-model="idParcours" type="hidden" />

                        <q-input ref="name" class="col-12 q-pr-sm q-my-xs" label="Intitulé *" color="primary" option-value="id" option-label="name" v-model="nameParcours" name="nameParcours" id="nameParcours" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un intitulé.']">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-address-card" />
                            </template>
                        </q-input>

                        <q-input class="col-12 q-my-xs" ref="description" v-model="descriptionParcours" autogrow type="textarea" label="Description *" option-value="id" option-label="name" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner une description.']" name="descriptionParcours" id="descriptionParcours">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-feather-alt" />
                            </template>
                        </q-input>

                        <q-input class="col-6 q-my-xs" ref="time" v-model="timeParcours" mask="time" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un temps prévu d\'estimation.']" :error-message="errormessagetime" :error="errortime" option-value="id" option-label="name" name="timeParcours" id="timeParcours">
                            <template v-slot:append>
                                <q-icon name="fas fa-clock" class="cursor-pointer">
                                    <q-popup-proxy transition-show="scale" transition-hide="scale">
                                        <q-time v-model="timeParcours"
                                                format24h />
                                    </q-popup-proxy>
                                </q-icon>
                            </template>
                        </q-input>

                        <q-toggle class="col-5 q-my-xs on-left" v-show="!loading" ref="handicap" v-model="handicapParcours" color="primary" icon="fas fa-wheelchair" label="Accès handicapé ?" option-value="id" option-label="name" name="handicapParcours" id="handicapParcours" />

                        <div class="row items-center justify-center col-12">
                            <q-select class="col-10 q-my-xs" ref="steps" clearable use-input fill-input v-model="stepSelected" multiple :options="stepsOptions" label="Étapes *" option-value="id" option-label="name" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez choisir une étape.']" @filter="filterStep" />
                            <div v-show="!loading" class="col-1 q-ml-md">
                                <q-btn color="primary" @click="navigateTo('/etape')" rounded icon="fas fa-plus" />
                            </div>
                        </div>

                        <div class="row justify-start">
                            <div v-for="tag in tagsParcours" v-bind:key="tag.idTag">
                                <q-chip outline removable class="q-my-xs q-pa-md" size="md" icon="fas fa-tag" @remove="removeTags(tag)">
                                    {{ tag.name }}
                                </q-chip>
                            </div>
                        </div>

                        <div class="row items-center justify-center col-12">
                            <q-select class="col-10 q-my-xs" ref="tags" v-model="tagSelected" :options="tagsAvailable" label="Tags" option-value="id" option-label="name" />
                            <div v-show="!loading" class="col-1 q-ml-md">
                                <q-btn color="primary" @click="navigateTo('/')" rounded icon="fas fa-plus" />
                            </div>
                        </div>
                    </div>
                </q-card-section>

                <q-card-actions v-show="!loading" align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="isEditing" label="Modifier" @click="updateParcours" color="secondary" />
                    <q-btn v-if="isAdding" label="Ajouter" @click="addedParcours" color="secondary" />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-if="parcoursSelected !== null" v-model="informations">
            <q-card class="modal-informations">
                <q-card-section class="items-center">
                    <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />
                    <h5 class="q-my-sm">{{ parcoursSelected.name }}</h5>
                    <p>{{ parcoursSelected.description }}</p>
                    <div class="row col-12" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                        <q-item>
                            <q-item-section avatar>
                                <q-icon color="grey" name="fas fa-clock" />
                            </q-item-section>
                            <q-item-section>{{ parcoursSelected.time | formatTime }}</q-item-section>
                        </q-item>
                    </div>
                    <div class="row col-12" v-if="parcoursSelected.handicap" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                        <q-item>
                            <q-icon size="md" class="q-mt-sm" name="fas fa-wheelchair" color="negative" />
                            <q-item-section class="q-ml-sm">Le parcours a des accés pour les handicapés.</q-item-section>
                        </q-item>
                    </div>
                    <div class="row col-12">
                        <q-list class="custom-expansion col-12">
                            <q-expansion-item expand-separator icon="fas fa-shoe-prints" label="Étapes">
                                <q-card class="card-expansion">
                                    <q-card-section v-for="step in parcoursSelected.routeSteps" v-bind:key="step.id" class="row items-center">
                                        <q-icon size="sm" name="fas fa-map-marker q-mr-sm" color="negative" />
                                        {{ step.step.name }}
                                    </q-card-section>
                                </q-card>
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
    import ParcoursDataService from '@/services/ParcoursDataService';
    import StepDataService from '@/services/StepDataService';
    import TagDataService from '@/services/TagDataService';

    export default {
        name: 'parcours',
        data() {
            return {

                manageParcours: false,
                messageDeleteParcours: null,
                messageBonus: "Si vous supprimer un parcours, cela supprimera uniquement ses informations !",

                confirm: false,
                isEditing: false,
                isAdding: false,
                informations: false,


                parcours: null,
                parcoursSelected: null,
                nameParcours: null,
                descriptionParcours: null,
                handicapParcours: null,
                timeParcours: null,
                tagsParcours: null,

                steps: null,
                stepSelected: null,
                stepsOptions: null,

                deleteParcours: null,
                errormessagetime: null,
                errortime: false,

                tags: null,
                tagSelected: null,
                tagsAvailable: null,

                idParcours: null,
                loading: false

            }
        },
        watch: {
            tagSelected: function (val) {
                if (val != null) {
                    this.tagsParcours.push(val)
                    var self = this;
                    self.filterTagAvailable(val).then(() => {
                        self.tagSelected = null;
                    });
                }

            }
        },
        created() {
            this.getAllParcours();
            this.getAllSteps();
        },
        filters: {
            formatTime: function (value) {
                if (!value) return ''
                var time = value.split(":")
                return `${time[0]} heure(s) et ${time[1]} minute(s)`
            }
        },
        methods: {
            navigateTo(page) {
                this.$router.push(page)
            },
            removeTags(tag) {
                this.tagsParcours = this.tagsParcours.filter(function (el) {
                    if (el.id != tag.id) return el;
                });
                this.tagsAvailable.push(tag);
            },
            resetInput() {
                this.idParcours = null
                this.nameParcours = null
                this.descriptionParcours = null
                this.handicapParcours = null
                this.timeParcours = null
                this.tagsParcours = null
                this.tagsAvailable = null
                this.tags = null
                this.stepSelected = null
            },
            openModalToGetInformations(parcour) {
                this.isAdding = false;
                this.isEditing = false;
                this.parcoursSelected = parcour;
                this.informations = true;
            },

            openModalToAdd() {
                this.isEditing = false;
                this.isAdding = true;
                this.resetInput();
                this.manageParcours = true;
            },

            openModalToEdit(parcour) {
                this.isEditing = true;
                this.isAdding = false;
                this.resetInput();
                this.fillForm(parcour);
                this.manageParcours = true
            },
            getAllSteps() {
                if (this.steps === null) {
                    StepDataService.getAll().then(response => {
                        this.steps = response.data;
                    }).catch();
                }
            },
            getAllParcours() {
                if (this.parcours === null) {
                    ParcoursDataService.getAll().then(response => {
                        this.parcours = response.data;
                    }).catch();
                }
            },
            filterTagAvailable(val) {
                return new Promise((resolve) => {
                    this.tagsAvailable = this.tagsAvailable.filter(function (el) {
                        if (el.id != val.id) return el;
                    });
                    resolve(this.tagsAvailable);
                })
            },
            getAllTags() {
                return new Promise((resolve) => {
                    if (this.tags === null) {
                        TagDataService.getAll().then(response => {
                            this.tags = response.data;
                            resolve(this.tags);
                        }).catch();
                    }
                })
            },
            async fillForm(parcour) {
                this.parcoursSelected = parcour;
                this.idParcours = parcour.id
                this.nameParcours = parcour.name;
                this.descriptionParcours = parcour.description;
                this.handicapParcours = parcour.handicap;
                this.timeParcours = parcour.time;
                this.stepSelected = parcour.routeSteps.map(function (t) {
                    return t.step;
                });
                this.tagsParcours = parcour.routeTags.map(function (t) {
                    return t.tag;
                });
                this.getAllTags().then(response => {
                    var tagParcours = this.tagsParcours.map(function (t) {
                        return t.id;
                    });
                    this.tagsAvailable = response.filter(function (el) {

                        if (!tagParcours.includes(el.id)) return el;
                    });
                });


            },
            updateParcours() {
                if (this.$refs.name.validate() && this.$refs.description.validate() && this.$refs.time.validate()) {
                    var timeSplit = this.timeParcours.split(":");
                    if (parseInt(timeSplit[0]) >= 0 && parseInt(timeSplit[0]) < 24 && parseInt(timeSplit[1]) >= 0 && parseInt(timeSplit[1]) < 59) {
                        this.loading = true;
                        var self = this;
                        var listeRouteTag = [];
                        this.tagsParcours.forEach(element => {
                            listeRouteTag.push({
                                idTag: element.id,
                                idRoute: self.idParcours
                            });
                        });
                        var listeRouteStep = [];
                        this.stepSelected.forEach(element => {
                            listeRouteStep.push({
                                idStep: element.id,
                                idRoute: self.idParcours
                            });
                        });
                        ParcoursDataService.update(this.idParcours, {
                            Name: this.nameParcours,
                            Description: this.descriptionParcours,
                            Handicap: this.handicapParcours,
                            Time: this.timeParcours,
                            RouteTags: listeRouteTag,
                            RouteSteps: listeRouteStep
                        }).then(response => {
                            this.loading = false;
                            if (response.data.status == 1) {
                                this.manageParcours = false;
                                this.onResetValidation();
                                this.parcours[this.parcours.map(e => e.id).indexOf(this.parcoursSelected.id)] = response.data.response

                                //STORE IMAGE TO THE CLOUD OF GOOGLE (AND THEN PASS THE URL AFTER THAT)

                                this.$q.notify({
                                    icon: 'fas fa-check-square',
                                    color: 'secondary',
                                    message: response.data.message,
                                    position: 'top'
                                })
                            } else {
                                this.$q.notify({
                                    icon: 'fas fa-exclamation-triangle',
                                    color: 'negative',
                                    message: response.data.message,
                                    position: 'top'
                                })
                            }
                        })
                    } else {
                        this.errortime = true;
                        this.errormessagetime = "Doit être compris entre 00:00 et 23:59";
                        setTimeout(this.onResetValidation, 3000);
                    }
                }
            },
            onResetValidation() {
                this.errortime = false
                this.errormessagetime = null
            },
            addedParcours() {
                if (this.$refs.name.validate() && this.$refs.description.validate() && this.$refs.time.validate()) {
                    this.loading = true;
                    var timeSplit = this.timeParcours.split(":");
                    if (parseInt(timeSplit[0]) >= 0 && parseInt(timeSplit[0]) < 24 && parseInt(timeSplit[1]) >= 0 && parseInt(timeSplit[1]) < 59) {
                        var listeRouteTag = [];
                        this.tagsParcours.forEach(element => {
                            listeRouteTag.push({
                                idTag: element.id,
                                idRoute: 0
                            });
                        });
                        var listeRouteStep = [];
                        this.stepSelected.forEach(element => {
                            listeRouteStep.push({
                                idStep: element.id,
                                idRoute: self.idParcours
                            });
                        });
                        ParcoursDataService.create({
                            Name: this.nameParcours,
                            Description: this.descriptionParcours,
                            Handicap: this.handicapParcours,
                            Time: this.timeParcours,
                            RouteTags: listeRouteTag,
                            RouteSteps: listeRouteStep
                        }).then(response => {
                            this.loading = false;
                            if (response.data.status == 1) {
                                this.manageParcours = false;
                                this.onResetValidation();
                                this.parcours.push(response.data.response);

                                //STORE IMAGE TO THE CLOUD OF GOOGLE (AND THEN PASS THE URL AFTER THAT)

                                this.$q.notify({
                                    icon: 'fas fa-check-square',
                                    color: 'secondary',
                                    message: response.data.message,
                                    position: 'top'
                                })
                            } else {
                                this.$q.notify({
                                    icon: 'fas fa-exclamation-triangle',
                                    color: 'negative',
                                    message: response.data.message,
                                    position: 'top'
                                })
                            }
                        }).catch();
                    } else {
                        this.errortime = true;
                        this.errormessagetime = "Doit être compris entre 00:00 et 23:59";
                        setTimeout(this.onResetValidation, 3000);
                    }
                }
            },

            openModalToDelete(parcour) {
                this.confirm = true;
                this.messageDeleteParcours = parcour.name;
                this.deleteParcours = parcour.id;
            },
            deletedParcours() {
                var self = this;
                ParcoursDataService.delete(self.deleteParcours).then(response => {
                    if (response.data.status == 1) {
                        self.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: response.data.message,
                            position: 'top'
                        })
                        self.parcours = self.parcours.filter(function (obj) {
                            return obj.id !== self.deleteParcours;

                        });
                    } else {
                        self.$q.notify({
                            message: response.data.message,
                            color: 'negative',
                            icon: 'fas fa-exclamation-triangle',
                            position: 'top'
                        })
                    }
                })
            },
            filterStep(val, update) {
                if (val === '') {
                    update(() => {
                        this.stepsOptions = this.steps
                    })
                    return
                }

                update(() => {
                    const needle = val.toLowerCase()
                    this.stepsOptions = this.steps.filter(v => v.step.name.toLowerCase().indexOf(needle) > -1)
                })
            },
            //filterJeux(val, update) {
            //    update(() => {
            //        if (val === '') {
            //            this.jeuxOptions = listeJeux
            //        }
            //        else {
            //            const needle = val.toLowerCase()
            //            this.jeuxOptions = listeJeux.filter(
            //                v => v.toLowerCase().indexOf(needle) > -1
            //            )
            //        }
            //    })
            //},
            //filterSteps(val, update) {
            //    update(() => {
            //        if (val === '') {
            //            this.stepsOptions = listeEtape
            //        }
            //        else {
            //            const needle = val.toLowerCase()
            //            this.stepsOptions = listeEtape.filter(
            //                v => v.toLowerCase().indexOf(needle) > -1
            //            )
            //        }
            //    })
            //}
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>