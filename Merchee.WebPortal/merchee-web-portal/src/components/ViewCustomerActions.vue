<template>
    <div>
        <n-select @update:value="loadCustomerActions" v-model:value="shelfProductId" :options="shelfProductsOptions" placeholder="Select shelf product"/>
        <n-data-table
            :columns="table.columns"
            :data="customerActions"
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
                    key: "actionTypeName",
                    title: "Type",
                },
                {
                    key: "quantity",
                    title: "Quantity",
                },
                {
                    key: "price",
                    title: "Price",
                },
                {
                    key: "time",
                    title: "Time",
                }
            ]
        },
        shelfProductId: null,
        shelfProductsOptions: [],
        customerActions: [],
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
        async loadCustomerActions() {
            console.log("Loading customer actions")
            var result = await http.get('/customerShelfActions', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize,
                shelfProductId: this.shelfProductId
            });
            if (result) {
                this.customerActions = result.map(e => {
                    return {
                        ...e,
                        actionTypeName: this.mapActionType(e.type)
                    }
                });
        }
    },
    mapActionType(actionType) {
        switch(actionType) {
            case 0: return "Take";
            case 1: return "Return";
        }
    },
    async loadShelfProducts() {
            var result = await http.get('/shelfProducts', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize
            });
            console.log(result);
            if (result) {
                this.shelfProductsOptions = result.map(i => {
                    return {
                        label: i.shelf.barcode + " : " + i.product.name,
                        value: i.id
                    };
                });
        }
    }
},
    computed: {
        
    },
    async mounted() {
            await this.loadShelfProducts()
        }
};
  </script>
  
  <style>

  </style>