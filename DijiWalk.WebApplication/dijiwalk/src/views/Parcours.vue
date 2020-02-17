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
                <q-card @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
                    <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
                </q-card>
            </div>
            <div v-for="parcour in parcours" v-bind:key="parcour.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <div @click="openModalToEdit(parcour)" class="game-card">
                        <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />

                        <q-card-section>
                            <q-btn v-on:click.stop="openModalToDelete(parcour)"
                                   fab
                                   color="negative"
                                   icon="fas fa-trash"
                                   class="absolute"
                                   style="top: 0; right: 12px; transform: translateY(-50%);  z-index: 999;" />

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

                        <q-toggle class="col-5 q-my-xs on-left" ref="handicap" v-model="handicapParcours" color="primary" icon="fas fa-wheelchair" label="Accès handicapé ?" option-value="id" option-label="name" name="handicapParcours" id="handicapParcours" />

                        <div class="row items-center justify-center col-12">
                            <q-select class="col-10 q-my-xs" ref="steps" clearable use-input fill-input v-model="stepSelected" multiple :options="stepsOptions" label="Étapes *" option-value="id" option-label="name" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez choisir une étape.']" @filter="filterStep" hint="Vous pouvez en rajouter une si elle n'existe pas (bouton à droite) !">
                                <template v-slot:option="scope">
                                    <q-item v-bind="scope.itemProps" v-on="scope.itemEvents">
                                        <q-avatar>
                                            <q-img :src="scope.opt.validation" style="width:50px; height: 50px;" />
                                        </q-avatar>
                                        <q-item-section class="q-ml-sm">
                                            <q-item-label v-html="scope.opt.name" />
                                        </q-item-section>
                                    </q-item>
                                </template>
                            </q-select>
                            <div class="col-1 q-ml-md">
                                <q-btn color="primary" @click="navigateTo('/etape')" rounded icon="fas fa-plus" />
                            </div>
                        </div>

                        <div class="row items-center justify-center col-12">
                            <q-select class="col-12 q-my-xs" ref="tags" v-model="tagSelected" clearable use-input fill-input :options="tagsOptions" label="Tags" option-value="id" option-label="name" @filter="filterTag" hint="Vous pouvez en rajouter un en l'écrivant puis touche entrer !" @new-value="createTag" />
                        </div>

                        <div class="row justify-start">
                            <div v-for="tag in tagsParcours" v-bind:key="tag.idTag">
                                <q-chip removable color="red" outline text-color="white" class="q-my-xs q-px-lg q-py-md" size="md" icon="fas fa-tag" @remove="removeTags(tag)">
                                    <div class="q-px-sm q-my-sm">{{ tag.name }}</div>
                                </q-chip>
                            </div>
                        </div>

                    </div>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat v-if="isEditing" label="Modifier" @click="updateParcours" color="secondary" />
                    <q-btn v-if="isAdding" label="Ajouter" @click="addedParcours" color="secondary" />
                </q-card-actions>
            </q-card>
        </q-dialog>

        <q-dialog v-if="showInfo" v-model="showInfo">
            <q-card class="modal-informations" style="width: 350px;">
                <q-card-section class="items-center">
                    <div class="row justify-center">
                        <q-avatar size="150px" class="q-my-md">
                            <q-img :src="infoWindowContext.picture" style="width:150px; height: 150px;" />
                        </q-avatar>
                    </div>
                    <p class="text-bold">{{ infoWindowContext.label }}</p>
                    <p class="text-caption">{{ infoWindowContext.description }}</p>
                </q-card-section>
            </q-card>
        </q-dialog>

        <q-dialog v-if="parcoursSelected !== null" v-model="informations">
            <q-card class="modal-informations">
                <q-card-section class="items-center">
                    <GmapMap ref="mapRef" :center="center" :zoom="12" style="width:100%;  height: 400px;">
                        <GmapMarker :key="index" v-for="(m, index) in markers" title="m.label" :position="m.position" @click="toggleInfoWindow(m)"></GmapMarker>
                    </GmapMap>
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
                    <div class="row col-12">
                        <q-list class="custom-expansion col-12">
                            <q-expansion-item expand-separator icon="fas fa-tags" label="Tags">
                                <div class="row justify-start">
                                    <div v-for="tag in parcoursSelected.routeTags" v-bind:key="tag.idTag">
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
                tagsOptions: null,

                idParcours: null,
                center: null,
                markers: [],
                infoWindowContext: null,
                showInfo: false

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
            toggleInfoWindow(context) {
                this.infoWindowContext = context;
                this.showInfo = true;
            },
            initMap(routeSteps) {
                this.markers = [];
                this.center = { lat: routeSteps[0].step.lat, lng: routeSteps[0].step.lng };
                routeSteps.forEach(e => this.addMarker(e));
            },
            addMarker(routeStep) {
                this.markers.push({ label: routeStep.step.name, description: routeStep.step.description, picture: routeStep.step.validation, position: { lat: routeStep.step.lat, lng: routeStep.step.lng } });
            },
            capitalizeFirstLetter(string) {
                return string.charAt(0).toUpperCase() + string.slice(1);
            },
            createTag(val) {
                var tagsAlreadyExists = this.tags.filter(function (el) {
                    if (el.name.toLowerCase() == val.toLowerCase()) return true;
                });
                if (tagsAlreadyExists.length == 0) {
                    TagDataService.create({
                        Id: 0,
                        Name: this.capitalizeFirstLetter(val.toLowerCase()),
                        Description: this.capitalizeFirstLetter(val.toLowerCase()),
                    }).then(response => {
                        this.tags.push(response.data.response);
                        this.tagsParcours.push(response.data.response);
                        this.tagSelected = null;
                    });
                }

            },
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
                this.initMap(parcour.routeSteps);
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
                this.$q.loading.show()
                if (this.parcours === null) {
                    ParcoursDataService.getAll().then(response => {
                        this.parcours = response.data;
                        this.$q.loading.hide()
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
                        this.$q.loading.show()
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
                            this.$q.loading.hide()
                            if (response.data.status == 1) {
                                this.manageParcours = false;
                                this.onResetValidation();
                                this.parcours[this.parcours.map(e => e.id).indexOf(this.parcoursSelected.id)] = response.data.response
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
                    this.$q.loading.show()
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
                            this.$q.loading.hide()
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
                                this.manageParcours = false;
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
                this.$q.loading.show()
                ParcoursDataService.delete(self.deleteParcours).then(response => {
                    this.$q.loading.hide()
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
                    this.stepsOptions = this.steps.filter(v => v.name.toLowerCase().indexOf(needle) > -1)
                })
            },
            filterTag(val, update) {
                if (val === '') {
                    update(() => {
                        this.tagsOptions = this.tagsAvailable
                    })
                    return
                }
                update(() => {
                    const needle = val.toLowerCase()
                    this.tagsOptions = this.tagsAvailable.filter(v => v.name.toLowerCase().indexOf(needle) > -1)
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