<template>
    <div>
        <n-form ref="formRef" :model="productModel" :rules="rules">
            <n-grid :span="24" :x-gap="24">
                <n-form-item-gi :span="12" path="productName" label="Product Name">
                    <n-input v-model:value="productModel.name" type="text" placeholder="Name" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="description" label="Description">
                    <n-input
                        v-model:value="productModel.description"
                        type="textarea"
                        placeholder="Description"
                    />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="barcode" label="Barcode">
                    <n-input v-model:value="productModel.barcode" type="text" placeholder="Barcode" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="price" label="Price">
                    <n-input-number v-model:value="productModel.price" placeholder="Price" step="0.01"/>
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="fullWeight" label="Full Weight">
                    <n-input-number v-model:value="productModel.fullWeight" placeholder="Full Weight" step="0.001" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="shelfLifetimeDays" label="Shelf Lifetime Days">
                    <n-input-number v-model:value="productModel.shelfLifeTimeDays" placeholder="Shelf Lifetime Days" step="1" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" label="Is Active?" path="isActive">
                    <n-switch v-model:value="productModel.active" />
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
        productModel: {}
    }
    },
    props: {
        productId: String,
    },
    methods: {
        async loadProduct() {
            var result = await http.get('/products/' + this.productId);
            if (result) {
                this.productModel = result;
            }
        },
        async save() {
            if (this.productId) {
                await http.put('/products/' + this.productId, this.productModel);
            } else {
                await http.post('/products', this.productModel);
            }

            this.$router.push("/products");
        }
    },
    computed: {
      
    },
    async mounted() {
        if (this.productId) {
            await this.loadProduct();
        }
    }
  };
  </script>
  
  <style>

  </style>