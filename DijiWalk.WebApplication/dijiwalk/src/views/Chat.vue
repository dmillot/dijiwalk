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

<div class="q-pa-md">
    <div class="row">
        <div class="col-12 col-md-4">
           <div class="q-pa-md bg-white-10 text-white">
            <q-list dark bordered separator>
                    <q-item clickable v-ripple>
                        <q-item-section>Participant Damien Millot</q-item-section>
                    </q-item>
                <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Pierre Morin</q-item-label>
                    </q-item-section>
                </q-item>
                <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Nathan Gagniarre</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Mathis Espanelle</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Clément Hugon</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Eric Shalk</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Valentin Virot</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Kim Dole</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Martin Balme</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Rafael Delcroix</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Loic Populier</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Benjamin Sorriaux</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Pierre Loreps</q-item-label>
                    </q-item-section>
                </q-item>
                 <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Jean Lafontaine</q-item-label>
                    </q-item-section>
                </q-item>
                <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Hugo Gumut</q-item-label>
                    </q-item-section>
                </q-item>
                <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Maxime Laroze</q-item-label>
                    </q-item-section>
                </q-item>
                <q-item clickable v-ripple>
                    <q-item-section>
                        <q-item-label>Participant Paul Haut</q-item-label>
                    </q-item-section>
                </q-item>
                
            </q-list>
        </div>
    </div>
<div style="background-color:white;" class="col-12 col-md-8">
 <q-chat-message
        name="Damien Millot - Groupe 1"
        avatar="https://cdn.quasar.dev/img/avatar3.jpg"
        :text="['On est bloqué il nous faudrait un indice svp.']"
        size="6"
        stamp="4 minutes ago"
        text-color="white"
        bg-color="primary"
      />
       <q-chat-message
        name="Organisateur"
        avatar="https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg"
        :text="['Il y a une chouette sur se monument.']"
        stamp="5 minutes ago"
        sent
        bg-color="amber-7"
      />
       <q-chat-message
        name="Damien Millot - Groupe 1"
        avatar="https://cdn.quasar.dev/img/avatar3.jpg"
        :text="['Haaa oui d’accord merci.' ]"
        size="6"
        stamp="7 minutes ago"
        text-color="white"
        bg-color="primary"
      />
     <q-chat-message
        name="Organisateur"
        avatar="https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg"
        :text="['Pas de soucis bonne visite. ']"
        stamp="9 minutes ago"
        sent
        bg-color="amber-7"
      />
      <div style="position:absolute; bottom:0px; width:62%; background-color:white;">
 <q-input  bottom-slots v-model="text" label="Message" :dense="dense">
        <template v-slot:before>
          <q-avatar>
            <img src="https://static.vecteezy.com/system/resources/previews/000/512/576/non_2x/vector-profile-glyph-black-icon.jpg">
          </q-avatar>
        </template>
        <q-btn color="blue" icon-right="fas fa-paper-plane"/>
 </q-input>
 </div>
      </div>
 
    </div>

    <div class="row">
      <div class="col-12 col-md">

      </div>
      <div class="col-12 col-md">

      </div>
      <div class="col-12 col-md">

      </div>
    </div>

  </div>

    
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
      jeux: listeJeux
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