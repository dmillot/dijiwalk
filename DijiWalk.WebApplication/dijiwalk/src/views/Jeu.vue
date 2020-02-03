<template>
  <q-page class="q-px-xl">
    <q-header elevated>
      <q-toolbar>

        <q-toolbar-title>DijiWalk</q-toolbar-title>

        <div class="q-ml-md cursor-pointer non-selectable">
            <q-icon name="fas fa-search" />
            <q-menu>
              <q-list bordered separator style="min-width: 100px">
                <q-item>
                  <q-select
                    filled
                    v-model="modelEquipe"
                    use-input
                    use-chips
                    multiple
                    input-debounce="0"
                    label="Filtrer par équipe"
                    :options="equipesOptions"
                    @filter="filterEquipe"
                    style="width: 250px"
                  />
                </q-item>
                <q-item>
                  <q-select
                      filled
                      v-model="modelParcours"
                      use-input
                      use-chips
                      multiple
                      input-debounce="0"
                      label="Filtrer par parcours"
                      :options="parcoursOptions"
                      @filter="filterParcours"
                      style="width: 250px"
                    />
                </q-item>
                <q-item>
                  <q-select
                      filled
                      v-model="modelParticipants"
                      use-input
                      use-chips
                      multiple
                      input-debounce="0"
                      label="Filtrer par participants"
                      :options="participantsOptions"
                      @filter="filterParticipants"
                      style="width: 250px"
                    />
                </q-item>
              </q-list>
            </q-menu>
          </div>
      </q-toolbar>
    </q-header>
    <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
      <div class="col-xs-12 col-md-4 col-grow">
        <q-card @click="addJeu = true" class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
          <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
        </q-card>
      </div>
      <div v-for="jeu in jeux" v-bind:key="jeu.id" class="col-xs-12 col-md-4 col-grow">
        <q-card class="my-card">
          <q-img src="https://images.frandroid.com/wp-content/uploads/2016/01/google-maps.png" />

          <q-card-section>
            <q-btn
              @click="confirm = true"
              fab
              color="negative"
              icon="fas fa-trash"
              class="absolute"
              style="top: 0; right: 12px; transform: translateY(-50%);"
            />

            <div class="row no-wrap">
              <div class="col text-left text-bold text-h6 ellipsis">
                {{ jeu.name }}
              </div>
            </div>
            <div class="row items-center no-wrap text-grey">
              <q-icon name="fas fa-calendar" />
              <p class="q-ma-none q-ml-xs">{{ jeu.date }}</p>
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
          <span class="q-ml-sm">Êtes-vous sûr de vouloir supprimer ce jeu ?</span>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn flat label="Annuler" color="primary" v-close-popup />
          <q-btn flat label="Supprimer" color="negative" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>

    <q-dialog v-model="addJeu">
      <q-card>
        <q-card-section class="row items-center">
            <div class="row justify-between">
                <div class="col-12">
                    <q-input v-model="text" label="Nom" />
                </div>
                <div class="col-12">
                    <q-input color="primary" v-model="date" type="date" />
                </div>
                <div class="col-12">
                    <q-select v-model="model" :options="parcoursOptions" label="Parcours" />
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
const equipes = [
  'Équipe 1', 'Équipe 2', 'Équipe 3', 'Équipe 4', 'Équipe 5'
]

const parcours = [
  'Parcours 1', 'Parcours 2', 'Parcours 3', 'Parcours 4', 'Parcours 5'
]

const participants = [
  'Damien', 'Nathan', 'Clément', 'Mathis', 'Eric'
]

const listeJeux = [
  { id: 1, name: "Jeu 1", date: "30/02/2020" },
  { id: 2, name: "Jeu 2", date: "30/03/2020" },
  { id: 3, name: "Jeu 3", date: "30/04/2020" },
  { id: 4, name: "Jeu 4", date: "30/05/2020" },
  { id: 5, name: "Jeu 5", date: "30/06/2020" },
  { id: 6, name: "Jeu 6", date: "30/07/2020" },
  { id: 7, name: "Jeu 7", date: "30/08/2020" },
]

export default {
  name: 'jeu',
  data () {
    return {
      modelEquipe: null,
      modelParcours: null,
      modelParticipants: null,
      equipesOptions: equipes,
      parcoursOptions: parcours,
      participantsOptions: participants,
      jeux: listeJeux,
      confirm: false,
      addJeu: false

    }
  },
  methods: {
    filterEquipe (val, update) {
      update(() => {
        if (val === '') {
          this.equipesOptions = equipes
        }
        else {
          const needle = val.toLowerCase()
          this.equipesOptions = equipes.filter(
            v => v.toLowerCase().indexOf(needle) > -1
          )
        }
      })
    },
    filterParcours (val, update) {
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
    },
    filterParticipants (val, update) {
      update(() => {
        if (val === '') {
          this.participantsOptions = participants
        }
        else {
          const needle = val.toLowerCase()
          this.participantsOptions = participants.filter(
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