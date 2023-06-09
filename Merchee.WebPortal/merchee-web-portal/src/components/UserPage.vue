<template>
    <div>
        <n-form ref="formRef" :model="productModel" :rules="rules">
            <n-grid :span="24" :x-gap="24">
                <n-form-item-gi :span="12" path="firstName" label="First Name">
                    <n-input v-model:value="userModel.firstName" type="text" placeholder="First Name" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="lastName" label="Last Name">
                    <n-input v-model:value="userModel.lastName" type="text" placeholder="Last Name" />
                </n-form-item-gi>
                <n-form-item-gi :span="24" path="email" label="Email">
                    <n-input v-model:value="userModel.email" type="text" placeholder="Email" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="password" label="Password">
                    <n-input v-model:value="userModel.password" type="text" placeholder="Password" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="repeatPassword" label="Repeat Password">
                    <n-input v-model:value="userModel.repeatPassword" type="text" placeholder="Repeat Password" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="role" label="Role">
                    <n-select v-model:value="userModel.role" :options="roles" placeholder="Select role"/>
                </n-form-item-gi>
                <n-form-item-gi :span="24">
                    <n-button
                    round
                    type="primary"
                    @click="save"
                >
                    Save
                </n-button>
                </n-form-item-gi>
            </n-grid>
        </n-form>
    </div>
</template>
  
  <script>
    import http from './../api.js';

  export default {
    data() {
      return {
        userModel: {},
        roles: [
            { value: 0, label: "Administrator" },
            { value: 1, label: "Merchandiser" },
            { value: 2, label: "Employee" },
        ]
    }
    },
    props: {
        userId: String,
    },
    methods: {
        async loadUser() {
            var result = await http.get('/auth/user' + this.userId);
            if (result) {
                this.userModel = result;
            }
        },
        async save() {
            if (this.productId) {
                await http.put('/auth/users/' + this.userId, this.userModel);
            } else {
                await http.post('/auth/registerUser', this.userModel);
            }

            this.$router.push("/users");
        }
    },
    computed: {
      
    },
    async mounted() {
        if (this.userId) {
            await this.loadProduct();
        }
    }
  };
  </script>
  
  <style>

  </style>