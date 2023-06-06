<template>
    <div class="">
        <div>Company name:</div>
        <div>{{companyInfo.name}}</div>
        <div>Address:</div>
        <div>{{companyInfo.address}}</div>
        <div>Currency:</div>
        <div>{{companyInfo.currency}}</div>
        <div>Company email:</div>
        <div>{{companyInfo.companyEmail}}</div>
        <div>Company phone:</div>
        <div>{{companyInfo.companyPhone}}</div>
    </div>
</template>

<script>
import http from './../api.js';

export default {
data() {
    return {
        companyInfo: {
            name: '',
            address: '',
            currency: '',
            companyEmail: '',
            companyPhone: ''
        }
    };
},
methods: {
    async loadCompany() {
        var result = await http.get('/companies/myCompany');
        if (result) {
            this.companyInfo = result;
        }
    }
},
async mounted() {
    await this.loadCompany();
  }
};
</script>

<style>
</style>