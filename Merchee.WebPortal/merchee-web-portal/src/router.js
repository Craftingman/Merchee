import { createRouter, createWebHistory } from 'vue-router';
//import http from './api.js';

import MainLayout from './components/MainLayout.vue';
import ViewCompany from './components/ViewCompany.vue';
import LoginPage from './components/LoginPage.vue';

const routes = [
  {
    path: '/login',
    component: LoginPage
  },
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
  
// Add a global navigation guard
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token'); // Retrieve the token from local storage or any other storage mechanism you are using

  if (to.path !== '/login' && !token) {
    next('/login'); // Redirect to the login page if the token is missing and the destination is not the login page
  } else {
    next(); // Proceed to the next page
  }
});

export default router;