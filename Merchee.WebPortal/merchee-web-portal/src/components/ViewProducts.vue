<template>
    <ve-table :columns="table.columns" :table-data="products"></ve-table>
</template>
  
  <script>
    import http from '@/api';

  export default {
    data() {
      return {
        table: {
            columns: [
                {
                    field: "name",
                    key: "a",
                    title: "Name",
                    align: "center",
                },
                {
                    field: "price",
                    key: "b",
                    title: "Price",
                    align: "center",
                },
                {
                    field: "fullWeight",
                    key: "b",
                    title: "Weight",
                    align: "center",
                },
                {
                    field: "shelfLifeTimeDays",
                    key: "b",
                    title: "Lifetime Days",
                    align: "center",
                }
            ]
        },
        products: [],
        pageSize: 10,
        pageNumber: 1
      };
    },
    methods: {
        async loadProducts() {
            var result = await http.get('/products', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize
            });
            if (result) {
                this.products = result;
        }
    }
    },
    computed: {
      
    },
    async mounted() {

    }
  };
  </script>
  
  <style>

  </style>