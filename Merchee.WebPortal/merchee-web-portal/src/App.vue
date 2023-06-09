<template>
      <router-view></router-view>
      <n-notification-provider>
        <content />
      </n-notification-provider>
</template>

<script>
import http from './api.js';

export default {
  mounted() {
    if (localStorage.getItem("token")) {
      http.get("/auth/user").then(result => {
        this.$store.commit("setUserData", result);
      });
    } else {
      this.$router.push("/login");
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
