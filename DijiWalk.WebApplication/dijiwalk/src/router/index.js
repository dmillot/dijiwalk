import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import { SessionStorage } from 'quasar'

Vue.use(VueRouter)

const routes = [
    {
        path: '/login',
        name: 'login',
        component: Login
    },
    {
        path: '/',
        name: 'home',
        component: Home
    },
    {
        path: '/jeuActuel',
        name: 'jeuActuel',
        component: () => import('../views/JeuActuel.vue')
    },
    {
        path: '/jeuActif/:idJeu',
        name: 'jeuActif',
        component: () => import('../views/JeuActif.vue')
    },
    {
        path: '/jeu',
        name: 'jeu',
        component: () => import('../views/Jeu.vue')
    },
    {
        path: '/equipe',
        name: 'equipe',
        component: () => import('../views/Equipe.vue')
    },
    {
        path: '/chat',
        name: 'chat',
        component: () => import('../views/Chat.vue')
    },
    {
        path: '/etape',
        name: 'etape',
        component: () => import('../views/Etape.vue'),
    },
    {
        path: '/transport',
        name: 'transport',
        component: () => import('../views/Transport.vue')
    },
    {
        path: '/parcours',
        name: 'parcours',
        component: () => import('../views/Parcours.vue')
    },
    {
        path: '/participant',
        name: 'participant',
        component: () => import('../views/Participant.vue')
    },
    {
        path: '/validation',
        name: 'validation',
        component: () => import('../views/Validation.vue')
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

router.beforeEach((to, from, next) => {
    if (to.path != '/login') {
         if (SessionStorage.has("connectedOrganizer")) next()
         else next('/login')
    } else {
        next()
    }
})

export default router
