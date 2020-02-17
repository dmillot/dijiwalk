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
                <q-card v-ripple:red-14 @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>

            <div v-for="step in steps" v-bind:key="step.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <div @click="openModalToEdit(step)" class="game-card">
                        <q-img :src="step.validation" />

                        <q-card-section>
                            <q-btn v-on:click.stop="openModalToDelete(step)"
                                   fab
                                   color="negative"
                                   icon="fas fa-trash"
                                   class="absolute"
                                   style="top: 0; right: 12px; transform: translateY(-50%); z-index: 999;" />
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
                    </div>

                    <q-card-actions>
                        <q-btn flat round @click="openModalToGetInformations(step)" class="dijiwalk-secondary" icon="fas fa-info-circle" />
                        <q-btn flat @click="openModalToGetInformations(step)" class="dijiwalk-secondary" color="primary text-bold">
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
                        <q-input v-if="isEditing" v-model="idStep" type="hidden" />
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

                        <q-file color="primary" class="col-12 q-my-xs" ref="picture" v-model="pictureStep" label="Image de validation *" accept=".jpg, image/*" lazy-rules :rules="[val => !!val || 'Image obligatoire que si nouvelle étape !']" clearable hint="Peut être vide si mise à jour !" @input="takePicture" @clear="clearPicture">
                            <template v-slot:prepend>
                                <q-avatar id="avatarSelected" v-show="!noPicture">

                                </q-avatar>
                                <q-icon v-show="noPicture" name="fas fa-image" />
                            </template>

                        </q-file>

                        <GmapMap class="q-my-sm" ref="mapRefManage" :center="centerManage" :zoom="15" style="width:100%;  height: 200px;" @click="changePosition">
                            <GmapMarker :position="markerManageStep.position"></GmapMarker>
                        </GmapMap>

                        <q-input class="col-6 q-my-xs q-pr-sm" ref="latitude" v-model="latitudeStep" type="text" label="Latitude *" option-value="id" option-label="name" name="latitudeStep" id="latitudeStep" :error-message="errormessagelatitude" :error="errorlatitude">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-map-pin" />
                            </template>
                        </q-input>

                        <q-input class="col-6 q-my-xs q-pl-sm" ref="longitude" v-model="longitudeStep" type="text" label="Longitude *" option-value="id" option-label="name" name="longitudeStep" id="longitudeStep" :error-message="errormessagelongitude" :error="errorlongitude">
                            <template v-slot:prepend>
                                <q-icon name="fas fa-map-pin" />
                            </template>
                        </q-input>

                        <div class="row items-center justify-center col-12">
                            <q-select class="col-10 q-my-xs" ref="mission" clearable use-input fill-input v-model="missionSelected" multiple :options="missionsOptions" label="Missions *" option-value="id" option-label="name" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez choisir une mission.']" @filter="filterMission" hint="Vous pouvez en rajouter une cliquant sur le bouton à droite !" />
                            <div class="col-1 q-ml-md">
                                <q-btn color="primary" @click="navigateTo('/')" rounded icon="fas fa-plus" />
                            </div>
                        </div>
                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="isEditing" label="Modifier" @click="updateStep" color="secondary" />
                    <q-btn v-if="isAdding" label="Ajouter" @click="addedStep" color="secondary" />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-if="stepSelected !== null" v-model="informations">
            <q-card class="modal-informations">
                <q-card-section class="col items-center">
                    <q-img :src="stepSelected.validation" />
                    <h5 class="q-my-sm">{{ stepSelected.name }}</h5>
                    <p>{{ stepSelected.description }}</p>
                    <div class="row col-12 q-my-sm" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                        <q-item>
                            <q-item-section avatar>
                                <q-icon color="grey" name="fas fa-calendar" />
                            </q-item-section>
                            <q-item-section>{{ stepSelected.creationDate | formatDate }}</q-item-section>
                        </q-item>
                    </div>
                    <GmapMap class="q-my-sm" ref="mapRef" :center="center" :zoom="15" style="width:100%;  height: 200px;">
                        <GmapMarker :position="markerStep.position"></GmapMarker>
                    </GmapMap>
                    <div class="row col-12" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                        <q-item>
                            <q-item-section avatar>
                                <q-icon color="grey" name="fas fa-map-pin" />
                            </q-item-section>
                            <q-item-section>
                                Latitude: {{ stepSelected.lat }}° / Longitude: {{ stepSelected.lng }}°
                            </q-item-section>
                        </q-item>
                    </div>
                    <div class="row col-12">
                        <q-list class="custom-expansion col-12">
                            <q-expansion-item expand-separator icon="fas fa-clipboard-list" label="Missions">
                                <q-expansion-item icon="fas fa-list-ol" v-for="mission in stepSelected.missions" v-bind:key="mission.id" v-bind:label="mission.name" :header-inset-level="1" :content-inset-level="2" style="border-bottom: 1px rgba(0,0,0,0.12) solid;">
                                    <q-card>
                                        <q-card-section v-for="trial in mission.trials" v-bind:key="trial.id" class="row items-center">
                                            <q-icon name="fas fa-question-circle" class="q-mr-md" style="color: #C10015 !important; font-size: 1.5em;" />
                                            Trial n°{{ trial.id }}
                                        </q-card-section>
                                    </q-card>
                                </q-expansion-item>
                            </q-expansion-item>
                        </q-list>
                    </div>
                    <div class="row col-12">
                        <q-list class="custom-expansion col-12">
                            <q-expansion-item expand-separator icon="fas fa-tags" label="Tags">
                                <div class="row justify-start">
                                    <div v-for="tag in stepSelected.stepTags" v-bind:key="tag.idTag">
                                        <q-chip outline class="q-my-xs q-px-lg q-py-md" color="red" text-color="white" size="md" icon="fas fa-tag">
                                            <div class="q-px-sm q-my-sm">{{ tag.tag.name }}</div>
                                        </q-chip>
                                    </div>
                                </div>
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
                isEditing: false,
                isAdding: false,
                informations: false,

                steps: null,
                stepSelected: null,
                nameStep: null,
                descriptionStep: null,
                pictureStep: null,
                latitudeStep: null,
                longitudeStep: null,
                creationDateStep: null,
                validationUrl: null,

                missions: null,
                missionSelected: null,
                missionsOptions: null,

                errormessagelatitude: null,
                errorlatitude: false,
                errormessagelongitude: null,
                errorlongitude: false,
                idStep: null,
                noPicture: true,

                center: null,
                centerManage: null,
                markerStep: { position: { lat: 0, lng: 0 } },
                markerManageStep: { position: { lat: 0, lng:0 }}
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
            initMap(step) {
                this.center = { lat: step.lat, lng: step.lng };
                this.addMarker(step);
            },
            initMapManage(step) {
                this.centerManage = { lat: step.lat, lng: step.lng };
                this.addMarkerManage(step);
            },
            addMarker(step) {
                this.markerStep = { position: { lat: step.lat, lng: step.lng } };    
            },
            addMarkerManage(step) {
                this.markerManageStep = { position: { lat: step.lat, lng: step.lng } };
            },
            clearPicture() {
                document.getElementById('avatarSelected').innerHTML = "";
                this.noPicture = true;
            },
            changePosition(mouseArgs) {
                this.latitudeStep = mouseArgs.latLng.lat()
                this.longitudeStep = mouseArgs.latLng.lng()
                this.markerManageStep.position = {lat: this.latitudeStep , lng: this.longitudeStep}
            },
            takePicture() {
                if (this.pictureStep != null) {
                    this.noPicture = false;
                    let reader = new FileReader();
                    reader.onload = function (e) {
                        if (e.target.result.indexOf("image") != -1) {
                            document.getElementById('avatarSelected').innerHTML = ['<img style="width:35px; height: 35px;" src="', e.target.result, '" />'].join('')
                        }
                    };
                    reader.readAsDataURL(this.pictureStep);
                }

            },
            fileConvert() {
                return new Promise((resolve, reject) => {
                    if (this.pictureStep != null) {
                        const reader = new FileReader();
                        reader.readAsDataURL(this.pictureStep);
                        reader.onload = () => resolve(reader.result);
                        reader.onerror = error => reject(error);
                    } else {
                        resolve(this.pictureStep);
                    }
                })
            },
            resetInput() {
                this.idStep = null
                this.nameStep = null
                this.descriptionStep = null
                this.pictureStep = null
                this.latitudeStep = null
                this.longitudeStep = null
                this.missionSelected = null
            },

            filterMission(val, update) {
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

            openModalToGetInformations(step) {
                this.isAdding = false;
                this.isEditing = false;
                this.stepSelected = step;
                this.markerStep = null;
                this.initMap(step);
                this.informations = true;
            },

            openModalToAdd() {
                this.isEditing = false;
                this.isAdding = true;
                this.resetInput();
                this.manageStep = true;
            },

            openModalToEdit(step) {
                this.isEditing = true;
                this.isAdding = false;
                this.resetInput();
                this.fillForm(step);
                this.markerManageStep = null;
                this.initMapManage(step);
                this.manageStep = true
            },
            getAllSteps() {
                this.$q.loading.show()
                if (this.steps === null) {
                    StepDataService.getAll().then(response => {
                        this.steps = response.data;
                        this.$q.loading.hide()
                    }).catch();
                }
            },
            getAllMissions() {
                if (this.missions === null) {
                    MissionDataService.getAll().then(response => {
                        this.missions = response.data;
                    }).catch();
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
                this.validationUrl = step.validation;
            },

            updateStep() {
                if (this.$refs.name.validate() && this.$refs.description.validate() && this.$refs.mission.validate()) {
                    if (new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.longitudeStep) && new RegExp('^[0-9]{1,}\.?[0-9]{1,}$').test(this.latitudeStep)) {
                        this.$q.loading.show()
                        this.fileConvert().then(response => {
                            StepDataService.update(this.idStep, {
                                Name: this.nameStep,
                                Description: this.descriptionStep,
                                CreationDate: this.creationDateStep,
                                Lng: parseFloat(this.longitudeStep.toString().includes(",") ? this.longitudeStep.toString().replace(",", ".") : this.longitudeStep),
                                Lat: parseFloat(this.latitudeStep.toString().includes(",") ? this.latitudeStep.toString().replace(",", ".") : this.latitudeStep),
                                Missions: this.missionSelected,
                                ImageBase64: this.$refs.picture.validate() ? response : this.validationUrl,
                                ImageChanged: this.$refs.picture.validate(),
                                Validation: this.validationUrl,
                            }).then(response => {
                                this.$q.loading.hide()
                                if (response.data.status == 1) {
                                    this.manageStep = false;
                                    this.onResetValidation();
                                    this.getAllMissions();
                                    this.steps[this.steps.map(e => e.id).indexOf(this.stepSelected.id)] = response.data.response

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
                        this.$q.loading.show()
                        this.fileConvert().then(response => {
                            StepDataService.create({
                                Name: this.nameStep,
                                Description: this.descriptionStep,
                                Lng: parseFloat(this.longitudeStep.toString().includes(",") ? this.longitudeStep.toString().replace(",", ".") : this.longitudeStep),
                                Lat: parseFloat(this.latitudeStep.toString().includes(",") ? this.latitudeStep.toString().replace(",", ".") : this.latitudeStep),
                                CreationDate: moment().format("YYYY-MM-DD hh:mm:ss"),
                                Missions: this.missionSelected,
                                ImageBase64: response,
                                ImageChanged: true
                            }).then(response => {
                                this.$q.loading.hide()
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
                            }).catch();
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
                this.$q.loading.show()
                var self = this;
                StepDataService.delete(self.deleteStep).then(response => {
                    this.$q.loading.hide()
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


        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>