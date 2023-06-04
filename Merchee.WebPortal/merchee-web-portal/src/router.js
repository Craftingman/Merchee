import { createRouter, createWebHistory } from 'vue-router';

import MainLayout from './components/MainLayout.vue';
import ViewCompany from './components/ViewCompany.vue';

const routes = [
  {
    path: '/',
    component: MainLayout,
    children: [
      { path: '', component: ViewCompany },
    ]
  }
];

const router = createRouter({
    history: createWebHistory(),
    routes
  });
  

export default router;