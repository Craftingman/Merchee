<template>
    <div>
        <n-button
                round
                type="primary"
                @click="addShelve"
            >
            Register new shelve
        </n-button>
        <n-data-table
            :columns="table.columns"
            :data="shelves"
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
                    key: "barcode",
                    title: "Barcode",
                },
                {
                    key: "maxWeight",
                    title: "Max Weight",
                },
                {
                    key: "shelfProducts[0].location",
                    title: "Location",
                },
                {
                    key: "shelfProducts[0].currentQuantity",
                    title: "Current quantity",
                },
                {
                    key: "shelfProducts[0].fullCapacity",
                    title: "Full capacity",
                },
                {
                    key: "shelfProducts[0].product.name",
                    title: "Product",
                }
            ]
        },
        shelves: [],
        pageSize: 10,
        pageNumber: 1
      };
    },
    methods: {
        rowProps(row) {
            var style = 'cursor: pointer;'
            if (!row.active) {
                style += 'background: grey!;'
            }
            return {
                style: style,
                onClick: () => {
                    this.$router.push("shelves/" + row.id);
                }
            }
        },
        async loadShelves() {
            var result = await http.get('/shelves', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize
            });
            if (result) {
                this.shelves = result;
        }
    },
    addShelve() {
            this.$router.push("/shelves/add");
        }
    },
    computed: {
      
    },
    async mounted() {
        await this.loadShelves();
    }
  };
  </script>
  
  <style>

  </style>