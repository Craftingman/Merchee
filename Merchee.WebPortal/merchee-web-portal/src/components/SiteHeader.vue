<template>
  <header class="header">
    <div class="logo">
      <img src=".\..\assets\logo.png" alt="Logo">
    </div>
    <div class="account">
      <div class="account-name">{{accountName}}</div>
      <button class="account-btn" @click="toggleDropdown"></button>
      <ul class="dropdown-menu" v-if="showDropdown">
        <li><a href="#">Profile</a></li>
        <li><a href="#">Settings</a></li>
        <li><a href="#" v-on:click="logout()">Logout</a></li>
      </ul>
    </div>
  </header>
</template>

<script>
export default {
  data() {
    return {
      showDropdown: false
    };
  },
  methods: {
    toggleDropdown() {
      this.showDropdown = !this.showDropdown;
    },
    logout() {
      this.$store.commit("logOut");
      this.$router.push("/login");
    }
  },
  computed: {
    accountName () {
      return this.$store.state.userData.firstName + " " + this.$store.state.userData.lastName;
    }
  }
};
</script>

<style>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  background-color: #f2f2f2;
  width: 100%;
}

.account-name {
  font-size: 24px;
}

.logo img {
  width: 50px;
}

.account-btn {
  background: none;
  border: none;
  cursor: pointer;
  background-image: url("./../assets/down-icon.png");
  background-repeat: no-repeat;
  background-size: contain;
  width: 50px;
  height: 50px;
}

.dropdown-menu {
  position: absolute;
  top: 40px;
  right: 0;
  list-style: none;
  padding: 0;
  margin: 0;
  background-color: #fff;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
}

.dropdown-menu li {
  padding: 10px;
}

.dropdown-menu li a {
  color: #333;
  text-decoration: none;
}

.dropdown-menu li a:hover {
  background-color: #f2f2f2;
}
</style>