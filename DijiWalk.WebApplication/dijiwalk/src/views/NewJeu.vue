<template>
    <q-page class="q-px-xl">
        <div class="row flex flex-center q-px-xl">
            <div class="col-xs-12 col-md-10">
                <q-card class="my-card">
                    <q-card-section>
                        <div class="row justify-between">
                            <div class="col-3">
                                <q-input v-model="text" label="Nom" />
                            </div>
                            <div class="col-3">
                                <q-input color="primary" v-model="date" label="Date" />
                            </div>
                            <div class="col-3">
                                <q-select v-model="model" :options="options" label="Parcours" />
                            </div>
                        </div>
                        <div class="row justify-center q-mt-xl">
                            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d86531.29608378504!2d4.962181193468455!3d47.33185966625776!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47f29d8ceffd9675%3A0x409ce34b31458d0!2s21000%20Dijon!5e0!3m2!1sfr!2sfr!4v1580477557627!5m2!1sfr!2sfr" width="800" height="600" frameborder="0" style="border:0;" allowfullscreen=""></iframe>
                        </div>
                    </q-card-section>
                </q-card>
            </div>
        </div>
    </q-page>
</template>

<script>
const parcours = [
    '1', '2', '3', '4'
]
export default {
    name: "new-jeu",
    data () {
        return {
            options: parcours
        }
    }
}
</script>