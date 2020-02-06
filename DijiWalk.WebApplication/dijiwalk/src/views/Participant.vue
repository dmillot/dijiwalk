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
                                          v-model="modelEquipe"
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par équipe"
                                          :options="equipesOptions"
                                          @filter="filterEquipe"
                                          style="width: 250px" />
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card @click="openModalToAdd()" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>
            <div v-for="participant in participants" v-bind:key="participant.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <q-img :src="participant.picture" />

                    <q-card-section>
                        <q-btn @click="openModalToDelete(participant)"
                               fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px; transform: translateY(-50%);" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ participant.firstName }} {{ participant.lastName }} / {{ participant.Login }}
                            </div>
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
                    <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer ce {{ messageDeleteParticipant }} ?</span>
                </q-card-section>

                <q-card-actions align="right">
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn flat label="Supprimer" color="negative" @click="deletedParticipant" v-close-popup />
                </q-card-actions>
            </q-card>
        </q-dialog>
        <q-dialog v-model="addParticipant">
            <q-card>
                <q-card-section class="row items-center">
                    <q-input ref="firstname" class="col-6 q-pr-sm  q-my-xs" label="Prénom *" color="primary" option-value="id" option-label="name" v-model="prenomParticipant" name="prenomParticipant" id="prenomParticipant" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un prénom.']">
                        <template v-slot:prepend>
                            <q-icon name="fas fa-address-card" />
                        </template>
                    </q-input>
                    <q-input ref="lastname" class="col-6 q-pl-sm q-my-xs" label="Nom *" color="primary" option-value="id" option-label="name" v-model="nomParticipant" name="nomParticipant" id="nomParticipant" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un nom.']">
                        <template v-slot:prepend>
                            <q-icon name="fas fa-address-card" />
                        </template>
                    </q-input>
                    <q-input ref="login" class="col-12 q-my-xs" label="Login *" color="primary" option-value="id" option-label="name" v-model="loginParticipant" name="loginParticipant" id="loginParticipant" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un pseudo.']" :error-message="errormessagelogin" :error="errorlogin">
                        <template v-slot:prepend>
                            <q-icon name="fas fa-user" />
                        </template>
                    </q-input>
                    <q-input class="col-12 q-my-xs" ref="password" label="Mot de passe *" v-model="passwordParticipant" :type="isPwd ? 'password' : 'text'" name="passwordParticipant" id="passwordParticipant" :error-message="errormessagepassword" :error="errorpassword" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un mot de passe.']">
                        <template v-slot:prepend>
                            <q-icon name="fas fa-lock" />
                        </template>
                        <template v-slot:append>
                            <q-icon :name="isPwd ? 'fas fa-eye-slash' : 'fas fa-eye'"
                                    class="cursor-pointer"
                                    @click="isPwd = !isPwd" />
                        </template>
                    </q-input>
                    <q-input ref="confirmPassword" class="col-12 q-my-xs" label="Mot de passe de confirmation *" v-model="passwordConfirmParticipant" :type="isPwd ? 'password' : 'text'" name="passwordConfirmParticipant" id="passwordConfirmParticipant" :error-message="errormessagepassword" :error="errorpassword" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez confirmer le mot de passe.']">
                        <template v-slot:prepend>
                            <q-icon name="fas fa-lock" />
                        </template>
                        <template v-slot:append>
                            <q-icon :name="isPwd ? 'fas fa-eye-slash' : 'fas fa-eye'"
                                    class="cursor-pointer"
                                    @click="isPwd = !isPwd" />
                        </template>
                    </q-input>
                    <q-input class="col-12 q-my-xs" ref="email" label="Email *" color="primary" option-value="id" option-label="name" v-model="emailParticipant" name="emailParticipant" type="email" id="emailParticipant" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un email.']" :error-message="errormessageemail" :error="erroremail">
                        <template v-slot:prepend>
                            <q-icon name="fas fa-at" />
                        </template>
                    </q-input>
                    <q-file class="col-12 q-my-xs" ref="picture" v-model="pictureParticipant" label="Image de profil *" accept=".jpg, image/*" lazy-rules :rules="[val => !!val || 'Image obligatoire !']" clearable >
                        <template v-slot:prepend>
                            <q-icon name="fas fa-image" />
                        </template>
                    </q-file>
                </q-card-section>
                <q-card-actions align="right">
                    <q-btn flat label="Réinitialiser" type="reset" color="red-14" class="q-ml-sm" />
                    <q-btn flat label="Annuler" color="primary" v-close-popup />
                    <q-btn label="Ajouter" color="secondary" @click="addedParticipant()" />
                </q-card-actions>
            </q-card>
        </q-dialog>

    </q-page>
</template>

