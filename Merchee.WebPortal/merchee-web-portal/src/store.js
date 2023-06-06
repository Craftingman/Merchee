import { createStore } from 'vuex'

const store = createStore({
    state () {
      return {
        userData: {},
        loggedIn: false
      }
    },
    mutations: {
      logIn (state, userData) {
        state.loggedIn = true;
        state.userData = userData;
        localStorage.setItem("token", userData.token);
      },
      logOut (state) {
        state.loggedIn = false;
        state.userData = {};
        localStorage.removeItem("token");
      }
    }
  });

  export default store;