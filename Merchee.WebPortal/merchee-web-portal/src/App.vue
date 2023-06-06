<template>
      <router-view></router-view>
</template>

<script>
import http from './api.js';

export default {
  async beforeCreate() {
    if (localStorage.getItem("token")) {
      var result = await http.get("/auth/user");
      this.$store.commit("logIn", result);
    }
  }
};
</script>

<style>
* {
  margin: 0;
  box-sizing: border-box;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
