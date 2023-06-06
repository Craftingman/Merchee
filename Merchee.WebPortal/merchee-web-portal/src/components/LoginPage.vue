<template>
    <div>
        <div>
            <div class="label">E-mail:</div>
            <input v-model="email" class="formInput" />
        </div>
        <div>
            <div class="label">Password:</div>
            <input v-model="password" class="formInput" />
        </div>
      <button @click="login">Login</button>
    </div>
  </template>
  
  <script>
  import http from './../api.js';

  export default {
    data() {
      return {
        bearerToken: '',
        email: '',
        password: '',
        userData: {}
      };
    },
    methods: {
      async login() {
        var sendData = {
            email: this.email,
            password: this.password
        };

        var result = await http.post('/auth/login', sendData);
            
        if (result.token) {
            this.userData = result;
            this.$store.commit("logIn", result);
            this.$router.push("/");
        }
      }
    }
  }
  </script>