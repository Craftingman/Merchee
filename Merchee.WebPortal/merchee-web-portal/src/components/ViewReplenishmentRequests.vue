<template>
    <div>
        <n-data-table
            :columns="table.columns"
            :data="replenishmentRequests"
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
                    key: "shelfProduct.shelf.barcode",
                    title: "Shelf Barcode",
                },
                {
                    key: "shelfProduct.location",
                    title: "Shelf Location",
                },
                {
                    key: "shelfProduct.product.barcode",
                    title: "Product Barcode",
                },
                {
                    key: "shelfProduct.product.name",
                    title: "Prodcut Name",
                },
                {
                    key: "quantityNeeded",
                    title: "Quantity Needed",
                },
                {
                    key: "timeCreated",
                    title: "Time Created",
                },
                {
                    key: "timeCompleted",
                    title: "Time Completed",
                },
            ]
        },
        replenishmentRequests: [],
        pageSize: 10,
        pageNumber: 1
      };
    },
    methods: {
        rowProps() {
            var style = 'cursor: pointer;'
            return {
                style: style
            }
        },
        async loadReplenishmentRequests() {
            var result = await http.get('/replenishmentRequests', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize
            });
            if (result) {
                this.replenishmentRequests = result;
        }
    }
    },
    computed: {
      
    },
    async mounted() {
        await this.loadReplenishmentRequests();
    }
  };
  </script>
  
  <style>

  </style>