<script>

    import PlayerDataService from '../services/PlayerDataService';

    export default {
        name: 'participant',
        data() {
            return {
                modelEquipe: null,
                modelJeu: null,
                equipesOptions: null,
                jeuxOptions: null,
                participants: null,
                confirm: false,
                addParticipant: false,
                messageDeleteParticipant: null,
                deleteParticipant: null,
                prenomParticipant: null,
                nomParticipant: null,
                loginParticipant: null,
                passwordParticipant: null,
                passwordConfirmParticipant: null,
                emailParticipant: null,
                pictureParticipant: null,
                errormessagepassword: null,
                errorpassword: false,
                errormessageemail: null,
                erroremail: false,
                errormessagelogin: null,
                errorlogin: false,
                isPwd: true
            }
        },
        created() {

            this.getAllParticipants();

        },

        methods: {
            openModalToAdd() {
                this.addParticipant = true
            },
            getAllParticipants() {
                if (this.participants === null) {
                    PlayerDataService.getAll().then(response => {
                        this.participants = response.data;
                    }).catch(reason => {
                        console.log(reason);
                    });
                }
            },
            onResetValidation() {
                this.errorpassword = false
                this.erroremail = false
                this.errorlogin = false
                this.errormessagepassword = null
                this.errormessageemail = null
                this.errormessagelogin = null
            },
            openModalToDelete(participant) {
                this.confirm = true;
                this.messageDeleteParticipant = participant.firstName + " " + participant.lastName;
                this.deleteParticipant = participant.id;
            },
            addedParticipant() {
                if (this.$refs.firstname.validate() && this.$refs.lastname.validate() && this.$refs.login.validate() && this.$refs.password.validate() && this.$refs.confirmPassword.validate() && this.$refs.email.validate() && this.$refs.picture.validate()) {
                    if (this.passwordParticipant === this.passwordConfirmParticipant) {
                        if (new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\/$%\/^&\/*])(?=.{8,})').test(this.passwordParticipant)) {
                            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(this.emailParticipant)) {
                                PlayerDataService.create({
                                    FirstName: this.prenomParticipant,
                                    LastName: this.nomParticipant,
                                    Login: this.loginParticipant,
                                    Password: this.passwordParticipant,
                                    Email: this.emailParticipant
                                }).then(response => {
                                    if (response.data.status == 1) {
                                        this.participants.push(response.data.response);
                                        this.addParticipant = false;
                                        this.onResetValidation();

                                        //STORE IMAGE TO THE CLOUD OF GOOGLE (AND THEN PASS THE URL AFTER THAT)

                                        this.$q.notify({
                                            icon: 'fas fa-check-square',
                                            color: 'secondary',
                                            message: response.data.message,
                                            position: 'top'
                                        })
                                    } else {
                                        var message = response.data.message;
                                        this.errorlogin = true;
                                        this.errormessagelogin = message;
                                        this.erroremail = true;
                                        this.errormessageemail = message;
                                        this.$q.notify({
                                            icon: 'fas fa-exclamation-triangle',
                                            color: 'negative',
                                            message: message,
                                            position: 'top'
                                        })
                                        setTimeout(this.onResetValidation, 3000);
                                    }
                                }).catch(reason => {
                                    console.log(reason);
                                });
                            } else {
                                this.erroremail = true;
                                this.errormessageemail = "Veuillez entrer un email valide.";
                                setTimeout(this.onResetValidation, 3000);
                            }
                        } else {
                            this.errorpassword = true;
                            this.errormessagepassword = "Il faut au moins 8 caractères, une majuscule, une minuscule, un nombre et un caractère spéciale.";
                            setTimeout(this.onResetValidation, 3000);
                        }
                    } else {
                        this.errorpassword = true;
                        this.errormessagepassword = "Les mots de passes ne correspondent pas !";
                        setTimeout(this.onResetValidation, 3000);
                    }
                }
            },
            onReset() {
                this.prenomParticipant = null
                this.nomParticipant = null
                this.loginParticipant = null
                this.passwordParticipant = null
                this.passwordConfirmParticipant = null
                this.emailParticipant = null
            },
            deletedParticipant() {
                var self = this;
                var id = self.deleteParticipant;
                PlayerDataService.delete(id).then(response => {
                    console.log(response.data.status)
                    if (response.data.status == 1) {
                        self.$q.notify({
                            icon: 'fas fa-check-square',
                            color: 'secondary',
                            message: "Suppression d'un participant réussi !",
                            position: 'top'
                        })
                        self.participants = self.participants.filter(function (obj) {
                            return obj.id !== self.deleteParticipant;

                        });
                    } else {
                        self.$q.notify({
                            message: response.data.message,
                            color: 'negative',
                            icon: 'fas fa-exclamation-triangle',
                            position: 'top'
                        })
                    }

                }).catch(reason => {
                    console.log(reason);
                });
            },
            filterEquipe(val, update) {
                update(() => {
                    if (val === '') {
                        this.equipesOptions = this.equipes
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.equipesOptions = this.equipes.filter(
                            v => v.toLowerCase().indexOf(needle) > -1
                        )
                    }
                })
            },
            filterJeux(val, update) {
                update(() => {
                    if (val === '') {
                        this.jeuxOptions = this.jeux
                    }
                    else {
                        const needle = val.toLowerCase()
                        this.jeuxOptions = this.jeux.filter(
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