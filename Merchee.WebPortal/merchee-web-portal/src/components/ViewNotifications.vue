<template>
    <div>
        <n-button v-if="checkedNotificationKeys.length > 0"
                round
                type="primary"
                @click="markNotificationsAsRead"
            >
            Mark selected as read
        </n-button>
        <n-data-table
            :columns="table.columns"
            :data="notifications"
            :pagination="false"
            :bordered="false"
            :row-key="rowKey"
            @update:checked-row-keys="handleSelect"
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
                    type: 'selection',
                    disabled(row) {
                        return row.read;
                    }
                },
                {
                    key: "notification.timeCreated",
                    title: "Time Created",
                },
                {
                    key: "notification.message",
                    title: "Message",
                }
            ]
        },
        notifications: [],
        checkedNotificationKeys: [],
        pageSize: 10,
        pageNumber: 1
      };
    },
    methods: {
        async loadNotifications() {
            var result = await http.get('/notifications', { 
                pageNumber: this.pageNumber, pageSize: this.pageSize
            });
            if (result) {
                this.notifications = result;
        }
    },
    rowKey(row) {
        return row.id;
    },
    handleSelect(keys) {
        console.log(keys);
        this.checkedNotificationKeys = keys
    },
    async markNotificationsAsRead() {
        for (var notificationId of this.checkedNotificationKeys) {
            await http.put('/notifications/markAsRead/' + notificationId);
        }

        await this.loadNotifications();
    }
},
    computed: {
    },
    async mounted() {
        await this.loadNotifications();
    }
  };
  </script>
  
  <style>

  </style>