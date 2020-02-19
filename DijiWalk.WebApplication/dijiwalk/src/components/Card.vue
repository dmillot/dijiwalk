<template>
    <div class="col-xs-12 col-md-4 col-grow">
        <q-card ripple="black" @click="navigateToLink()" class="my-card full-height cursor-pointer">
            <q-card-section class="flex column flex-center">
                <q-icon :name="icon" class="q-mt-lg" style="font-size: 4em" />
                <h5 class="text-bold q-ma-none q-mt-lg">{{ title }}</h5>
                <p class="q-mt-xl text-center">{{ description }}</p>
            </q-card-section>
        </q-card>
    </div>
</template>

<script>
    export default {
        name: 'card',
        props: ['link', 'params', 'icon', 'title', 'description', 'disabled'],
        methods: {
            navigateToLink() {
                this.$q.loading.show()
                if (this.params !== undefined) {
                    if (this.disabled === false) {
                        this.$q.loading.hide()
                        this.$router.push({ name: this.link, params: { idJeu: this.params.id } });
                    }
                }
                else {
                    this.$q.loading.hide()
                    this.$router.push(this.link);
                }
            }
        }
    }
</script>
