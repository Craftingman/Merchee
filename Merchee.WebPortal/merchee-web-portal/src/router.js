import { createRouter, createWebHistory } from 'vue-router';
//import http from './api.js';

import MainLayout from './components/MainLayout.vue';
import ViewCompany from './components/ViewCompany.vue';
import ViewProducts from './components/ViewProducts.vue';
import LoginPage from './components/LoginPage.vue';
import ProductPage from './components/ProductPage.vue';
import ViewShelves from './components/ViewShelves.vue';
import ShelfPage from './components/ShelfPage.vue';
import ViewUsers from './components/ViewUsers.vue';
import UserPage from './components/UserPage.vue';
import ViewReplenishmentRequests from './components/ViewReplenishmentRequests.vue';
import ViewCustomerActions from './components/ViewCustomerActions.vue';
import ViewNotifications from './components/ViewNotifications.vue';

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
      { path: 'products', component: ViewProducts },
      { path: 'products/:productId', component: ProductPage, props: true },
      { path: 'products/add', component: ProductPage },
      { path: 'shelves', component: ViewShelves },
      { path: 'shelves/:shelfId', component: ShelfPage, props: true },
      { path: 'shelves/add', component: ShelfPage },
      { path: 'users', component: ViewUsers },
      { path: 'users/:userId', component: UserPage, props: true },
      { path: 'users/add', component: UserPage },
      { path: 'replenishmentRequests', component: ViewReplenishmentRequests },
      { path: 'customerActions', component: ViewCustomerActions },
      { path: 'notifications', component: ViewNotifications },
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