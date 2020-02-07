<template>
  <q-page class="q-px-xl">

    <h5 class="text-bold text-left">Jeu en cours</h5>

    <div class="row flex-center full-width justify-center q-mt-lg q-col-gutter-xl">
        <Card link="/" icon="fas fa-hand-lizard" title="Jeu" description="Page de visualisation des information général du jeu" />
        <Card link="/" icon="fas fa-map-marker-alt" title="Etapes" description="Page de visualisation des etapes" />
        <Card link="" icon="fas fa-clipboard-check" title="Validation" description="Page de gestion des problèmes de validation" />
        <Card link="" icon="fas fa-comments" title="Chat" description="Page de chat avec les participants" />


        <!--<Card link="/" icon="fas fa-route" title="Parcours" description="Page de visualisation des parcours des différentes equipes" />
        <Card link="/" icon="fas fa-users" title="Equipes" description="Page de visualisation des participants et leur equipe" />-->
    </div>

  </q-page>
</template>

<script>
// @ is an alias to /src
import Card from '@/components/Card.vue'
import GameDataService from "@/services/GameDataService";

export default {
        name: 'jeuActuel',

        components: {
            Card
            },

        data() {
            return {
                games: null,
                game: null
            }
        },

        created () {
            this.getActualGame();
        },
  
        methods: {

            getActualGame() {
                if (this.games === null) {
                    GameDataService.getAll().then(response => {
                        this.games = response.data;
                    }).catch(reason => {
                        console.log(reason);
                    });
                }
                this.game = this.games.filter(function (item) {
	                return game.active == true;
                });
            }
        }   
    }
</script>
