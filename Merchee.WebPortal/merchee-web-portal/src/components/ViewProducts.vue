<template>
    <div>
        <n-button
                round
                type="primary"
                @click="addProduct"
            >
            Add new product
        </n-button>
        <n-data-table
            :columns="table.columns"
            :data="products"
            :pagination=false
            :bordered="false"
            :row-props="rowProps"
        />
    </div>
</template>
  
  <script>
    import http from './../api.js';

  export default {
    data() {
      return {
        table: {
            columns: [
                {
                    key: "name",
                    title: "Name",
                },
                {
                    key: "price",
                    title: "Price",
                },
                {
                    key: "fullWeight",
                    title: "Weight",
                },
                {
                    key: "shelfLifeTimeDays",
                    title: "Lifetime Days",
                },
            ]
        },
        products: [],
        pageSize: 10,
        pageNumber: 1
      };
    },
    methods: {
        rowProps(row) {
            var style = 'cursor: pointer;'
            if (!row.active) {
                style += 'color: grey !important;'
            }
            return {
                style: style,
                onClick: () => {
                    this.$router.push("products/" + row.id);
                }
            }
        },
        async loadProducts() {
            var result = await http.get('/products', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize
            });
            if (result) {
                this.products = result;
        }
    },
        addProduct() {
            this.$router.push("/products/add");
        }
    },
    computed: {
      
    },
    async mounted() {
        await this.loadProducts();
    }
  };
  </script>
  
  <style>

  </style>