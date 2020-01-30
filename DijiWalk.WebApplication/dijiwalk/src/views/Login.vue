<template>
  <q-page class="row flex flex-center q-px-xl">
    <div class="col-xs-12 col-md-5">
      <q-card class="my-card full-height q-px-xl q-py-lg">
        <q-card-section class="flex column flex-center">
          <h4 v-if="login" class="text-bold text-red-14 q-ma-none q-mb-lg">Connexion</h4>
          <h4 v-else class="text-bold text-red-14 q-ma-none q-mb-lg">Inscription</h4>
          <q-form @reset="onReset" class="row">
            <template v-if="!login">
              <div class="row justify-between">
                <div class="col-5">
                  <q-input color="red-9" v-model="firstName" label="Prenom *" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un pr�nom.']">
                    <template v-slot:prepend>
                      <q-icon name="fas fa-user" />
                    </template>
                  </q-input>
                </div>
                <div class="col-5">
                  <q-input color="red-9" v-model="lastName" label="Nom *" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un nom.']">
                    <template v-slot:prepend>
                      <q-icon name="fas fa-user" />
                    </template>
                  </q-input>
                </div>
              </div>
              <div class="col-12">
                <q-input color="red-9" v-model="mail" label="Email *" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un email.']">
                  <template v-slot:prepend>
                    <q-icon name="fas fa-at" />
                  </template>
                </q-input>
              </div>
            </template>
            <div class="col-12">
              <q-input color="red-9" v-model="name" label="Pseudonyme *" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un nom.']">
                <template v-slot:prepend>
                  <q-icon name="fas fa-user" />
                </template>
              </q-input>
            </div>
            <div class="col-12">
              <q-input color="red-9" v-model="password" label="Mot de passe *" :type="isPwd ? 'password' : 'text'" lazy-rules :rules="[ val => val && val.length > 0 || 'Veuillez renseigner un mot de passe.']">
                <template v-slot:prepend>
                  <q-icon name="fas fa-lock" />
                </template>
                <template v-slot:append>
                  <q-icon :name="isPwd ? 'fas fa-eye-slash' : 'fas fa-eye'"
                          class="cursor-pointer"
                          @click="isPwd = !isPwd" />
                </template>
              </q-input>
            </div>

            <div class="q-mt-lg">
              <q-btn v-if="login" label="Se connecter" type="submit" color="red-14" />
              <q-btn v-else label="Envoyer" type="submit" color="red-14" />
              <q-btn label="Reinitialiser" type="reset" color="red-14" flat class="q-ml-sm" />
            </div>

            <q-btn @click="login = !login" v-if="login" outline color="red-14" label="Demander un compte organisateur" class="q-mt-lg" />
            <q-btn @click="login = !login" v-else outline color="red-14" label="J'ai deja un compte" class="q-mt-lg" />

          </q-form>

        </q-card-section>
      </q-card>
    </div>
  </q-page>
</template>

<script>

export default {
  data () {
    return {
      login: true,
      isPwd: true,
      name: null,
      password: null,
      firstName: null,
      lastName: null,
      mail: null
    }
  },
  methods: {
    onReset () {
      this.name = null
      this.password = null
      this.firstName = null
      this.lastName = null
      this.mail = null
    }
  }
}
</script>
