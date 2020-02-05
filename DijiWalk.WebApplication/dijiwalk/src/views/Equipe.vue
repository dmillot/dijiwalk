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
        <q-card class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card">
          <q-icon name="fas fa-plus text-white" style="font-size: 4em;" />
        </q-card>
      </div>
      <div v-for="equipe in equipes" v-bind:key="equipe.id" class="col-xs-12 col-md-4 col-grow">
        <q-card  class="my-card">
          <div class="row" >
            <div v-for="participant in participantsOptions" v-bind:key="participant.id" class="col-6">
              <div v-if=" equipe.id === participant.IdEquipe" >
                <q-img class="flex" :src="participant.Image"
                style="width: 200px; height: 200px;" />
              </div>
            </div>
          </div>
      
          <q-card-section>
            <q-btn
              fab
              color="negative"
              icon="fas fa-trash"
              class="absolute"
              style="top: 0; right: 12px; transform: translateY(-50%);"
            />

             <q-btn
              fab
              color="negative"
              icon="fas fa-edit"
              class="absolute"
              style="top: 0; right: 100px; transform: translateY(-50%);"
            />

            <div class="row no-wrap">
              <div class="col text-left text-bold text-h6 ellipsis">
                {{ equipe.name }}
              </div>
            </div>
            <div class="row items-center no-wrap text-grey">
              <q-icon name="fas fa-calendar" />
              <p class="q-ma-none q-ml-xs">{{ equipe.date }}</p>
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
  </q-page>
</template>

<script>
const equipes = [
  {id: 1, name:"Équipe 1" },
  {id: 2, name:"Équipe 2" },
  {id: 3, name:"Équipe 3" },
  {id: 4, name:"Équipe 4" },
  {id: 5, name:"Équipe 5" },
  {id: 6, name:"Équipe 6" },
  {id: 7, name:"Équipe 7" }
]

const parcours = [
  'Parcours 1', 'Parcours 2', 'Parcours 3', 'Parcours 4', 'Parcours 5'
]

const participants = [
  {id: 14, name: "Nicolas", IdEquipe: 4 ,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 13, name: "Kim", IdEquipe: 4,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 1, name: "Damien", IdEquipe: 1,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 2, name: "Nathan", IdEquipe: 1,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 3, name: "Clément", IdEquipe: 1,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 4, name: "Mathis", IdEquipe: 1,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 5, name: "Eric", IdEquipe: 6,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 6, name: "Pierre", IdEquipe: 6,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 7, name: "Jean", IdEquipe: 5,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 16, name: "Benjamin", IdEquipe: 5,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 8, name: "Valentin", IdEquipe: 2,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 9, name: "Jaques", IdEquipe: 2,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 10, name: "Martin", IdEquipe: 3,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 11, name: "Raphael", IdEquipe: 3 ,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 12, name: "Loic", IdEquipe: 3 ,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
  {id: 17, name: "Geoffrey", IdEquipe: 6 ,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg"},
  {id: 18, name: "Chalelie", IdEquipe: 7 ,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg"},
  {id: 15, name: "Chalie", IdEquipe: 7 ,Image:"https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg" },
]

const listeEquipes = [
  { id: 1, name: "Équipe 1", date: "30/02/2020" },
  { id: 2, name: "Équipe 2", date: "30/03/2020" },
  { id: 3, name: "Équipe 3", date: "30/04/2020" },
  { id: 4, name: "Équipe 4", date: "30/05/2020" },
  { id: 5, name: "Équipe 5", date: "30/06/2020" },
  { id: 6, name: "Équipe 6", date: "30/07/2020" },
  { id: 7, name: "Équipe 7", date: "30/08/2020" },
]

export default {
  name: 'equipe',
  data () {
    return {
      modelEquipe: null,
      modelParcours: null,
      modelParticipants: null,
      equipesOptions: equipes,
      parcoursOptions: parcours,
      participantsOptions: participants,
      equipes: listeEquipes
    }
  },
  methods: {
    // navigateToJeu () {
    //   this.$router.push({ name: 'home' })
    // },
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