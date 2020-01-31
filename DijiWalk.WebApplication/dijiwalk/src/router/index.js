import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'

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
    path: '/jeu',
    name: 'jeu',
    component: () => import('../views/Jeu.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
