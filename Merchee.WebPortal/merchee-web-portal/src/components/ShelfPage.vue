<template>
    <div>
        <n-form ref="shelfFormRef" :model="shelfModel" :rules="rules">
            <n-grid :span="24" :x-gap="24">
                <n-form-item-gi :span="10" path="barcode" label="Barcode">
                    <n-input v-model:value="shelfModel.barcode" type="text" placeholder="Barcode" :readonly="!!shelfId" />
                </n-form-item-gi>
                <n-form-item-gi :span="10" path="maxWeight" label="Max Weight">
                    <n-input-number v-model:value="shelfModel.maxWeight" placeholder="Max Weight" step="1"/>
                </n-form-item-gi>
                <n-form-item-gi :span="4">
                    <n-button
                    round
                    type="primary"
                    @click="saveShelf"
                    >
                        Save
                    </n-button>
                </n-form-item-gi>
            </n-grid>
        </n-form>
        <n-divider />
        <n-form v-if="!!shelfProductModel" ref="shelfProductFormRef" :model="shelfProductModel" :rules="rules">
            <n-grid :span="24" :x-gap="24">
                <n-form-item-gi :span="12" path="product" label="Product">
                    <n-select v-model:value="shelfProductModel.productID" :options="productsOptions" placeholder="Select product"/>
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="location" label="Location">
                    <n-input v-model:value="shelfProductModel.location" type="text" placeholder="Location" />
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="fullCapacity" label="Full Capacity">
                    <n-input-number v-model:value="shelfProductModel.fullCapacity" placeholder="Full Capacity" step="1"/>
                </n-form-item-gi>
                <n-form-item-gi :span="12" path="minQuantity" label="Min Quantity">
                    <n-input-number v-model:value="shelfProductModel.minQuantity" placeholder="Min Quntity" step="1"/>
                </n-form-item-gi>
                <n-form-item-gi :span="4">
                    <n-button
                    round
                    type="primary"
                    @click="saveShelfProduct"
                    >
                        Save shelf product
                    </n-button>
                </n-form-item-gi>
            </n-grid>
        </n-form>
        <n-button v-else
            round
            type="primary"
            @click="addEmptyShelfProduct"
            >
                Add product
        </n-button>
    </div>
</template>
  
  <script>
    import http from './../api.js';

  export default {
    data() {
      return {
        shelfModel: {},
        shelfProductModel: null,
        productsOptions: []
    }
    },
    props: {
        shelfId: String,
    },
    methods: {
        async addEmptyShelfProduct() {
            this.shelfProductModel = {}
        },
        async loadShelf() {
            var result = await http.get('/shelves/' + this.shelfId);
            if (result) {
                this.shelfModel = result;
            }
        },
        async loadShelfProduct() {
            var result = await http.get('/shelfProducts/byShelf/' + this.shelfId);
            if (result) {
                this.shelfProductModel = result[0];
            }
        },
        async loadProducts() {
            var result = await http.get('/products');
            if (result) {
                this.productsOptions = result.map(i => {
                    return {
                        label: i.name,
                        value: i.id
                    };
                });
            }
        },
        async saveShelf() {
            if (this.shelfId) {
                await http.put('/shelves/' + this.shelfId, this.shelfModel);
            } else {
                await http.post('/shelves', this.shelfModel);
            }

            this.$router.push("/shelves");
        },
        async saveShelfProduct() {
            this.shelfProductModel.shelfId = this.shelfModel.id;
            if (this.shelfProductModel.id) {
                await http.put('/shelfProducts/' + this.shelfProductModel.id, this.shelfProductModel);
            } else {
                await http.post('/shelfProducts', this.shelfProductModel);
            }

            alert("Shelf Product Saved");
        }
    },
    computed: {
    },
    async mounted() {
        if (this.shelfId) {
            await this.loadShelf();
            await this.loadShelfProduct();
            await this.loadProducts();
        }
    }
  };
  </script>
  
  <style>

  </style>