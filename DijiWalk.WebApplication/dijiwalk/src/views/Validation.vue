<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar class="bg-toolbar">
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/jeuActuel' " />
                <q-toolbar-title>DijiWalk</q-toolbar-title>

                <div class="q-ml-md cursor-pointer non-selectable">
                    <q-icon name="fas fa-search" />
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width q-mt-lg">
            <q-toolbar class="bg-primary corner-toolbar text-white shadow-2">
                <q-toolbar-title class="text-left text-header-list q-py-md">Demande de validation (échouée pour l'automatique):</q-toolbar-title>
            </q-toolbar>
            <q-list class="qlist-toolbar col-12 bg-white shadow-8 list-overflow">
                <q-item v-for="validate in toValidate" v-bind:key="validate.id" class="row q-pa-none">
                    <div class="col-12">
                        <div class="row q-px-md q-py-md">
                            <div class="row col-9">
                                <div top class="row no-wrap items-center col-4">
                                    <q-icon name="fas fa-map-marked-alt" class="q-my-none" color="grey-5" size="40px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-h6">Parcours:</q-item-label>
                                        <p class="q-my-none text-left text-no-wrap text-overflow full-width">{{validate.routeStep.route.name}}</p>
                                    </div>
                                </div>
                                <div top class="row no-wrap items-center col-4">
                                    <q-icon name="fas fa-users" class="q-my-none" color="grey-5" size="40px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-h6">Équipe:</q-item-label>
                                        <p class="q-my-none text-left text-no-wrap text-overflow full-width">{{validate.team.name}}</p>
                                    </div>
                                </div>
                                <div top class="row no-wrap items-center col-4">
                                    <q-icon name="fas fa-map-signs" class="q-my-none" color="grey-5" size="40px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-h6">Étape:</q-item-label>
                                        <p class="q-my-none text-left text-no-wrap text-overflow full-width">{{validate.routeStep.step.name}}</p>
                                    </div>
                                </div>
                            </div>
                            <div class="row absolute-right text-grey-8 q-pr-sm">
                                <div class="column items-center justify-center q-pa-sm">
                                    <q-spinner-hourglass size="25px" />
                                    <q-item-label caption>{{validate.askValidationDate | formatAgo}}</q-item-label>
                                </div>
                                <q-btn class="q-pa-sm" size="15px" v-on:click.stop="validAsk(validate)" flat dense color="secondary" icon="fas fa-check" />
                                <q-btn class="q-pa-sm" size="15px" v-on:click.stop="rejectAsk(validate)" flat dense color="negative" icon="fas fa-times" />
                                <q-btn class="q-pa-sm" size="15px" v-on:click.stop="seeValidation(validate, true)" flat dense icon="fas fa-eye" />
                            </div>
                        </div>
                        <q-separator></q-separator>
                    </div>
                </q-item>
                <q-item v-if="emptyToValidate" class="row justify-center q-pa-none">
                    <h5 class="text-grey-5">PAS DE DEMANDE DE VALIDATION</h5>
                </q-item>
            </q-list>
        </div>
        <div class="row col-12">
            <div class="row col-6 q-mt-lg q-pr-sm">
                <q-toolbar class="bg-secondary corner-toolbar text-white shadow-2">
                    <q-toolbar-title class="text-left text-header-list q-py-md">Validations accéptées:</q-toolbar-title>
                </q-toolbar>
                <q-list class="qlist-toolbar col-12 bg-white shadow-8 list-overflow">
                    <q-item v-for="validate in isValidate" v-bind:key="validate.id" class="row q-pa-none">
                        <div class="col-12">
                            <div class="row q-px-sm q-py-sm">
                                <div top class="row no-wrap items-center col-3">
                                    <q-icon name="fas fa-map-marked-alt" class="q-my-none" color="grey-5" size="20px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-validation">Parcours:</q-item-label>
                                        <p class="text-validation-sub q-my-none text-left text-no-wrap text-overflow full-width">{{validate.routeStep.route.name}}</p>
                                    </div>
                                </div>
                                <div top class="row no-wrap items-center col-3 q-pl-sm">
                                    <q-icon name="fas fa-users" class="q-my-none" color="grey-5" size="20px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-validation">Équipe:</q-item-label>
                                        <p class="text-validation-sub q-my-none text-left text-no-wrap text-overflow full-width">{{validate.team.name}}</p>
                                    </div>
                                </div>
                                <div top class="row no-wrap items-center col-3 q-pl-sm">
                                    <q-icon name="fas fa-map-signs" class="q-my-none" color="grey-5" size="20px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-validation">Étape:</q-item-label>
                                        <p class="text-validation-sub q-my-none text-left text-no-wrap text-overflow full-width">{{validate.routeStep.step.name}}</p>
                                    </div>
                                </div>

                                <div class="row col text-grey-8 justify-end">
                                    <div class="column items-center justify-center">
                                        <q-spinner-hourglass size="18px" />
                                        <q-item-label caption>{{validate.askValidationDate | formatAgo}}</q-item-label>
                                    </div>
                                    <q-btn class="q-pa-sm" size="10px" v-on:click.stop="seeValidation(validate, false)" flat dense round icon="fas fa-eye" />
                                </div>
                            </div>
                            <q-separator></q-separator>
                        </div>
                    </q-item>
                    <q-item v-if="emptyIsValidate" class="row justify-center q-pa-none">
                        <h5 class="text-grey-5">PAS DE DEMANDE DE VALIDATION</h5>
                    </q-item>
                </q-list>
            </div>
            <div class="row col-6 q-mt-lg q-pl-sm">
                <q-toolbar class="bg-negative corner-toolbar text-white shadow-2">
                    <q-toolbar-title class="text-left text-header-list q-py-md">Validations rejetées:</q-toolbar-title>
                </q-toolbar>
                <q-list class="qlist-toolbar col-12 bg-white shadow-8 list-overflow">
                    <q-item v-for="validate in isNotValidate" v-bind:key="validate.id" class="row q-pa-none">
                        <div class="col-12">
                            <div class="row q-px-sm q-py-sm">
                                <div top class="row no-wrap items-center col-3">
                                    <q-icon name="fas fa-map-marked-alt" class="q-my-none" color="grey-5" size="20px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-validation">Parcours:</q-item-label>
                                        <p class="text-validation-sub q-my-none text-left text-no-wrap text-overflow full-width">{{validate.routeStep.route.name}}</p>
                                    </div>
                                </div>
                                <div top class="row no-wrap items-center col-3 q-pl-sm">
                                    <q-icon name="fas fa-users" class="q-my-none" color="grey-5" size="20px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-validation">Équipe:</q-item-label>
                                        <p class="text-validation-sub q-my-none text-left text-no-wrap text-overflow full-width">{{validate.team.name}}</p>
                                    </div>
                                </div>
                                <div top class="row no-wrap items-center col-3 q-pl-sm">
                                    <q-icon name="fas fa-map-signs" class="q-my-none" color="grey-5" size="20px" />
                                    <div class="column items-start q-pl-md no-wrap text-overflow">
                                        <q-item-label class="q-my-none text-bold text-validation">Étape:</q-item-label>
                                        <p class="text-validation-sub q-my-none text-left text-no-wrap text-overflow full-width">{{validate.routeStep.step.name}}</p>
                                    </div>
                                </div>

                                <div class="row col text-grey-8 justify-end">
                                    <div class="column items-center justify-center">
                                        <q-spinner-hourglass size="18px" />
                                        <q-item-label caption>{{validate.askValidationDate | formatAgo}}</q-item-label>
                                    </div>
                                    <q-btn class="q-pa-sm" size="10px" v-on:click.stop="seeValidation(validate, false)" flat dense round icon="fas fa-eye" />
                                </div>
                            </div>
                            <q-separator></q-separator>
                        </div>
                    </q-item>
                    <q-item v-if="emptyIsNotValidate" class="row justify-center q-pa-none">
                        <h5 class="text-grey-5">PAS DE DEMANDE DE VALIDATION</h5>
                    </q-item>
                </q-list>
            </div>
        </div>
        <q-dialog v-model="confirm">
            <q-card style="max-width: 700px !important;">
                <div class="row col-12 q-pt-md q-mb-md q-px-sm">
                    <q-img :src="pictureStep" class="vertical-top q-mx-sm rounded-borders" :ratio="16/9" style="max-height:300px; width:300px; max-width:300px;">
                        <div class="absolute-bottom text-subtitle1 text-center text-bold">
                            Image de l'étape
                        </div>
                    </q-img>
                    <q-img :src="pictureCaptain" class="vertical-top q-mx-sm rounded-borders" :ratio="16/9" style="max-height:300px; width:300px; max-width:300px;">
                        <div class="absolute-bottom text-subtitle1 text-center text-bold">
                            Image du capitaine
                        </div>
                    </q-img>
                </div>
                <q-separator spaced></q-separator>
                <div class="column items-center q-mt-md vertical-top q-pb-md full-width">
                    <q-img :src="pictureAsk" class="rounded-borders" :ratio="16/9" style="max-height:300px; max-width:600px;">
                        <div class="absolute-bottom text-subtitle1 text-center text-bold">
                            Image de la demande
                        </div>
                    </q-img>
                </div>
                <q-card-actions align="right">
                    <q-btn class="q-pa-sm text-bold" size="15px" flat dense color="primary" label="retour" v-close-popup />
                    <q-btn class="q-pa-sm" size="15px" v-show="canValidate" v-on:click.stop="rejectAsk(validateSelected)" flat dense round color="negative" icon="fas fa-times" v-close-popup />
                    <q-btn class="q-pa-sm" size="15px" v-show="canValidate" v-on:click.stop="validAsk(validateSelected)" flat dense round color="secondary" icon="fas fa-check" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>


    </q-page>
</template>

<script>
    import ValidationDataService from "@/services/ValidationDataService";

    export default {
        name: 'validation',
         props: ['idJeu'],
        data() {
            return {
                id: null,
                validations: null,
                toValidate: null,
                isValidate: null,
                isNotValidate: null,
                confirm: false,
                validateSelected: null,
                pictureStep: null,
                pictureCaptain: null,
                pictureAsk: null,
                canValidate: false,
                emptyToValidate: false,
                emptyIsValidate: false,
                emptyIsNotValidate: false
            }
        },
        created() {
            console.log(this.$route.params.idJeu)
            this.id = this.$route.params.idJeu;
            this.getAllValidations(this.id);
        },
        filters: {
            formatAgo: function (value) {
                if (!value) return ''
                var askValidationDate = new Date(String(value));
                var today = new Date();
                var agoDate = new Date(today - askValidationDate);
                var years = (agoDate.getFullYear() - 1970 != 0) ? (agoDate.getFullYear() - 1970 != 1) ? `${agoDate.getFullYear() - 1970} années ` : `${agoDate.getFullYear() - 1970} année ` : "";
                var months = (agoDate.getMonth() != 0) ? `${agoDate.getMonth()} mois ` : "";
                var days = (agoDate.getDay() - 4 != 0) ? `${agoDate.getDay() - 4} jours ` : "";
                var hours = (agoDate.getHours() - 1 != 0) ? (agoDate.getHours() - 1 != 1) ? `${agoDate.getHours() - 1} heures ` : `${agoDate.getHours() - 1} heure ` : "";
                var minutes = (agoDate.getMinutes() != 0) ? (agoDate.getMinutes() != 1) ? `${agoDate.getMinutes()} minutes ` : `${agoDate.getMinutes()} minute ` : "";
                return years + months + days + hours + minutes;
            }
        },
        methods: {
            getAllValidations(idJeu) {
                this.$q.loading.show()
                if (this.validations === null) {
                    ValidationDataService.get(idJeu).then(response => {
                        this.validations = response.data;
                        this.toValidate = this.validations.filter(function (el) {
                            if (!el.validate && el.validationDate == null)
                                return el;
                        })
                        if (this.toValidate == null || this.toValidate.length == 0) {
                            this.emptyToValidate = true;
                        }
                        this.isValidate = this.validations.filter(function (el) {
                            if (el.validate && el.validationDate != null) return el;
                        })
                        if (this.isValidate == null || this.isValidate.length == 0) {
                            this.emptyIsValidate = true;
                        }
                        this.isNotValidate = this.validations.filter(function (el) {
                            if (!el.validate && el.validationDate != null) return el;
                        })
                        if (this.isNotValidate == null || this.isNotValidate.length == 0) {
                            this.emptyIsNotValidate = true;
                        }
                        this.$q.loading.hide()
                    }).catch();
                }
            },

            seeValidation(validate, action) {
                this.confirm = true;
                this.validateSelected = validate;
                this.pictureStep = validate.routeStep.step.validation;
                this.pictureCaptain = validate.team.captain.picture;
                this.pictureAsk = validate.picture;
                this.canValidate = action;
                validate.expand = true;
            },

            validAsk(validate) {
                validate.validate = true;
                validate.validationDate = new Date();
                ValidationDataService.valid(validate).then(response => {
                    if (response.data.status == 1) {
                        this.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: response.data.message,
                            position: 'top'
                        })
                        this.toValidate = this.toValidate.filter(function (el) {
                            if (el.id != response.data.response.id) {
                                return el;
                            }
                        })
                        this.isValidate.push(response.data.response)
                    } else {
                        this.$q.notify({
                            icon: 'fas fa-exclamation-triangle',
                            color: 'negative',
                            message: response.data.message,
                            position: 'top'
                        })
                    }
                });

            },
            rejectAsk(validate) {
                validate.validate = false;
                validate.validationDate = new Date();
                ValidationDataService.reject(validate).then(response => {
                    if (response.data.status == 1) {
                        this.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: response.data.message,
                            position: 'top'
                        })
                        this.toValidate = this.toValidate.filter(function (el) {
                            if (el.id != response.data.response.id) {
                                return el;
                            }
                        })
                        this.isNotValidate.push(response.data.response)
                    } else {
                        this.$q.notify({
                            icon: 'fas fa-exclamation-triangle',
                            color: 'negative',
                            message: response.data.message,
                            position: 'top'
                        })
                    }
                });
            }

        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }

    .text-header-list {
        font-size: 25px !important;
        font-weight: 100 !important;
    }

    .text-overflow {
        overflow: hidden !important;
        text-overflow: ellipsis !important;
    }

    .corner-toolbar {
        border-radius: 4px 4px 0px 0px !important;
    }

    .qlist-toolbar {
        border-radius: 0px 0px 4px 4px !important;
    }

    .list-overflow {
        max-height: 35vh !important;
        overflow-y: auto;
    }

    .text-validation {
        font-size: 15px !important;
    }

    .text-validation-sub {
        font-size: 10px !important;
    }

</style>