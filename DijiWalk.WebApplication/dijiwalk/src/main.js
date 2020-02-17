import Vue from 'vue'
import App from './App.vue'
import * as VueGoogleMaps from "vue2-google-maps";
import router from './router'
import './quasar'
import './vuex'


Vue.config.productionTip = false

Vue.use(VueGoogleMaps, {
    load: {
        key: "AIzaSyAUcoQ5Qyho5_2zQgaSBH3i1RtFsY1FLho",
        libraries: "places" // necessary for places input
    }
});

new Vue({
    router,
    render: h => h(App)
}).$mount('#app')


