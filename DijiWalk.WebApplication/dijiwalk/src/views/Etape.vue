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
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par parcours"
                                          style="width: 250px" />
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card v-ripple:red-14 @click="openModalToManage(false)" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>

            <div v-for="step in steps" v-bind:key="step.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <q-img :src="step.validation" />

                    <q-card-section>
                        <q-btn @click="openModalToDelete(step)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px;" />
                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ step.name }}
                            </div>
                        </div>
                        <div class="row items-center no-wrap text-grey">
                            <q-icon name="fas fa-calendar" />
                            <p class="q-ma-none q-ml-xs">{{ step.creationDate | formatDate }}</p>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <q-card-actions>
                        <q-btn flat round icon="fas fa-info-circle" />
                        <q-btn flat @click="openModalToManage(true); fillForm(step)" color="primary text-bold">
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
                        <q-avatar icon="fas fa-exclamation-triangle" color="negative" text-color="white" size="70px"/>
                    </div>
                    <div class="row col-10">
                        <span>Êtes-vous sûr de vouloir supprimer l'étape {{ messageDeleteStep }} ?</span>
                        <span class="text-caption text-negative q-mt-sm" color="negative" style="font-size: 10px;">{{ messageBonus }}</span>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" @click="deletedStep" color="negative" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-model="manageStep">
            <q-card>
                <q-card-section class="row items-center">
                    <div class="row justify-between">
                        <q-input v-model="idStep" type="hidden" />
                        <q-input ref="name" class="col-12 q-pr-sm q-my-xs" label="Intitulé *" color="primary" option-value="id" option-label="name" v-model="nameStep" name="nameStep" id="nameStep" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un intitulé.']">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-address-card" />
                            </template>
                        </q-input>
                        <q-input class="col-12 q-my-xs" ref="description" v-model="descriptionStep" autogrow type="textarea" label="Description *" option-value="id" option-label="name" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner une description.']" name="descriptionStep" id="descriptionStep">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-feather-alt" />
                            </template>
                        </q-input>

                        <q-file color="primary" class="col-12 q-my-xs" ref="picture" v-model="pictureStep" label="Image de validation *" accept=".jpg, image/*" lazy-rules :rules="[val => !!val || 'Image obligatoire !']" clearable>
                            <template v-slot:prepend>
                                <q-icon name="fas fa-image" />
                            </template>
                        </q-file>

                        <q-input class="col-12 q-my-xs" ref="latitude" v-model="latitudeStep" type="text" label="Latitude *" option-value="id" option-label="name" name="latitudeStep" id="latitudeStep" :error-message="errormessagelatitude" :error="errorlatitude">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-map-pin" />
                            </template>
                        </q-input>

                        <q-input class="col-12 q-my-xs" ref="longitude" v-model="longitudeStep" type="text" label="Longitude *" option-value="id" option-label="name" name="longitudeStep" id="longitudeStep" :error-message="errormessagelongitude" :error="errorlongitude">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-map-pin" />
                            </template>
                        </q-input>

                        <q-select class="col-12 q-my-xs" ref="mission" clearable use-input fill-input v-model="missionSelected" multiple :options="missionsOptions" label="Missions *" option-value="id" option-label="name" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez choisir une mission.']"  @filter="filterMission"/>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="show" label="Modifier" @click="updateStep" color="secondary" />
                    <q-btn v-else label="Ajouter" @click="addedStep" color="secondary" />
                </q-card-actions>
            </q-card>
        </q-dialog>

    </q-page>
</template>


