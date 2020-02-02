import Vue from 'vue'

import './styles/quasar.scss'
import iconSet from 'quasar/icon-set/fontawesome-v5.js'
import '@quasar/extras/fontawesome-v5/fontawesome-v5.css'
import { Quasar, Notify, GoBack } from 'quasar'

Vue.use(Quasar, {
    config: {
        notify: { /* Notify defaults */ }
    },
    components: { /* not needed if importStrategy is not 'manual' */ },
    directives: {
        GoBack
    },
    plugins: {
        Notify
    },
    iconSet: iconSet
})