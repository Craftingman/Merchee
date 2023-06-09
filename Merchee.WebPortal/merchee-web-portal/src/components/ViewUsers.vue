<template>
    <div>
        <n-button
                round
                type="primary"
                @click="addUser"
            >
            Register new user
        </n-button>
        <n-data-table
            :columns="table.columns"
            :data="users"
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
                    key: "firstName",
                    title: "First Name",
                },
                {
                    key: "lastName",
                    title: "Last Name",
                },
                {
                    key: "email",
                    title: "Email",
                },
                {
                    key: "roleName",
                    title: "Role",
                },
            ]
        },
        users: [],
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
                    this.$router.push("users/" + row.id);
                }
            }
        },
        async loadUsers() {
            var result = await http.get('auth/users', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize
            });
            if (result) {
                this.users = result.map(e => {
                    return {
                        ...e,
                        roleName: this.mapRole(e.role)
                    }
                });
        }
    },
    mapRole(roleValue) {
        switch(roleValue) {
            case 0: return "Administrator";
            case 1: return "Merchandiser";
            case 2: return "Employee";
        }
    },
        addUser() {
            this.$router.push("/users/add");
        }
    },
    computed: {
      
    },
    async mounted() {
        await this.loadUsers();
    }
  };
  </script>
  
  <style>

  </style>