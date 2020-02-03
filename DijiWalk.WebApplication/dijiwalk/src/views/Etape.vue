<template>
    <q-page class="q-px-xl">
        <q-header elevated>
            <q-toolbar>

 
                <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-arrow-left" v-go-back=" '/' "/>
                <q-toolbar-title>DijiWalk</q-toolbar-title>

                <div class="q-mr-md cursor-pointer">
                    <q-btn flat round color="white" class="q-ml-md cursor-pointer" icon="fas fa-search" />
                    <q-menu>
                        <q-list bordered separator style="min-width: 100px">
                            <q-item>
                                <q-select filled
                                          v-model="modelParcours"
                                          use-input
                                          use-chips
                                          multiple
                                          input-debounce="0"
                                          label="Filtrer par parcours"
                                          :options="parcoursOptions"
                                          @filter="filterParcours"
                                          style="width: 250px" />
                            </q-item>
                        </q-list>
                    </q-menu>
                </div>
            </q-toolbar>
        </q-header>
        <div class="row full-width justify-center q-pr-xl q-my-md q-col-gutter-xl">
            <div class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card full-height cursor-pointer flex column justify-center items-center bg-negative first-card q-py-md">
                    <q-icon name="fas fa-plus text-white" style="font-size: 3vw;" />
                </q-card>
            </div>
            <div v-for="etape in etapes" v-bind:key="etape.id" class="col-xs-12 col-md-4 col-grow">
                <q-card class="my-card">
                    <q-img :src="etape.link" />

                    <q-card-section>
                        <q-btn fab
                               color="negative"
                               icon="fas fa-trash"
                               class="absolute"
                               style="top: 0; right: 12px; transform: translateY(-50%);" />

                        <div class="row no-wrap">
                            <div class="col text-left text-bold text-h6 ellipsis">
                                {{ etape.name }}
                            </div>
                        </div>
                        <div class="row items-center no-wrap text-grey">
                            <q-icon name="fas fa-calendar" />
                            <p class="q-ma-none q-ml-xs">{{ etape.date }}</p>
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

    const parcours = [
        'Parcours 1', 'Parcours 2', 'Parcours 3', 'Parcours 4', 'Parcours 5'
    ]

    const listeEtape = [
        { id: 1, name: "Etape 1", link: "https://ekladata.com/Q33WRkQK-cgPuG5zGUU7dTqOEpA.jpg", date: "30/02/2020" },
        { id: 2, name: "Etape 2", link: "https://www.cap-voyage.com/wp-content/uploads/2015/11/Dijon-C%C3%B4te-dOr-Patrimoine.jpg", date: "30/03/2020" },
        { id: 4, name: "Etape 3", link: "https://lh5.googleusercontent.com/proxy/YqT0wR_8t7yT5PRq0Z9Ha7ThARpzQ6JFQHsYuIC6jJ5Ox2elNpEzKKr-_Q9d39vVgr8gHvuEUgECj11bY95ElK5XhcshkeBhDMBjBbgPWyxeCFlzn0-zy5om7-6UtDHvqRlKgZn_bixF", date: "30/05/2020" },
        { id: 5, name: "Etape 4", link: "https://blog.ruedesvignerons.com/wp-content/uploads/2018/01/Halle-de-dijon-blog.png", date: "30/06/2020" },
        { id: 3, name: "Etape 5", link: "https://www.macommune.info/wp-content/uploads/2015/04/main-square-in-dijon.jpg", date: "30/04/2020" },
        { id: 6, name: "Etape 6", link: "https://www.cityzeum.com/media/poi/small/383848.jpg", date: "30/06/2020" }
    ]


    export default {
        name: 'etape',
        data() {
            return {
                modelParcours: null,
                parcoursOptions: parcours,
                etapes: listeEtape
            }
        },
        methods: {
            filterParcours(val, update) {
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
            }
        }
    }
</script>

<style scoped>
    .first-card:hover {
        background-color: #cc0016 !important;
    }
</style>