<script>
    import StepDataService from '@/services/StepDataService';
    import MissionDataService from '@/services/MissionDataService';
    import moment from 'moment';

    export default {
        name: 'etape',
        data() {
            return {

                manageStep: false,
                messageDeleteStep: null,
                messageBonus: "Si vous supprimer une étape, cela supprimera toutes ses missions, questions et réponses !",

                deleteStep: null,
                confirm: false,
                show: true,

                steps: null,
                stepSelected: null,
                nameStep: null,
                descriptionStep: null,
                pictureStep: null,
                latitudeStep: null,
                longitudeStep: null,
                creationDateStep: null,

                missions: null,
                missionSelected: null,
                missionsOptions: null,

                errormessagelatitude: null,
                errorlatitude: false,
                errormessagelongitude: null,
                errorlongitude: false,
                idStep: null,
            }
        },
        created() {
            this.getAllSteps();
            this.getAllMissions();
        },
        filters: {
            formatDate: function (value) {
                if (!value) return ''
                return moment(String(value)).format('DD/MM/YYYY')
            }
        },
        methods: {
            resetInput() {
                this.idStep = null
                this.nameStep = null
                this.descriptionStep = null
                this.pictureStep = null
                this.latitudeStep = null
                this.longitudeStep = null
                this.missionSelected = null
            },

            filterMission (val, update) {
              if (val === '') {
                  update(() => {
                      this.missionsOptions = this.missions
                    })
                    return
                }

                update(() => {
                    const needle = val.toLowerCase()
                    this.missionsOptions = this.missions.filter(v => v.name.toLowerCase().indexOf(needle) > -1)
                })
            },

            openModalToManage(action) {
                this.resetInput();
                this.manageStep = true
                this.show = action;
            },

            getAllSteps() {
                if (this.steps === null) {
                    StepDataService.getAll().then(response => {
                        this.steps = response.data;
                    }).catch(reason => {
                        console.log(reason);
                    });
                }
            },
            getAllMissions() {
                if (this.missions === null) {
                    MissionDataService.getAll().then(response => {
                        this.missions = response.data;

                    }).catch(reason => {
                        console.log(reason);
                    });
                }
            },
            fillForm(step) {
                this.stepSelected = step;
                this.idStep = step.id
                this.nameStep = step.name;
                this.descriptionStep = step.description;
                this.latitudeStep = step.lat;
                this.longitudeStep = step.lng;
                this.creationDateStep = step.creationDate;
                this.missionSelected = step.missions;
            },

            updateStep() {
                if (this.$refs.name.validate() && this.$refs.description.validate() && this.$refs.picture.validate() && this.$refs.mission.validate()) {
                    if (new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.longitudeStep) && new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.latitudeStep)) {
                        StepDataService.update(this.idStep, {
                            Name: this.nameStep,
                            Description: this.descriptionStep,
                            CreationDate: this.creationDateStep,
                            Validation: this.stepSelected.validation,
                            Longitude: parseFloat(this.longitudeStep.toString().includes(",") ? this.longitudeStep.toString().replace(",", ".") : this.longitudeStep),
                            Latitude: parseFloat(this.latitudeStep.toString().includes(",") ? this.latitudeStep.toString().replace(",", ".") : this.latitudeStep),
                            Missions: this.missionSelected
                        }).then(response => {
                            if (response.data.status == 1) {
                                this.manageStep = false;
                                this.onResetValidation();
                                this.getAllMissions();
                                this.steps[this.steps.map(e => e.id).indexOf(this.stepSelected.id)] = response.data.response

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
                                setTimeout(this.onResetValidation, 3000);
                            }
                        })
                    } else {
                        if (!new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.$refs.latitude)) {
                            this.errorlatitude = true;
                            this.errormessagelatitude = "Doit être du format '11.11'.";
                        }
                        if (!new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.$refs.longitude)) {
                            this.errorlongitude = true;
                            this.errormessagelongitude = "Doit être du format '11.11'.";
                        }
                        setTimeout(this.onResetValidation, 3000);
                    }
                }
            },
            onResetValidation() {
                this.errorlatitude = false
                this.errorlongitude = false
                this.errormessagelatitude = null
                this.errormessagelongitude = null
            },
            addedStep() {
                if (this.$refs.name.validate() && this.$refs.description.validate() && this.$refs.picture.validate() && this.$refs.latitude.validate() && this.$refs.longitude.validate() && this.$refs.mission.validate()) {
                    if (new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.longitudeStep) && new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.latitudeStep)) {
                        StepDataService.create({
                            Name: this.nameStep,
                            Description: this.descriptionStep,
                            Longitude: parseFloat(this.longitudeStep.toString().includes(",") ? this.longitudeStep.toString().replace(",", ".") : this.longitudeStep),
                            Latitude: parseFloat(this.latitudeStep.toString().includes(",") ? this.latitudeStep.toString().replace(",", ".") : this.latitudeStep),
                            CreationDate: moment().format("YYYY-MM-DD hh:mm:ss"),
                            Missions: this.missionSelected
                        }).then(response => {
                            if (response.data.status == 1) {
                                this.manageStep = false;
                                this.onResetValidation();
                                this.getAllMissions();
                                this.steps.push(response.data.response);
                               
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
                                setTimeout(this.onResetValidation, 3000);
                            }
                        }).catch(reason => {
                            console.log(reason);
                        });

                    } else {
                        if (!new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.latitudeStep)) {
                            this.errorlatitude = true;
                            this.errormessagelatitude = "Doit être du format '11.11'.";
                        }
                        if (!new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.longitudeStep)) {
                            this.errorlongitude = true;
                            this.errormessagelongitude = "Doit être du format '11.11'.";
                        }
                        setTimeout(this.onResetValidation, 3000);
                    }
                }
            },

            openModalToDelete(step) {
                this.confirm = true;
                this.messageDeleteStep = step.name;
                this.deleteStep = step.id;
            },

            deletedStep() {
                var self = this;
                StepDataService.delete(self.deleteStep).then(response => {
                    if (response.data.status == 1) {
                        self.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: response.data.message,
                            position: 'top'
                        })
                        self.steps = self.steps.filter(function (obj) {
                            return obj.id !== self.deleteStep;

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

           
            //filterParcours(val, update) {
            //    update(() => {
            //        if (val === '') {
            //            this.parcoursOptions = parcours
            //        }
            //        else {
            //            const needle = val.toLowerCase()
            //            this.parcoursOptions = parcours.filter(
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